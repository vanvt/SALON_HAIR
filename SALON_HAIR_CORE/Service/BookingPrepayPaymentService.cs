

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class BookingPrepayPaymentService: GenericRepository<BookingPrepayPayment> ,IBookingPrepayPayment
    {
        private salon_hairContext _salon_hairContext;
        public BookingPrepayPaymentService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(BookingPrepayPayment bookingPrepayPayment)
        {
            bookingPrepayPayment.Updated = DateTime.Now;

            base.Edit(bookingPrepayPayment);
        }
        public async new Task<int> EditAsync(BookingPrepayPayment bookingPrepayPayment)
        {            
            bookingPrepayPayment.Updated = DateTime.Now;         
            return await base.EditAsync(bookingPrepayPayment);
        }
        public new async Task<int> AddAsync(BookingPrepayPayment bookingPrepayPayment)
        {
            bookingPrepayPayment.Created = DateTime.Now;
            return await base.AddAsync(bookingPrepayPayment);
        }
        public new void Add(BookingPrepayPayment bookingPrepayPayment)
        {
            bookingPrepayPayment.Created = DateTime.Now;
            base.Add(bookingPrepayPayment);
        }
        public new void Delete(BookingPrepayPayment bookingPrepayPayment)
        {
            bookingPrepayPayment.Status = "DELETED";
            base.Edit(bookingPrepayPayment);
        }
        public new async Task<int> DeleteAsync(BookingPrepayPayment bookingPrepayPayment)
        {
            bookingPrepayPayment.Status = "DELETED";
            return await base.EditAsync(bookingPrepayPayment);
        }
    }
}
    