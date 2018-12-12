

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;

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
            //check BookingCustomer
            //deleted all old BookingCustomer
            var listNeedDelete = _salon_hairContext.BookingDetail
                .Where(e => e.BookingId == booking.Id)
                .Where(e => !booking.BookingDetail.Select(x => x.Id).Contains(e.Id));
            _salon_hairContext.BookingDetail.RemoveRange(listNeedDelete);
            //update BookingCustomer by new Entity
            var listNeedUpdate = booking.BookingDetail.Where(e=>e.Id!=default);
            foreach (var item in listNeedUpdate)
            {
                //check BookingCustomerService
                //deleted all old BookingCustomerService
                var listBookingCustomerSeriveNeedDelete = _salon_hairContext.BookingDetailService
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
    }
}
    