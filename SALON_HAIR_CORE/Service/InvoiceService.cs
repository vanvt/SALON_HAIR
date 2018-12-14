

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
            //Get Warehoure Transaction
            var warehouseTransactionByInvoice = WarehouseTransactionByInvoice(dataUpdate, listInvoiceDetail);
            _salon_hairContext.WarehouseTransaction.Add(warehouseTransactionByInvoice);
            _salon_hairContext.Invoice.Update(dataUpdate);
            await _salon_hairContext.SaveChangesAsync();
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
    }
}
    