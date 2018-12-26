

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SALON_HAIR_CORE.Service
{
    public class InvoiceService: GenericRepository<Invoice> ,IInvoice
    {
        private salon_hairContext _salon_hairContext;
        private ISysObjectAutoIncreament _sysObjectAutoIncreamentService;
        public InvoiceService(ISysObjectAutoIncreament sysObjectAutoIncreamentService,salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
            _sysObjectAutoIncreamentService = sysObjectAutoIncreamentService;
        }        
        public new void Edit(Invoice invoice)
        {
            invoice.Updated = DateTime.Now;

            base.Edit(invoice);
        }
        public async new Task<int> EditAsync(Invoice invoice)
        {            
            invoice.Updated = DateTime.Now;         
            return await base.EditAsync(invoice);
        }
        public new async Task<int> AddAsync(Invoice invoice)
        {
            var indexObject = _salon_hairContext.SysObjectAutoIncreament.Where(e => e.SpaId == invoice.SalonId && e.ObjectName.Equals("Invoice")).FirstOrDefault();
         
            if(indexObject == null)
            {
                indexObject = new SysObjectAutoIncreament
                {
                    SpaId = invoice.SalonId,
                    ObjectIndex = 1,
                    ObjectName = "Invoice"
                };
                await _salon_hairContext.SysObjectAutoIncreament.AddAsync(
                 indexObject
                    );
            }
            else
            {
                indexObject.ObjectIndex++;
                _salon_hairContext.SysObjectAutoIncreament.Update(indexObject);
            }
            invoice.Created = DateTime.Now;
            invoice.Code = "ES" + indexObject.ObjectIndex.ToString("000000");
            //invoice = LoadAllReference(invoice);
            await  _salon_hairContext.Invoice.AddAsync(invoice);
            return await _salon_hairContext.SaveChangesAsync();
        }
        public new void Add(Invoice invoice)
        {
            invoice.Created = DateTime.Now;
            base.Add(invoice);
        }
        public new void Delete(Invoice invoice)
        {
            invoice.Status = "DELETED";
            base.Edit(invoice);
        }
        public new async Task<int> DeleteAsync(Invoice invoice)
        {
            invoice.Status = "DELETED";
            return await base.EditAsync(invoice);
        }
        public async Task EditAsPayAsync(Invoice dataUpdate)
        {
            #region package transaction
            var listInvoiceDetail = _salon_hairContext.InvoiceDetail.Where(e => e.InvoiceId == dataUpdate.Id).ToList();          
            //add transaction customer-package
            var listCustomerPackageTransaction = GetCustomerPackageTransactionsByInvoiceDetail(dataUpdate, listInvoiceDetail);
            await _salon_hairContext.CustomerPackageTransaction.AddRangeAsync(listCustomerPackageTransaction);
            #endregion
            //   //Get Warehoure Transaction
            #region warehouse transaction 
            var warehouseTransactionByInvoice = WarehouseTransactionByInvoice(dataUpdate, listInvoiceDetail);
           _salon_hairContext.WarehouseTransaction.Add(warehouseTransactionByInvoice);
            #endregion
            //var cashBookIncome = CreateCashBookIncomeTransaction(dataUpdate, listInvoiceDetail);
            #region cashbook transaction           
            var listcashBookIncome = new List<CashBookTransaction>();
            var sysObjectAutoIncreamentService = _sysObjectAutoIncreamentService.GetCodeByObjectAsyncWithoutSave(_salon_hairContext, nameof(CashBookTransaction), dataUpdate.SalonId);
            var paymentMethod = _salon_hairContext.InvoicePayment.Where(e => e.InvoiceId == dataUpdate.Id).Include(e=>e.InvoiceMethod).AsNoTracking().ToList();
            var cashbookTransactionCategoryId = _salon_hairContext.CashBookTransactionCategory.Where(e => e.Code.Equals(CASH_BOOK_TRANSACTION_CATEGORY.SALE)).Select(e => e.Id).FirstOrDefault();
            paymentMethod.Where(e=>!e.InvoiceMethod.Code.Equals(PAYMENT_METHOD.DEBIT)).ToList().ForEach(e => {
                var code = GENERATECODE.BOOKING + sysObjectAutoIncreamentService.ObjectIndex.ToString(GENERATECODE.FORMATSTRING);
                listcashBookIncome.Add(CreateCashBookIncomeTransaction(e, dataUpdate, cashbookTransactionCategoryId, code));
                sysObjectAutoIncreamentService.ObjectIndex++;
            });
            #endregion

            #region customer dept transaction
            paymentMethod.Where(e => e.InvoiceMethod.Code.Equals(PAYMENT_METHOD.DEBIT)).ToList().ForEach(e => {
                var customerDeptransaction = new CustomerDebtTransaction {
                  CustomerId = dataUpdate.CustomerId,
                  CreatedBy = dataUpdate.CreatedBy,
                  Created = DateTime.Now,
                  Action = DEPT_BEHAVIOR.DEBIT,
                  InvoiceId = dataUpdate.Id,
                  SalonBranchId = dataUpdate.SalonBranchId,
                  SalonId = dataUpdate.SalonId,
                  Money = e.Total,                          
                };
                _salon_hairContext.CustomerDebtTransaction.Add(customerDeptransaction);
            });           
            #endregion

                ///Caculate commision by CreateCashBookOutcomeTransaction(dataUpdate, listInvoiceDetail);
                ///
                //var cashBookOutcome = CreateCashBookOutcomeTransaction(dataUpdate, listInvoiceDetail);3
                _salon_hairContext.Invoice.Update(dataUpdate);
            await _sysObjectAutoIncreamentService.CreateOrUpdateAsync(_salon_hairContext, sysObjectAutoIncreamentService);
            _salon_hairContext.CashBookTransaction.AddRange(listcashBookIncome);
           // _salon_hairContext.CashBookTransaction.Add(cashBookOutcome);
            await _salon_hairContext.SaveChangesAsync();
        }

        private CashBookTransaction CreateCashBookOutcomeTransaction(Invoice invoice, List<InvoiceDetail> listInvoiceDetail)
        {
            var indexObject = _salon_hairContext.SysObjectAutoIncreament.Where(e => e.SpaId == invoice.SalonId && e.ObjectName.Equals(nameof(CashBookTransaction))).FirstOrDefault();

            if (indexObject == null)
            {
                indexObject = new SysObjectAutoIncreament
                {
                    SpaId = invoice.SalonId,
                    ObjectIndex = 1,
                    ObjectName = nameof(CashBookTransaction)
                };
                _salon_hairContext.SysObjectAutoIncreament.Add(
                indexObject
                   );
            }
            else
            {
                indexObject.ObjectIndex++;
                _salon_hairContext.SysObjectAutoIncreament.Update(indexObject);
            }

            var cashBookTransactionOutcome = new CashBookTransaction
            {
                Action = CASHBOOKTRANSACTIONACTION.OUTCOME,
                //Cashier = invoice.Cashier.Name,
                Description = $"OutCome FROM INVOICE :{invoice.Code}",
                Created = DateTime.Now,
                SalonBranchId = invoice.SalonBranchId,
                SalonId = invoice.SalonId,
                CreatedBy = invoice.UpdatedBy,
                //Money = invoice.Total,
                Code = indexObject.ObjectIndex.ToString("ES000000"),
                InvoiceId = invoice.Id
            };
            _salon_hairContext.SysObjectAutoIncreament.Update(indexObject);
            var listCommision = _salon_hairContext.CommissionArrangement
                .Where(e => e.InvoiceId == invoice.Id).ToList()
                .Where(e=>e.SaleStaffId!=default || e.ServiceStaffId!=default);
            //For Commision/ get ObjectPrice to cacualate the commission           
            var listCashBookTransactionDetail = new List<CashBookTransactionDetail>();

            var listStaffProductCommissionTransaction = CreateStaffProductCommissionTransactions
                (listCommision.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.PRODUCT)).ToList(),invoice.SalonId,invoice.SalonBranchId);
            var listStaffPackageCommissionTransaction = CreateStaffPackageCommissionTransactions
                (listCommision.Where(e=>e.ObjectType.Equals(INVOICEOBJECTTYPE.PACKAGE)).Where(e=>e.IsPaid==false).ToList(), invoice.SalonId, invoice.SalonBranchId);
            var listStaffServiceCommissionTransaction = CreateStaffServiceCommissionTransaction
                (listCommision.Where(e=>e.ObjectType.Equals(INVOICEOBJECTTYPE.SERVICE)).Where(e=>e.IsPaid==false).ToList(), invoice.SalonId, invoice.SalonBranchId);
            var listStaffServiceServiceCommissionTransaction = CreateStaffServiceServiceCommissionTransaction
              (listCommision.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.SERVICE)).Where(e => e.IsPaid == true).ToList(), invoice.SalonId, invoice.SalonBranchId);
            #region Tracking staff service - product - package
          
            _salon_hairContext.StaffProductCommissionTransaction.AddRange(listStaffProductCommissionTransaction);
            _salon_hairContext.StaffPackageCommissionTransaction.AddRange(listStaffPackageCommissionTransaction);
            _salon_hairContext.StaffServiceCommissionTransaction.AddRange(listStaffServiceCommissionTransaction);
            _salon_hairContext.StaffServiceCommissionTransaction.AddRange(listStaffServiceServiceCommissionTransaction);

            listStaffProductCommissionTransaction.ForEach(e => {
                listCashBookTransactionDetail.Add(CreateCashBookTransactionDetails(e.SalonId, e.SalonBranchId, "Pay for sale Product", e.CommissionValue, e.StaffId));
            });
            listStaffPackageCommissionTransaction.ForEach(e => {
                listCashBookTransactionDetail.Add(CreateCashBookTransactionDetails(e.SalonId, e.SalonBranchId, "Pay for sale Package", e.CommissionValue, e.StaffId));
            });
            listStaffServiceCommissionTransaction.ForEach(e => {
                listCashBookTransactionDetail.Add(CreateCashBookTransactionDetails(e.SalonId, e.SalonBranchId, "Pay for sale Service ", e.CommissionValue, e.StaffId));
            });
            listStaffServiceServiceCommissionTransaction.ForEach(e => {
                listCashBookTransactionDetail.Add(CreateCashBookTransactionDetails(e.SalonId, e.SalonBranchId, "Pay for do Service ", e.CommissionServiceValue, e.StaffId));
            });
            #endregion 
            cashBookTransactionOutcome.CashBookTransactionDetail = listCashBookTransactionDetail;
            cashBookTransactionOutcome.Money = listCashBookTransactionDetail.Sum(e => e.Money);
            return cashBookTransactionOutcome;
        }

        private List<CustomerPackageTransaction> GetCustomerPackageTransactionsByInvoiceDetail(Invoice dataUpdate, List<InvoiceDetail> invoiceDetails)
        {
            //Get list package
            var listPackge = invoiceDetails
                .Where(e => e.Status.Equals(OBJECTSTATUS.ENABLE))
                .Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.PACKAGE));

            var listCustomerPackageTransaction = new List<CustomerPackageTransaction>();
            listPackge.ToList().ForEach(e => {
                listCustomerPackageTransaction.Add(new CustomerPackageTransaction
                {
                    CreatedBy = dataUpdate.CreatedBy,
                    Created = DateTime.Now,
                    Action = e.IsPaid.Value ? CUSTOMERPACKAGETRANSACTIONACTION.USE : CUSTOMERPACKAGETRANSACTIONACTION.PAY,
                    CustomerId = dataUpdate.CustomerId,
                    PackageId = e.ObjectId,
                    Quantity = e.Quantity,
                    SalonId = dataUpdate.SalonId,
                    SalonBranchId = dataUpdate.SalonBranchId
                });
            });
            return listCustomerPackageTransaction;
        } 
        private WarehouseTransaction WarehouseTransactionByInvoice(Invoice invoice, List<InvoiceDetail> invoiceDetails)
        {
            WarehouseTransaction warehouseTransaction = new WarehouseTransaction
            {
                Action = WAREHOUSETRANSATIONACTION.SALE,
                Created = DateTime.Now,
                CreatedBy = invoice.CreatedBy,
                InvoiceId = invoice.Id,
                SalonBranchId = invoice.SalonBranchId,
                SalonId = invoice.SalonId,
                Creator = invoice.CreatedBy,
                Description = invoice.Note
            };
            var listWarehouseTransactionDetail = new List<WarehouseTransactionDetail>();


            //invoiceDetails.ForEach(e =>
            foreach (var e in invoiceDetails)
          
            {
                switch (e.ObjectType)
                {   //Product
                    case INVOICEOBJECTTYPE.PRODUCT:
                        listWarehouseTransactionDetail.Add(new WarehouseTransactionDetail
                        {
                            CreatedBy = invoice.CreatedBy,                            
                            Created = DateTime.Now,
                            ProductId = e.ObjectId,
                            Quantity = e.Quantity,                            
                        });
                    break;
                        //service
                    case INVOICEOBJECTTYPE.SERVICE:
                        var serviceProduct = _salon_hairContext.ServiceProduct.Where(x => x.ServiceId == e.ObjectId);
                        serviceProduct.ToList().ForEach(x =>
                        {
                            listWarehouseTransactionDetail.Add(new WarehouseTransactionDetail
                            {
                                CreatedBy = invoice.CreatedBy,
                                Created = DateTime.Now,
                                ProductId = x.ProductId,
                                Quantity = 0,
                                TotalVolume = x.Quota * e.Quantity,
                            });
                        });
                    break;
                    case INVOICEOBJECTTYPE.PACKAGE:
                        if (e.IsPaid.Value)
                        {
                            var listService = _salon_hairContext.ServicePackage.Where(c=>c.PackageId == e.ObjectId)
                            .Include(c => c.Service)
                            .ThenInclude(c => c.ServiceProduct);

                            foreach (var x in listService)
                            {
                                x.Service.ServiceProduct.ToList().ForEach(v =>
                                {
                                    listWarehouseTransactionDetail.Add(new WarehouseTransactionDetail
                                    {
                                        CreatedBy = invoice.CreatedBy,
                                        Created = DateTime.Now,
                                        ProductId = v.ProductId,
                                        Quantity = 0,
                                        TotalVolume = v.Quota * e.Quantity,                                        
                                    });
                                });
                            }
                        }
                    break;
                };
            };
            warehouseTransaction.WarehouseTransactionDetail = listWarehouseTransactionDetail;
            return warehouseTransaction;
            
        }
        private List<CashBookTransaction> CreateCashBookTransaction(Invoice invoice, List<InvoiceDetail> invoiceDetails)
        {
            List<CashBookTransaction> cashBookTransactions = new List<CashBookTransaction>();
            #region Create Code
            var indexObject = _salon_hairContext.SysObjectAutoIncreament.Where(e => e.SpaId == invoice.SalonId && e.ObjectName.Equals(nameof(CashBookTransaction))).FirstOrDefault();

            if (indexObject == null)
            {
                indexObject = new SysObjectAutoIncreament
                {
                    SpaId = invoice.SalonId,
                    ObjectIndex = 1,
                    ObjectName = nameof(CashBookTransaction)
                };
                 _salon_hairContext.SysObjectAutoIncreament.Add(
                 indexObject
                    );
            }
            else
            {
                indexObject.ObjectIndex++;               
            }
            #endregion
            //income on invoice
            var cashBookTransactionIncome = new CashBookTransaction
            {
                Action = CASHBOOKTRANSACTIONACTION.INCOME,
                // Cashier = invoice.Cashier.Name,
                Description = $"INCOME FROM INVOICE :{invoice.Code}",
                Created = DateTime.Now,
                SalonBranchId = invoice.SalonBranchId,
                SalonId = invoice.SalonId,
                CreatedBy = invoice.UpdatedBy,
                Money = invoice.Total,
                Code = indexObject.ObjectIndex.ToString("000000"),
                InvoiceId = invoice.Id
        };

            indexObject.ObjectIndex++;
            var cashBookTransactionOutcome = new CashBookTransaction
            {
                Action = CASHBOOKTRANSACTIONACTION.OUTCOME,
                //Cashier = invoice.Cashier.Name,
                Description = $"OutCome FROM INVOICE :{invoice.Code}",
                Created = DateTime.Now,
                SalonBranchId = invoice.SalonBranchId,
                SalonId = invoice.SalonId,
                CreatedBy = invoice.UpdatedBy,
                //Money = invoice.Total,
                Code = indexObject.ObjectIndex.ToString("000000"),
                InvoiceId = invoice.Id
            };
            _salon_hairContext.SysObjectAutoIncreament.Update(indexObject);

            var listCashBookTransactionDetail = new List<CashBookTransactionDetail>();
            var listProductId = invoiceDetails.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.PRODUCT)).Select(e => e.ObjectId);
            var listServiceId = invoiceDetails.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.SERVICE)).Select(e => e.ObjectId);
            var listPackageId = invoiceDetails.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.PACKAGE)).Select(e => e.ObjectId);
            var commissionArrangements = _salon_hairContext.CommissionArrangement.Where(e => e.InvoiceId == invoice.Id).ToList();

            var listStaffProductCommissionTransaction = CreateStaffProductCommissionTransactions(commissionArrangements.Where(e=>e.ObjectType.Equals(INVOICEOBJECTTYPE.PRODUCT)).ToList(),invoice.SalonId,invoice.SalonBranchId);
            var listStaffPackageCommissionTransaction = CreateStaffPackageCommissionTransactions(commissionArrangements.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.PACKAGE)).Where(e=>e.IsPaid==false).ToList(), invoice.SalonId, invoice.SalonBranchId);
            var listStaffServiceCommissionTransaction = CreateStaffServiceCommissionTransaction(commissionArrangements.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.SERVICE)).Where(e=>e.IsPaid==false).ToList(), invoice.SalonId, invoice.SalonBranchId);
            var listStaffServiceServiceCommissionTransaction = CreateStaffServiceServiceCommissionTransaction(commissionArrangements.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.SERVICE)).Where(e => e.IsPaid == true).ToList(), invoice.SalonId, invoice.SalonBranchId);

       

            listStaffProductCommissionTransaction.ForEach(e=> {
                listCashBookTransactionDetail.Add(CreateCashBookTransactionDetails(e.SalonId, e.SalonBranchId, "Pay for sale Product", e.CommissionValue, e.StaffId));
            });
            listStaffPackageCommissionTransaction.ForEach(e => {
                listCashBookTransactionDetail.Add(CreateCashBookTransactionDetails(e.SalonId, e.SalonBranchId, "Pay for sale Package", e.CommissionValue, e.StaffId));
            });
            listStaffPackageCommissionTransaction.ForEach(e => {
                listCashBookTransactionDetail.Add(CreateCashBookTransactionDetails(e.SalonId, e.SalonBranchId, "Pay for sale Service ", e.CommissionValue, e.StaffId));
            });
            listStaffServiceServiceCommissionTransaction.ForEach(e => {
                listCashBookTransactionDetail.Add(CreateCashBookTransactionDetails(e.SalonId, e.SalonBranchId, "Pay for do Service ", e.CommissionServiceValue, e.StaffId));
            });

            //cashBookTransactionOutcome.CashBookTransactionDetail = listCashBookTransactionDetail;
            cashBookTransactionOutcome.Money = listCashBookTransactionDetail.Sum(e => e.Money);
            return new List<CashBookTransaction> { cashBookTransactionIncome, cashBookTransactionOutcome };

        }
        private CashBookTransaction CreateCashBookIncomeTransaction(Invoice invoice, List<InvoiceDetail> invoiceDetails)
        {
          
            var indexObject = _salon_hairContext.SysObjectAutoIncreament.Where(e => e.SpaId == invoice.SalonId && e.ObjectName.Equals(nameof(CashBookTransaction))).FirstOrDefault();

            if (indexObject == null)
            {
                indexObject = new SysObjectAutoIncreament
                {
                    SpaId = invoice.SalonId,
                    ObjectIndex = 1,
                    ObjectName = nameof(CashBookTransaction)
                };
                _salon_hairContext.SysObjectAutoIncreament.Add(
                indexObject
                   );
            }
            else
            {
                indexObject.ObjectIndex++;
                _salon_hairContext.SysObjectAutoIncreament.Update(indexObject);
            }

            //income on invoice
            var cashBookTransactionIncome = new CashBookTransaction
            {
                Action = CASHBOOKTRANSACTIONACTION.INCOME,
                // Cashier = invoice.Cashier.Name,
                Description = $"INCOME FROM INVOICE :{invoice.Code}",
                Created = DateTime.Now,
                SalonBranchId = invoice.SalonBranchId,
                SalonId = invoice.SalonId,
                CreatedBy = invoice.UpdatedBy,
                Money = invoice.Total,
                Code = indexObject.ObjectIndex.ToString("ES000000"),
                InvoiceId = invoice.Id
            };

            return cashBookTransactionIncome;

        }
        private CashBookTransaction CreateCashBookIncomeTransaction(InvoicePayment invoicePayment,Invoice invoice,long cashBookTransactionCategoryId,string code)
        {
            var cashBookTransaction = new CashBookTransaction {
                Action = CASHBOOKTRANSACTIONACTION.INCOME,
               Money= invoicePayment.Total,
               Created = DateTime.Now,
               InvoiceId = invoicePayment.InvoiceId,
               CustomerId = invoice.CustomerId,
               PaymentMethodId = invoicePayment.InvoiceMethodId,
               CashBookTransactionCategoryId = cashBookTransactionCategoryId,
               SalonId = invoice.SalonId,
               SalonBranchId = invoice.SalonBranchId,
               Code = code
            };
            return cashBookTransaction;
        }
        private List<StaffProductCommissionTransaction> CreateStaffProductCommissionTransactions(List<CommissionArrangement> commissionArrangements,long salonId,long branchId )
        {
            commissionArrangements = commissionArrangements.Where(e => e.SaleStaffId != default).ToList();
            var productCommision = _salon_hairContext.CommissionProduct
                .Where(e => e.SalonBranchId == branchId)
                .Where(e => commissionArrangements.Select(x => x.ObjectId).Contains(e.ProductId)).ToList();
            var listStaffProductCommisionTransaction = new List<StaffProductCommissionTransaction>();
            foreach (var item in commissionArrangements)
            {
                listStaffProductCommisionTransaction.Add(new StaffProductCommissionTransaction {
                    SalonId = salonId,
                    SalonBranchId = branchId,
                    ProductId = item.ObjectId,
                    StaffId = item.SaleStaffId,
                    //checkSetting AD
                    CommissionValue = GetCommisionValueProduct(productCommision, item.ObjectPrice, item.ObjectPriceDiscount.Value, item.SaleStaffId.Value, item.ObjectId.Value)
                });
            }            
            return listStaffProductCommisionTransaction;
        }      
        private List<StaffPackageCommissionTransaction> CreateStaffPackageCommissionTransactions(List<CommissionArrangement> commissionArrangements, long salonId, long branchId)
        {
            commissionArrangements = commissionArrangements.Where(e => e.SaleStaffId != default).ToList();
            var listStaffProductCommisionTransaction = new List<StaffPackageCommissionTransaction>();
            var packageCommision = _salon_hairContext.CommissionPackage
             .Where(e => e.SalonBranchId == branchId)
             .Where(e => commissionArrangements.Select(x => x.ObjectId).Contains(e.PackageId)).ToList();
            foreach (var item in commissionArrangements)
            {
                listStaffProductCommisionTransaction.Add(new StaffPackageCommissionTransaction
                {
                    SalonId = salonId,
                    SalonBranchId = branchId,
                    PackageId = item.ObjectId,
                    StaffId = item.SaleStaffId,
                    CommissionValue = GetCommisionValuePackage(packageCommision, item.ObjectPrice, item.ObjectPriceDiscount.Value, item.SaleStaffId.Value, item.ObjectId.Value)
                });
            }
            return listStaffProductCommisionTransaction;
        }
        private List<StaffServiceCommissionTransaction> CreateStaffServiceCommissionTransaction(List<CommissionArrangement> commissionArrangements, long salonId, long branchId)
        {
            commissionArrangements = commissionArrangements.Where(e => e.SaleStaffId != default).ToList();
            var listStaffServiceCommissionTransaction = new List<StaffServiceCommissionTransaction>();
            var packageCommision = _salon_hairContext.CommissionService
             .Where(e => e.SalonBranchId == branchId)
             .Where(e => commissionArrangements.Select(x => x.ObjectId).Contains(e.ServiceId)).ToList();
            foreach (var item in commissionArrangements)
            {
                listStaffServiceCommissionTransaction.Add(new StaffServiceCommissionTransaction
                {
                    SalonId = salonId,
                    SalonBranchId = branchId,
                    ServiceId = item.ObjectId,
                    StaffId = item.SaleStaffId,
                    CommissionValue = GetCommisionValueService(packageCommision,item.ObjectPrice,item.ObjectPriceDiscount.Value,item.SaleStaffId.Value,item.ObjectId.Value)
                });
            }
           
            return listStaffServiceCommissionTransaction;
        }
        private List<StaffServiceCommissionTransaction> CreateStaffServiceServiceCommissionTransaction(List<CommissionArrangement> commissionArrangements, long salonId, long branchId)
        {
            var listStaffServiceCommissionTransaction = new List<StaffServiceCommissionTransaction>();
               var packageCommision = _salon_hairContext.CommissionService
             .Where(e => e.SalonBranchId == branchId)
             .Where(e => commissionArrangements.Select(x => x.ObjectId).Contains(e.ServiceId)).ToList();
            foreach (var item in commissionArrangements)
            {
                listStaffServiceCommissionTransaction.Add(new StaffServiceCommissionTransaction
                {
                    SalonId = salonId,
                    SalonBranchId = branchId,
                    ServiceId = item.ObjectId,
                    StaffId = item.ServiceStaffId,
                    CommissionServiceValue = GetCommisionValueServiceSerivce(packageCommision, item.ObjectPrice,item.ObjectPriceDiscount.Value,item.ServiceStaffId.Value,item.ObjectId.Value)
                });
            }

            return listStaffServiceCommissionTransaction;
        }
        private CashBookTransactionDetail CreateCashBookTransactionDetails(long? salonId, long? branchId,string description,decimal money,long? staffId)
        {
            return new CashBookTransactionDetail {
                Money = money,
                SalonBranchId = branchId.Value,
                SalonId = salonId.Value,
                Description = description,
                StaffId = staffId
            };
        }
        private decimal GetCommisionValueProduct(List<CommissionProduct> commissionProducts, decimal price, decimal priceDiscout, long staffId, long productId)
        {
            var commission = commissionProducts
             .Where(e => e.StaffId == staffId)
             .Where(e => e.ProductId == productId).FirstOrDefault();
            //Check setting Advance
            var priceToCacalate = price;
            if (commission.CommissionUnit.Equals(DISCOUNTUNIT.MONEY))
            {
                return  commission.CommissionValue;
            }
            return
            priceToCacalate * (1 - commission.CommissionValue / 100);
        }
        private decimal GetCommisionValueService(List<CommissionService> commissionProducts, decimal price, decimal priceDiscout, long staffId, long serviceId)
        {
            var commission = _salon_hairContext.CommissionService
             .Where(e => e.StaffId == staffId)
             .Where(e => e.ServiceId == serviceId).FirstOrDefault();
            //Check setting Advance
            var priceToCacalate = price;
            if (commission.CommissionUnit.Equals(DISCOUNTUNIT.MONEY))
            {
                return  commission.CommissionValue;
            }
            return
            priceToCacalate * (1 - commission.CommissionValue / 100);
        }
        private decimal GetCommisionValuePackage(List<CommissionPackage> commissionProducts, decimal price, decimal priceDiscout, long staffId, long packageId)
        {
            var commission = _salon_hairContext.CommissionPackage
             .Where(e => e.StaffId == staffId)
             .Where(e => e.PackageId == packageId).FirstOrDefault();
            //Check setting Advance
            var priceToCacalate = price;
            if (commission.CommissionUnit.Equals(DISCOUNTUNIT.MONEY))
            {
                return  commission.CommissionValue;
            }
            return
            priceToCacalate * (1 - commission.CommissionValue / 100);
        }
        private decimal GetCommisionValueServiceSerivce(List<CommissionService> commissionProducts, decimal price, decimal priceDiscout, long staffId, long serviceId)
        {
          
            var commission = commissionProducts
             .Where(e => e.StaffId == staffId)
             .Where(e => e.ServiceId == serviceId).FirstOrDefault();
            //Check setting Advance
            var priceToCacalate = price;
            if (commission.CommissionServiceUnit.Equals(DISCOUNTUNIT.MONEY))
            {
                return  commission.CommissionServiceValue;
            }
            return
            priceToCacalate * (1 - commission.CommissionServiceValue / 100);
        }
    }
}
