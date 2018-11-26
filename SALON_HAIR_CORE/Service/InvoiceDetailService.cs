

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SALON_HAIR_CORE.Service
{
    public class InvoiceDetailService : GenericRepository<InvoiceDetail>, IInvoiceDetail
    {
        private salon_hairContext _salon_hairContext;
        public InvoiceDetailService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }
        public new void Edit(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.Updated = DateTime.Now;

            base.Edit(invoiceDetail);
        }
        public async new Task<int> EditAsync(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.Updated = DateTime.Now;
            return await base.EditAsync(invoiceDetail);
        }
        public new async Task<int> AddAsync(InvoiceDetail invoiceDetail)
        {

            invoiceDetail.Created = DateTime.Now;
            return await base.AddAsync(invoiceDetail);
        }
        public new void Add(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.Created = DateTime.Now;
            base.Add(invoiceDetail);
        }
        public new void Delete(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.Status = "DELETED";
            base.Edit(invoiceDetail);
        }
        public new async Task<int> DeleteAsync(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.Status = "DELETED";
            return await base.EditAsync(invoiceDetail);
        }

        public async Task AddAsServiceAsync(InvoiceDetail invoiceDetail)
        {
            await _salon_hairContext.InvoiceDetail.AddAsync(invoiceDetail);

            List<InvoiceStaffArrangement> InvoiceStaffArrangements = new List<InvoiceStaffArrangement>();
            for (int i = 0; i < invoiceDetail.Quantity; i++)
            {
                InvoiceStaffArrangements.Add(
                    InvoiceStaffArrangement(
                        invoiceDetail.Id, invoiceDetail.InvoiceId, invoiceDetail.ObjectId, invoiceDetail.CreatedBy));
            }
            await _salon_hairContext.InvoiceStaffArrangement.AddRangeAsync(InvoiceStaffArrangements);
            
            await _salon_hairContext.SaveChangesAsync();
        }

        public async Task AddAsPackgeAsync(InvoiceDetail invoiceDetail)
        {
            var customerID = _salon_hairContext.Invoice.Find(invoiceDetail.InvoiceId).CustomerId;
            if (customerID.HasValue)
            {
                CustomerPackage customerPackage = new CustomerPackage
                {
                    InoveId = invoiceDetail.InvoiceId,
                    Created = DateTime.Now,
                    CustomerId = customerID.Value,
                    NumberUse = invoiceDetail.Quantity,
                    PackageId = invoiceDetail.ObjectId.Value,
                   // NumberUsed = invoiceDetail.Quantity,
                };
                await _salon_hairContext.CustomerPackage.AddAsync(customerPackage);
            }

            await _salon_hairContext.InvoiceDetail.AddAsync(invoiceDetail);
            var serviceIds = _salon_hairContext.ServicePackage.Where(e => e.PackageId == invoiceDetail.ObjectId).Select(e => e.ServiceId);
            List<InvoiceStaffArrangement> InvoiceStaffArrangements = new List<InvoiceStaffArrangement>();
            await serviceIds.ForEachAsync(e => {
                for (int i = 0; i < invoiceDetail.Quantity; i++)
                {
                    InvoiceStaffArrangements.Add(
                      InvoiceStaffArrangement(
                        invoiceDetail.Id, invoiceDetail.InvoiceId, e, invoiceDetail.CreatedBy));
                }
            });
            await _salon_hairContext.InvoiceStaffArrangement.AddRangeAsync(InvoiceStaffArrangements);
            await _salon_hairContext.SaveChangesAsync();
            //await Task.WhenAll( t2, t3, t4, t5);
        }

        public string GetObjectName(InvoiceDetail invoiceDetail)
        {
            string rs;
            switch (invoiceDetail.ObjectType)
            {
                case "SERVICE":
                    rs = _salon_hairContext.Service.Find(invoiceDetail.ObjectId).Name;
                    break;
                case "PRODUCT":
                    rs = _salon_hairContext.Product.Find(invoiceDetail.ObjectId).Name;

                    break;
                case "PACKAGE":
                    rs = _salon_hairContext.Package.Find(invoiceDetail.ObjectId).Name;
                    break;
                case "EXTRA":
                    rs = invoiceDetail.ObjectName;
                    break;
                default:
                    rs = "";
                    break;
            }
            return rs;
        }

        public async Task EditAsServiceAsync(InvoiceDetail invoiceDetail)
        {
                var oldInvoiceDetail = _salon_hairContext.InvoiceDetail.Find(invoiceDetail.Id).Quantity;
            if (oldInvoiceDetail > invoiceDetail.Quantity)
            {
                var numberItemRemove = oldInvoiceDetail.Value - invoiceDetail.Quantity;
                //Remove {numberItemRemove} last InvoiceStaffArrangement
                var listInvoiceStaffArrangement = _salon_hairContext.InvoiceStaffArrangement.
                    Where(e => e.InvoiceDetailId == invoiceDetail.Id).OrderByDescending(e => e.Id).Take(numberItemRemove.Value);

                _salon_hairContext.InvoiceStaffArrangement.RemoveRange(listInvoiceStaffArrangement);
            }
            if (oldInvoiceDetail < invoiceDetail.Quantity)
            {
                var numberItemAddnew = invoiceDetail.Quantity - oldInvoiceDetail.Value;
                //Add new {numberItemRemove} InvoiceStaffArrangement
                List<InvoiceStaffArrangement> InvoiceStaffArrangements = new List<InvoiceStaffArrangement>();
                for (int i = 0; i < numberItemAddnew; i++)
                {
                    InvoiceStaffArrangements.Add(
                      InvoiceStaffArrangement(
                        invoiceDetail.Id, invoiceDetail.InvoiceId, invoiceDetail.ObjectId, invoiceDetail.CreatedBy));
                }
            }

            _salon_hairContext.InvoiceDetail.Update(invoiceDetail);
            await _salon_hairContext.SaveChangesAsync();
            //throw new NotImplementedException();
        }
        private InvoiceStaffArrangement InvoiceStaffArrangement(long? invoiceDetailId, long? invoiceId, long? serviceId, string createdBy)
        {
            return new InvoiceStaffArrangement
            {
                Created = DateTime.Now,
                InvoiceDetailId = invoiceDetailId,
                InvoiceId = invoiceId,
                ServiceId = serviceId,
                CreatedBy = createdBy
            };
        }

        public async Task EditAsPackgeAsync(InvoiceDetail invoiceDetail)
        {
            var oldInvoiceDetail = _salon_hairContext.InvoiceDetail.Find(invoiceDetail.Id).Quantity;
            if (oldInvoiceDetail > invoiceDetail.Quantity)
            {
                var numberItemRemove = oldInvoiceDetail.Value - invoiceDetail.Quantity;
                //Remove {numberItemRemove} last InvoiceStaffArrangement
                var listInvoiceStaffArrangement = _salon_hairContext.InvoiceStaffArrangement.
                    Where(e => e.InvoiceDetailId == invoiceDetail.Id).OrderByDescending(e => e.Id).Take(numberItemRemove.Value);
                _salon_hairContext.InvoiceStaffArrangement.RemoveRange(listInvoiceStaffArrangement);
            }
            if (oldInvoiceDetail < invoiceDetail.Quantity)
            {
                var numberItemAddnew = invoiceDetail.Quantity - oldInvoiceDetail.Value;
                //Add new {numberItemRemove} InvoiceStaffArrangement


                //List<InvoiceStaffArrangement> InvoiceStaffArrangements = new List<InvoiceStaffArrangement>();
                //for (int i = 0; i < invoiceDetail.Quantity; i++)
                //{
                //    InvoiceStaffArrangements.Add(
                //      InvoiceStaffArrangement(
                //        invoiceDetail.Id, invoiceDetail.InvoiceId, invoiceDetail.ObjectId, invoiceDetail.CreatedBy));
                //}
                //_salon_hairContext.InvoiceStaffArrangement.AddRange(InvoiceStaffArrangements);


                var serviceIds = _salon_hairContext.ServicePackage.Where(e => e.PackageId == invoiceDetail.ObjectId).Select(e => e.ServiceId);
                List<InvoiceStaffArrangement> InvoiceStaffArrangements = new List<InvoiceStaffArrangement>();
                await serviceIds.ForEachAsync(e => {
                    for (int i = 0; i < numberItemAddnew; i++)
                    {
                        InvoiceStaffArrangements.Add(
                          InvoiceStaffArrangement(
                            invoiceDetail.Id, invoiceDetail.InvoiceId, e, invoiceDetail.CreatedBy));
                    }
                });
                await _salon_hairContext.InvoiceStaffArrangement.AddRangeAsync(InvoiceStaffArrangements);

            }
         
            await _salon_hairContext.SaveChangesAsync();
        }

        public InvoiceDetail GetObjectDetail(InvoiceDetail invoiceDetail)
        {
           
     
            switch (invoiceDetail.ObjectType)
            {
                case "SERVICE":
                    var service = _salon_hairContext.Service.Find(invoiceDetail.ObjectId);
                    invoiceDetail.ObjectName = service.Name;
                    invoiceDetail.ObjectPrice = service.Price;
                   
                    break;
                case "PRODUCT":
                    var product = _salon_hairContext.Product.Find(invoiceDetail.ObjectId);                   
                    invoiceDetail.ObjectName = product.Name;
                    invoiceDetail.ObjectPrice = product.Price;
                    invoiceDetail.ObjectCode = product.Code;
                    break;
                case "PACKAGE":
                    var package = _salon_hairContext.Package.Find(invoiceDetail.ObjectId);
                    invoiceDetail.ObjectName = package.Name;
                    invoiceDetail.ObjectPrice = package.Price;                
                    break;
                case "EXTRA":                   
                    break;            
            }

            var total = invoiceDetail.Quantity.Value * invoiceDetail.ObjectPrice;
            var discount = invoiceDetail.DiscountUnitId == 1 ? (total * invoiceDetail.DiscountValue.Value) / 100 : invoiceDetail.DiscountValue.Value;
            invoiceDetail.Total = total - discount;
            return invoiceDetail;

        }

        public async Task EditAsServiceAsync(InvoiceDetail invoiceDetail, int? oldQuantity)
        {
        
            if (oldQuantity > invoiceDetail.Quantity)
            {
                var numberItemRemove = oldQuantity.Value - invoiceDetail.Quantity;
                //Remove {numberItemRemove} last InvoiceStaffArrangement
                var listInvoiceStaffArrangement = _salon_hairContext.InvoiceStaffArrangement.
                    Where(e => e.InvoiceDetailId == invoiceDetail.Id).OrderByDescending(e => e.Id).Take(numberItemRemove.Value);

                _salon_hairContext.InvoiceStaffArrangement.RemoveRange(listInvoiceStaffArrangement);
            }
            if (oldQuantity < invoiceDetail.Quantity)
            {
                var numberItemAddnew = invoiceDetail.Quantity - oldQuantity.Value;
                //Add new {numberItemRemove} InvoiceStaffArrangement
                List<InvoiceStaffArrangement> InvoiceStaffArrangements = new List<InvoiceStaffArrangement>();
                for (int i = 0; i < numberItemAddnew; i++)
                {
                    InvoiceStaffArrangements.Add(
                      InvoiceStaffArrangement(
                        invoiceDetail.Id, invoiceDetail.InvoiceId, invoiceDetail.ObjectId, invoiceDetail.CreatedBy));
                }
                _salon_hairContext.InvoiceStaffArrangement.AddRange(InvoiceStaffArrangements);
            }
 
            //_salon_hairContext.InvoiceDetail.Update(invoiceDetail);
            await _salon_hairContext.SaveChangesAsync();
        }
    }
}
