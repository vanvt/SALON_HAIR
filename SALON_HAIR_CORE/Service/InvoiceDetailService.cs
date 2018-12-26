

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
        public async Task AddAsServiceGenCommisionAsync(InvoiceDetail invoiceDetail)
        {
            await _salon_hairContext.InvoiceDetail.AddAsync(invoiceDetail);

            List<CommissionArrangement> commissionArrangements = new List<CommissionArrangement>();
            for (int i = 0; i < invoiceDetail.Quantity; i++)
            {
                commissionArrangements.Add(
                    InvoiceCommissionArrangement(invoiceDetail));
            }
            await _salon_hairContext.CommissionArrangement.AddRangeAsync(commissionArrangements);
            await _salon_hairContext.SaveChangesAsync();
        }
        public async Task AddAsPackgeAsync(InvoiceDetail invoiceDetail)
        {
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
        private CommissionArrangement InvoiceCommissionArrangement(InvoiceDetail invoiceDetail)
        {
            return new CommissionArrangement
            {
                Created = DateTime.Now,                            
                CreatedBy = invoiceDetail.CreatedBy,
                InvoiceId = invoiceDetail.InvoiceId.Value,
                IsPaid = invoiceDetail.IsPaid,
                ObjectCode = invoiceDetail.ObjectCode,
                ObjectId = invoiceDetail.ObjectId,
                ObjectName = invoiceDetail.ObjectName,
                ObjectPrice = invoiceDetail.ObjectPrice,
                ObjectType = invoiceDetail.ObjectType,
                SalonBranchId = invoiceDetail.SalonBranchId,
                SalonId = invoiceDetail.SalonId,
                InvoiceDetailId = invoiceDetail.Id,
                ObjectPriceDiscount = invoiceDetail.DiscountUnit.Equals(DISCOUNTUNIT.MONEY) ? (invoiceDetail.ObjectPrice - (decimal)invoiceDetail.DiscountValue)
                    : (invoiceDetail.ObjectPrice - (invoiceDetail.ObjectPrice * (decimal)invoiceDetail.DiscountValue / 100))
            };
        }
        private async Task<List<CommissionArrangement>> GetCommissionArrangementFromInvoiceDetailAsync(InvoiceDetail invoiceDetail)
        {
            List<CommissionArrangement> commissionArrangements = new List<CommissionArrangement>();
            if (invoiceDetail.ObjectType.Equals(INVOICEOBJECTTYPE.PACKAGE) && invoiceDetail.IsPaid == true)
            {
                var services = _salon_hairContext.ServicePackage.Where(e => e.PackageId == invoiceDetail.ObjectId).Include(e => e.Service);
                await services.ForEachAsync(e =>
                {
                    for (int i = 0; i < invoiceDetail.Quantity; i++)
                    {
                        var serviceCommision = InvoiceCommissionArrangement(invoiceDetail);
                        serviceCommision.ObjectName = e.Service.Name;
                        serviceCommision.ObjectId = e.ServiceId;
                        serviceCommision.ObjectType = INVOICEOBJECTTYPE.SERVICE;
                        serviceCommision.ObjectPrice = e.Service.Price;                        
                     
                        serviceCommision.SalonId = invoiceDetail.SalonId;
                        serviceCommision.SalonBranchId = invoiceDetail.SalonBranchId;
                        commissionArrangements.Add(serviceCommision);
                    }
                });
            }
            else
            {
                for (int i = 0; i < invoiceDetail.Quantity; i++)
                {
                    commissionArrangements.Add(InvoiceCommissionArrangement(invoiceDetail));

                }
            }            
            return commissionArrangements;
           
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
                case INVOICEOBJECTTYPE.SERVICE:
                    var service = _salon_hairContext.Service.Find(invoiceDetail.ObjectId);
                    invoiceDetail.ObjectName = service.Name;
                    //invoiceDetail.ObjectPrice = service.Price;
                   
                    break;
                case INVOICEOBJECTTYPE.PRODUCT:
                    var product = _salon_hairContext.Product.Find(invoiceDetail.ObjectId);                   
                    invoiceDetail.ObjectName = product.Name;
                    //invoiceDetail.ObjectPrice = product.Price;
                    invoiceDetail.ObjectCode = product.Code;
                    break;
                case INVOICEOBJECTTYPE.PACKAGE:
                    var package = _salon_hairContext.Package.Find(invoiceDetail.ObjectId);
                    invoiceDetail.ObjectName = package.Name;
                    //invoiceDetail.ObjectPrice = package.Price;                
                    break;
                case INVOICEOBJECTTYPE.EXTRA:                   
                    break;
                case INVOICEOBJECTTYPE.PACKAGE_ON_BILL:
                    //var service_packge = _salon_hairContext.Service.Find(invoiceDetail.ObjectId);
                    //invoiceDetail.ObjectName = service_packge.Name;
                    var package_on_bill = new Package
                    {
                        SalonId = invoiceDetail.SalonId.Value,
                        PackageSalonBranch = new List<PackageSalonBranch> { new PackageSalonBranch { SalonBranchId = invoiceDetail.SalonBranchId.Value } },
                        CreatedBy = invoiceDetail.CreatedBy,
                        Created = DateTime.Now,
                        Name = invoiceDetail.ObjectName,
                        ServicePackage = new List<ServicePackage> { new ServicePackage { Created = DateTime.Now,CreatedBy = invoiceDetail.CreatedBy,ServiceId = invoiceDetail.ObjectId.Value, } },
                        Price = invoiceDetail.ObjectPrice,
                        NumberOfUse = 1,
                        UsedInMonth = 1,
                        OriginalPrice = invoiceDetail.ObjectPrice,                        
                    };
                    _salon_hairContext.Package.Add(package_on_bill);
                    invoiceDetail.ObjectId = package_on_bill.Id;
                    break;

            }
            var total = invoiceDetail.Quantity.Value * invoiceDetail.ObjectPrice;
            invoiceDetail.DiscountValue = invoiceDetail.DiscountValue.HasValue ? invoiceDetail.DiscountValue.Value : 0;
            var discount = invoiceDetail.DiscountUnit.Equals("PERCENT") ? (total * invoiceDetail.DiscountValue.Value) / 100 : invoiceDetail.DiscountValue.Value;
            if (!invoiceDetail.IsPaid.Value)
            {                           
                invoiceDetail.Total = total - discount;
            }
            else
            {
                invoiceDetail.Total = 0;
            }
            invoiceDetail.TotalExcludeDiscount = total;
            invoiceDetail.TotalIncludeDiscount = total - discount;
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
        public async Task AddAsGenCommisonAsync(InvoiceDetail invoiceDetail)
        {
            await _salon_hairContext.InvoiceDetail.AddAsync(invoiceDetail);
            var commissionArrangementFromInvoiceDetail = await GetCommissionArrangementFromInvoiceDetailAsync(invoiceDetail);
            await _salon_hairContext.CommissionArrangement.AddRangeAsync(commissionArrangementFromInvoiceDetail);
            await _salon_hairContext.SaveChangesAsync();
        }
        public async Task EditAsEditCommissionAsync(InvoiceDetail invoiceDetail,int? oldQuantity)
        {
            //var oldInvoiceDetail = _salon_hairContext.InvoiceDetail.Find(invoiceDetail.Id).Quantity;
            if (oldQuantity > invoiceDetail.Quantity)
            {
                var numberItemRemove = oldQuantity.Value - invoiceDetail.Quantity;
                //Remove {numberItemRemove} last InvoiceStaffArrangement
                var listInvoiceStaffArrangement = _salon_hairContext.CommissionArrangement.
                    Where(e => e.InvoiceDetailId == invoiceDetail.Id).OrderByDescending(e => e.Id).Take(numberItemRemove.Value);

                _salon_hairContext.CommissionArrangement.RemoveRange(listInvoiceStaffArrangement);
            }
            if (oldQuantity < invoiceDetail.Quantity)
            {
                var numberItemAddnew = invoiceDetail.Quantity - oldQuantity.Value;
                //Add new {numberItemRemove} InvoiceStaffArrangement
                List<CommissionArrangement> InvoiceStaffArrangements = new List<CommissionArrangement>();
                for (int i = 0; i < numberItemAddnew; i++)
                {
                    InvoiceStaffArrangements.Add(
                      InvoiceCommissionArrangement(invoiceDetail));
                }
                _salon_hairContext.CommissionArrangement.AddRange(InvoiceStaffArrangements);
            }

            if(oldQuantity == invoiceDetail.Quantity)
            {
                var listInvoiceStaffArrangement = _salon_hairContext.CommissionArrangement.
                   Where(e => e.InvoiceDetailId == invoiceDetail.Id).ToList();
                var objectPriceDiscount = invoiceDetail.DiscountValue.Equals(DISCOUNTUNIT.MONEY) ? invoiceDetail.ObjectPrice - invoiceDetail.DiscountValue.Value :
                    invoiceDetail.ObjectPrice * (1 - invoiceDetail.DiscountValue / 100);
                listInvoiceStaffArrangement.ForEach(e => {
                    e.ObjectPriceDiscount = objectPriceDiscount;
                });
                //_salon_hairContext.CommissionArrangement.UpdateRange(listInvoiceStaffArrangement);
                invoiceDetail.CommissionArrangement = listInvoiceStaffArrangement;
            }
            _salon_hairContext.InvoiceDetail.Update(invoiceDetail);
           
            await _salon_hairContext.SaveChangesAsync();

        }
        public async Task RemoveAsEditCommissionAsync(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.Status = OBJECTSTATUS.DELETED;
            var dataNeedRemove = _salon_hairContext.CommissionArrangement.Where(e => e.InvoiceDetailId == invoiceDetail.Id);
            _salon_hairContext.CommissionArrangement.RemoveRange(dataNeedRemove);
            _salon_hairContext.InvoiceDetail.Update(invoiceDetail);
            await _salon_hairContext.SaveChangesAsync();
        }

        public Task EditAsEditCommissionAsync(InvoiceDetail invoiceDetail)
        {
            throw new NotImplementedException();
        }
    }
}
