

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class BookingCustomerServiceService: GenericRepository<SALON_HAIR_ENTITY.Entities.BookingCustomerService> ,IBookingCustomerService
    {
        private salon_hairContext _salon_hairContext;
        public BookingCustomerServiceService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(SALON_HAIR_ENTITY.Entities.BookingCustomerService bookingCustomerService)
        {
            bookingCustomerService.Updated = DateTime.Now;

            base.Edit(bookingCustomerService);
        }
        public async new Task<int> EditAsync(SALON_HAIR_ENTITY.Entities.BookingCustomerService bookingCustomerService)
        {            
            bookingCustomerService.Updated = DateTime.Now;         
            return await base.EditAsync(bookingCustomerService);
        }
        public new async Task<int> AddAsync(SALON_HAIR_ENTITY.Entities.BookingCustomerService bookingCustomerService)
        {
            bookingCustomerService.Created = DateTime.Now;
            return await base.AddAsync(bookingCustomerService);
        }
        public new void Add(SALON_HAIR_ENTITY.Entities.BookingCustomerService bookingCustomerService)
        {
            bookingCustomerService.Created = DateTime.Now;
            base.Add(bookingCustomerService);
        }
        public new void Delete(SALON_HAIR_ENTITY.Entities.BookingCustomerService bookingCustomerService)
        {
            bookingCustomerService.Status = "DELETED";
            base.Edit(bookingCustomerService);
        }
        public new async Task<int> DeleteAsync(SALON_HAIR_ENTITY.Entities.BookingCustomerService bookingCustomerService)
        {
            bookingCustomerService.Status = "DELETED";
            return await base.EditAsync(bookingCustomerService);
        }
    }
}
    