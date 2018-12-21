

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
        public InvoiceService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
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
            var listInvoiceDetail = _salon_hairContext.InvoiceDetail.Where(e => e.InvoiceId == dataUpdate.Id).ToList();          
            //add transaction customer-package
            var listCustomerPackageTransaction = GetCustomerPackageTransactionsByInvoiceDetail(dataUpdate, listInvoiceDetail);
            await _salon_hairContext.CustomerPackageTransaction.AddRangeAsync(listCustomerPackageTransaction);
         //   //Get Warehoure Transaction
           var warehouseTransactionByInvoice = WarehouseTransactionByInvoice(dataUpdate, listInvoiceDetail);
           _salon_hairContext.WarehouseTransaction.Add(warehouseTransactionByInvoice);
            _salon_hairContext.Invoice.Update(dataUpdate);
            var cashBookIncome = CreateCashBookIncomeTransaction(dataUpdate, listInvoiceDetail);
            var cashBookOutcome = CreateCashBookOutcomeTransaction(dataUpdate, listInvoiceDetail);
            _salon_hairContext.CashBookTransaction.Add(cashBookIncome);
            _salon_hairContext.CashBookTransaction.Add(cashBookOutcome);
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
            var listCommision = _salon_hairContext.CommissionArrangement.Where(e => e.InvoiceId == invoice.Id).ToList().Where(e=>e.);
            //For Commision/ get ObjectPrice to cacualate the commission
           

            var listCashBookTransactionDetail = new List<CashBookTransactionDetail>();


            var listStaffProductCommissionTransaction = CreateStaffProductCommissionTransactions
                (listCommision.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.PRODUCT)).ToList());
            var listStaffPackageCommissionTransaction = CreateStaffPackageCommissionTransactions
                (listCommision.Where(e=>e.ObjectType.Equals(INVOICEOBJECTTYPE.PACKAGE)).Where(e=>e.IsPaid==false).ToList());
            var listStaffServiceCommissionTransaction = CreateStaffServiceCommissionTransaction
                (listCommision.Where(e=>e.ObjectType.Equals(INVOICEOBJECTTYPE.SERVICE)).Where(e=>e.IsPaid==false).ToList());
            var listStaffServiceServiceCommissionTransaction = CreateStaffServiceServiceCommissionTransaction
              (listCommision.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.SERVICE)).Where(e => e.IsPaid == true).ToList());

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
            listStaffPackageCommissionTransaction.ForEach(e => {
                listCashBookTransactionDetail.Add(CreateCashBookTransactionDetails(e.SalonId, e.SalonBranchId, "Pay for sale Service ", e.CommissionValue, e.StaffId));
            });
            listStaffServiceServiceCommissionTransaction.ForEach(e => {
                listCashBookTransactionDetail.Add(CreateCashBookTransactionDetails(e.SalonId, e.SalonBranchId, "Pay for do Service ", e.CommissionServiceValue, e.StaffId));
            });

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
            var listStaffProductCommissionTransaction = CreateStaffProductCommissionTransactions(invoice, invoiceDetails);
            var listStaffPackageCommissionTransaction = CreateStaffPackageCommissionTransactions(invoice, invoiceDetails);
            var listStaffServiceCommissionTransaction = CreateStaffServiceCommissionTransaction(invoice, invoiceDetails);
            var listStaffServiceServiceCommissionTransaction = CreateStaffServiceServiceCommissionTransaction(invoice, invoiceDetails);

           // _salon_hairContext.StaffProductCommissionTransaction.AddRange(listStaffProductCommissionTransaction);
            //_salon_hairContext.StaffPackageCommissionTransaction.AddRange(listStaffPackageCommissionTransaction);
            //_salon_hairContext.StaffServiceCommissionTransaction.AddRange(listStaffServiceCommissionTransaction);
            //_salon_hairContext.StaffServiceCommissionTransaction.AddRange(listStaffServiceServiceCommissionTransaction);

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



        private List<StaffProductCommissionTransaction> CreateStaffProductCommissionTransactions(Invoice invoice, List<InvoiceDetail> invoiceDetails)
        {
            var listStaffProductCommisionTransaction = new List<StaffProductCommissionTransaction>();

            var listProductIdInvoice = invoiceDetails.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.PRODUCT)).Select(e => e.ObjectId);
            var listCommisonProduct = _salon_hairContext.CommissionProduct
                .Where(e => e.StaffId == invoice.SalesmanId)
                .Where(e => e.SalonBranchId == invoice.SalonBranchId)
                .Where(e => listProductIdInvoice.Contains(e.ProductId)).ToList();

            invoiceDetails.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.PRODUCT)).ToList().ForEach(e => {
                decimal commissionValue = 0;
                var commission = listCommisonProduct
                    .Where(c => c.ProductId == e.ObjectId)
                    .Where(c => c.StaffId == invoice.SalesmanId)
                    .FirstOrDefault();
                if (commission != null)
                {
                    if (commission.CommissionUnit.Equals(DISCOUNTUNIT.MONEY))
                    {
                        commissionValue = commission.CommissionValue;
                    }
                    else
                    {
                        //PERCENT
                        //Check setting addvance
                        commissionValue = e.Total.Value * (1 - commission.CommissionValue / 100);
                    }
                }
                listStaffProductCommisionTransaction.Add(new StaffProductCommissionTransaction
                {
                    ProductId = e.ObjectId,
                    Created = DateTime.Now,
                    SalonBranchId = invoice.SalonBranchId,
                    SalonId = invoice.SalonId,
                    StaffId = invoice.SalesmanId,
                    CommissionValue = commissionValue,

                });
            });
            return listStaffProductCommisionTransaction;
        }
        private List<StaffPackageCommissionTransaction> CreateStaffPackageCommissionTransactions(Invoice invoice, List<InvoiceDetail> invoiceDetails)
        {
            var listStaffPackageCommisionTransaction = new List<StaffPackageCommissionTransaction>();

            var listPackageIdInvoice = invoiceDetails.
                Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.PACKAGE))
                .Where(e=>e.IsPaid == false)
                .Select(e => e.ObjectId);
            var listCommisonPackage = _salon_hairContext.CommissionPackage
                .Where(e => e.StaffId == invoice.SalesmanId)
                .Where(e => e.SalonBranchId == invoice.SalonBranchId)
                .Where(e => listPackageIdInvoice.Contains(e.PackageId)).ToList();
                
            invoiceDetails.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.PACKAGE)).ToList().ForEach(e => {
                decimal commissionValue = 0;
                var commission = listCommisonPackage
                    .Where(c => c.PackageId == e.ObjectId)
                    .Where(c => c.StaffId == invoice.SalesmanId)
                    .FirstOrDefault();
                if (commission != null)
                {
                    if (commission.CommissionUnit.Equals(DISCOUNTUNIT.MONEY))
                    {
                        commissionValue = commission.CommissionValue;
                    }
                    else
                    {
                        //PERCENT
                        //Check setting addvance
                        commissionValue = e.Total.Value * (1 - commission.CommissionValue / 100);
                    }
                }
                listStaffPackageCommisionTransaction.Add(new StaffPackageCommissionTransaction
                {
                    PackageId = e.ObjectId,
                    Created = DateTime.Now,
                    SalonBranchId = invoice.SalonBranchId,
                    SalonId = invoice.SalonId,
                    StaffId = invoice.SalesmanId,
                    CommissionValue = commissionValue,
                });
            });
            return listStaffPackageCommisionTransaction;
        }
        private List<StaffServiceCommissionTransaction> CreateStaffServiceCommissionTransaction(Invoice invoice, List<InvoiceDetail> invoiceDetails)
        {
            var listStaffServiceCommissionTransaction = new List<StaffServiceCommissionTransaction>();

            var listPackageIdInvoice = invoiceDetails.
                Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.SERVICE))              
                .Select(e => e.ObjectId);
            var listCommisonPackage = _salon_hairContext.CommissionService
                .Where(e => e.StaffId == invoice.SalesmanId)
                .Where(e => e.SalonBranchId == invoice.SalonBranchId)
                .Where(e => listPackageIdInvoice.Contains(e.ServiceId)).ToList();

            invoiceDetails.Where(e => e.ObjectType.Equals(INVOICEOBJECTTYPE.SERVICE)).ToList().ForEach(e => {
                decimal commissionValue = 0;
                var commission = listCommisonPackage
                    .Where(c => c.ServiceId == e.ObjectId)
                    .Where(c => c.StaffId == invoice.SalesmanId)
                    .FirstOrDefault();
                if (commission != null)
                {
                    if (commission.CommissionUnit.Equals(DISCOUNTUNIT.MONEY))
                    {
                        commissionValue = commission.CommissionValue;
                    }
                    else
                    {
                        //PERCENT
                        //Check setting addvance
                        commissionValue = e.Total.Value * (1 - commission.CommissionValue / 100);
                    }
                }
                listStaffServiceCommissionTransaction.Add(new StaffServiceCommissionTransaction
                {
                    ServiceId = e.ObjectId,
                    Created = DateTime.Now,
                    SalonBranchId = invoice.SalonBranchId,
                    SalonId = invoice.SalonId,
                    StaffId = invoice.SalesmanId,
                    CommissionValue = commissionValue,
                });
            });
            return listStaffServiceCommissionTransaction;
        }
        private List<StaffServiceCommissionTransaction> CreateStaffServiceServiceCommissionTransaction(Invoice invoice, List<InvoiceDetail> invoiceDetails)
        {
            var listStaffServiceCommissionTransaction = new List<StaffServiceCommissionTransaction>();

            var invoiceStaffArrangements = _salon_hairContext.InvoiceStaffArrangement.Where(e => e.InvoiceId == invoice.Id);
            var listServiceIdInvoice = invoiceStaffArrangements.Select(e => e.Service);
            var listStaffIdInvoice = invoiceStaffArrangements.Select(e => e.ServiceId);
            var listCommisonService = _salon_hairContext.CommissionService
                .Where(e => e.StaffId == invoice.SalesmanId)
                .Where(e => e.SalonBranchId == invoice.SalonBranchId)
                .Where(e => listServiceIdInvoice.Select(x=>x.Id).Contains(e.ServiceId)).ToList();
            invoiceStaffArrangements.Where(e => e.StaffId!=null).ToList().ForEach(e => {
                decimal commissionValue = 0;
                var commission = listCommisonService
                    .Where(c => c.ServiceId == e.ServiceId)
                    .Where(c => c.StaffId == e.StaffId)
                    .FirstOrDefault();
                if (commission != null)
                {
                    if (commission.CommissionUnit.Equals(DISCOUNTUNIT.MONEY))
                    {
                        commissionValue = commission.CommissionServiceValue;
                    }
                    else
                    {
                        var servicePrice = listServiceIdInvoice.Where(x => x.Id == e.ServiceId).FirstOrDefault();
                        if(servicePrice != null)
                        {
                            commissionValue = servicePrice.Price* (1 - commission.CommissionServiceValue / 100);
                        }
                    
                    }
                }
                listStaffServiceCommissionTransaction.Add(new StaffServiceCommissionTransaction
                {
                    ServiceId = e.ServiceId,
                    Created = DateTime.Now,
                    SalonBranchId = invoice.SalonBranchId,
                    SalonId = invoice.SalonId,
                    StaffId = invoice.SalesmanId,
                    CommissionServiceValue = commissionValue,
                });
            });
            return listStaffServiceCommissionTransaction;
        }


        private List<StaffProductCommissionTransaction> CreateStaffProductCommissionTransactions(List<CommissionArrangement> commissionArrangements)
        {
            var listStaffProductCommisionTransaction = new List<StaffProductCommissionTransaction>();
            foreach (var item in commissionArrangements)
            {
                listStaffProductCommisionTransaction.Add(new StaffProductCommissionTransaction {
                    SalonId = item.SalonId,
                 
                    SalonBranchId = item.SalonBranchId,
                    ProductId = item.ObjectId,
                    StaffId = item.SaleStaffId,
                    CommissionValue = item.ObjectPrice
                });
            }            
            return listStaffProductCommisionTransaction;
        }
        private List<StaffPackageCommissionTransaction> CreateStaffPackageCommissionTransactions(List<CommissionArrangement> commissionArrangements)
        {
            var listStaffProductCommisionTransaction = new List<StaffPackageCommissionTransaction>();
            foreach (var item in commissionArrangements)
            {
                listStaffProductCommisionTransaction.Add(new StaffPackageCommissionTransaction
                {
                    SalonId = item.SalonId,
                    SalonBranchId = item.SalonBranchId,
                    PackageId = item.ObjectId,
                    StaffId = item.SaleStaffId,
                    CommissionValue = item.ObjectPrice
                });
            }
            return listStaffProductCommisionTransaction;
        }
        private List<StaffServiceCommissionTransaction> CreateStaffServiceCommissionTransaction(List<CommissionArrangement> commissionArrangements)
        {
            var listStaffServiceCommissionTransaction = new List<StaffServiceCommissionTransaction>();
          
            foreach (var item in commissionArrangements)
            {
                listStaffServiceCommissionTransaction.Add(new StaffServiceCommissionTransaction
                {
                    SalonId = item.SalonId,
                    SalonBranchId = item.SalonBranchId,
                    ServiceId = item.ObjectId,
                    StaffId = item.SaleStaffId,
                    CommissionValue = item.ObjectPrice
                });
            }
           
            return listStaffServiceCommissionTransaction;
        }
        private List<StaffServiceCommissionTransaction> CreateStaffServiceServiceCommissionTransaction(List<CommissionArrangement> commissionArrangements)
        {
            var listStaffServiceCommissionTransaction = new List<StaffServiceCommissionTransaction>();

            foreach (var item in commissionArrangements)
            {
                listStaffServiceCommissionTransaction.Add(new StaffServiceCommissionTransaction
                {
                    SalonId = item.SalonId,
                    SalonBranchId = item.SalonBranchId,
                    ServiceId = item.ObjectId,
                    StaffId = item.ServiceStaffId,
                    CommissionServiceValue = item.ObjectPrice
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
    }
}
    