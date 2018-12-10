

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class BookingCustomerService: GenericRepository<BookingCustomer> ,IBookingCustomer
    {
        private salon_hairContext _salon_hairContext;
        public BookingCustomerService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(BookingCustomer bookingCustomer)
        {
            bookingCustomer.Updated = DateTime.Now;

            base.Edit(bookingCustomer);
        }
        public async new Task<int> EditAsync(BookingCustomer bookingCustomer)
        {            
            bookingCustomer.Updated = DateTime.Now;         
            return await base.EditAsync(bookingCustomer);
        }
        public new async Task<int> AddAsync(BookingCustomer bookingCustomer)
        {
            bookingCustomer.Created = DateTime.Now;
            return await base.AddAsync(bookingCustomer);
        }
        public new void Add(BookingCustomer bookingCustomer)
        {
            bookingCustomer.Created = DateTime.Now;
            base.Add(bookingCustomer);
        }
        public new void Delete(BookingCustomer bookingCustomer)
        {
            bookingCustomer.Status = "DELETED";
            base.Edit(bookingCustomer);
        }
        public new async Task<int> DeleteAsync(BookingCustomer bookingCustomer)
        {
            bookingCustomer.Status = "DELETED";
            return await base.EditAsync(bookingCustomer);
        }
    }
}
    