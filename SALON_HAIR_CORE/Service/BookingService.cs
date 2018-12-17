

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SALON_HAIR_CORE.Service
{
    public class BookingService: GenericRepository<Booking> ,IBooking
    {
        private salon_hairContext _salon_hairContext;
        public BookingService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(Booking booking)
        {
            booking.Updated = DateTime.Now;

            base.Edit(booking);
        }
        public async new Task<int> EditAsync(Booking booking)
        {            
            booking.Updated = DateTime.Now;         
            return await base.EditAsync(booking);
        }
        public new async Task<int> AddAsync(Booking booking)
        {
            booking.Created = DateTime.Now;

            return await base.AddAsync(booking);
        }
        public new void Add(Booking booking)
        {
            booking.Created = DateTime.Now;
            base.Add(booking);
        }
        public new void Delete(Booking booking)
        {
            booking.Status = "DELETED";
            base.Edit(booking);
        }
        public new async Task<int> DeleteAsync(Booking booking)
        {
            booking.Status = "DELETED";
            return await base.EditAsync(booking);
        }
        public async Task EditAsyncOnetoManyAsync(Booking booking)
        {
            booking.BookingDetail.ToList().ForEach(e => {
                e.BookingDetailService.ToList().ForEach(a => {
                    a.Service = null;
                });
            });
            if (booking.Customer != null)
            {
                if (booking.CustomerId == booking.Customer.Id)
                {
                    booking.Customer = null;
                }
                else if (booking.Customer.Id == default)
                {
                    //Add new customer while create booking.
                    booking.Customer.CreatedBy = booking.CreatedBy;
                    booking.Customer.Created = DateTime.Now;
                    booking.Customer.SoucreCustomerId = booking.SourceChannelId;
                    booking.Customer.ChannelCustomerId = booking.SourceChannelId;
                }
            }else
            {
                booking.Customer = null;
            }
            //check BookingCustomer
            //deleted all old BookingCustomer
            var listNeedDelete = _salon_hairContext.BookingDetail
                .Where(e => e.BookingId == booking.Id)
                .Where(e => !booking.BookingDetail.Select(x => x.Id).Contains(e.Id));
            _salon_hairContext.BookingDetail.RemoveRange(listNeedDelete);
            //update BookingCustomer by new Entity
            var listBookingDetailServiceEffect = _salon_hairContext.BookingDetailService.Where(e => e.BookingDetail.BookingId == booking.Id).AsNoTracking().ToList();
            var listNeedUpdate = booking.BookingDetail.Where(e=>e.Id!=default);
            foreach (var item in listNeedUpdate)
            {
                //check BookingCustomerService
                //deleted all old BookingCustomerService
                // var listBookingCustomerSeriveNeedDelete = _salon_hairContext.BookingDetailService
                //.Where(e => e.BookingDetailId == item.Id)
                //.Where(e => !item.BookingDetailService.Select(x => x.Id).Contains(e.Id));

                var listBookingCustomerSeriveNeedDelete = listBookingDetailServiceEffect
               .Where(e => e.BookingDetailId == item.Id)
               .Where(e => !item.BookingDetailService.Select(x => x.Id).Contains(e.Id));

                _salon_hairContext.BookingDetailService.RemoveRange(listBookingCustomerSeriveNeedDelete);

                ///update BookingCustomerSerive  by new Entity
                var listBookingCustomerSeriveNeedUpdate = item.BookingDetailService.Where(e => e.Id != default);
               
                _salon_hairContext.BookingDetailService.UpdateRange(listBookingCustomerSeriveNeedUpdate);
                // add new BookingCustomerSerive
                var listBookingCustomerSeriveNeedToAdd = item.BookingDetailService.Where(e => e.Id == default);
              
                _salon_hairContext.BookingDetailService.AddRange(listBookingCustomerSeriveNeedToAdd);
            }

            _salon_hairContext.BookingDetail.UpdateRange(listNeedUpdate);
            // add new BookingCustomer
            var listNeedToAdd =  booking.BookingDetail.Where(e => e.Id == default);
            _salon_hairContext.BookingDetail.AddRange(listNeedToAdd);
            _salon_hairContext.Booking.Update(booking);
           await _salon_hairContext.SaveChangesAsync();
        }
        public async Task AddRemoveNoNeedAsync(Booking booking)
        {
            if (booking.Customer != null)
            {
                if (booking.CustomerId == booking.Customer.Id)
                {
                    booking.Customer = null;
                }
                else if (booking.Customer.Id== default)
                {
                    //Add new customer while create booking.
                    booking.Customer.CreatedBy = booking.CreatedBy;
                    booking.Customer.Created = DateTime.Now;
                    booking.Customer.SoucreCustomerId = booking.SourceChannelId;
                    booking.Customer.ChannelCustomerId = booking.SourceChannelId;
                }
            }
            booking.BookingDetail.ToList().ForEach(e => {
                e.BookingDetailService.ToList().ForEach(a => {
                    a.Service = null;
                });
            });          
            booking.Created = DateTime.Now;
            
             await base.AddAsync(booking);
        }

        public async Task EditAsyncCheckinAsync(Booking booking)
        {
            //Create new invoice


            var indexObject = _salon_hairContext.SysObjectAutoIncreament.Where(e => e.SpaId == booking.SalonId && e.ObjectName.Equals(nameof(Invoice))).FirstOrDefault();

            if (indexObject == null)
            {
                indexObject = new SysObjectAutoIncreament
                {
                    SpaId = booking.SalonId,
                    ObjectIndex = 1,
                    ObjectName = nameof(Invoice)
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


            Invoice invoice = new Invoice
            {
                BookingId = booking.Id,
                CustomerId = booking.CustomerId,
                SalonBranchId = booking.SalonBranchId,
                SalonId = booking.SalonId,
                Code = "ES" + indexObject.ObjectIndex.ToString("000000")
            };
            var listServiceBooking = new List<InvoiceDetail>();
         
            //Get package selected from booking
            if (booking.SelectedPackageId != default)
            {
                var package = _salon_hairContext.Package.Find(booking.SelectedPackageId);
                InvoiceDetail packageSelected = new InvoiceDetail
                {
                    Created = DateTime.Now,
                    CreatedBy = booking.CreatedBy,
                    IsPaid = true,
                    ObjectId = booking.SelectedPackageId,
                    ObjectName = package.Name,
                    ObjectPrice = 0,
                    ObjectType = INVOICEOBJECTTYPE.PACKAGE,
                    Quantity = 1,

                };
                listServiceBooking.Add(packageSelected);
            }
           
           
            //Get services 
           var listService =  _salon_hairContext.BookingDetailService.Where(e => e.BookingDetail.BookingId == booking.Id)
                .AsNoTracking().GroupBy(e=>e.Service).Select(e=>new {Service = e.Key ,ServiceCount =  e.Count()});
           await listService.ForEachAsync(e => {
                listServiceBooking.Add(new InvoiceDetail
                {
                    Created = DateTime.Now,
                    CreatedBy = booking.CreatedBy,
                    IsPaid = false,
                    ObjectId = e.Service.Id,
                    ObjectName = e.Service.Name,
                    ObjectType = INVOICEOBJECTTYPE.SERVICE,
                    ObjectPrice = e.Service.Price,
                    Quantity = e.ServiceCount,
                    Total = e.Service.Price * e.ServiceCount,
                    
                });
            });
            booking.BookingStatus = BOOKINGSTATUS.CHECKIN;
            booking.Updated = DateTime.Now;
            booking.UpdatedBy = booking.CreatedBy;

            invoice.InvoiceDetail = listServiceBooking;

            _salon_hairContext.Invoice.Add(invoice);          
            _salon_hairContext.Booking.Update(booking);
         await  _salon_hairContext.SaveChangesAsync();

        }
    }
}