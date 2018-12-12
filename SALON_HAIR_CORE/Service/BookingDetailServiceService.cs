

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class BookingDetailServiceService: GenericRepository<SALON_HAIR_ENTITY.Entities.BookingDetailService> ,IBookingDetailService
    {
        private salon_hairContext _salon_hairContext;
        public BookingDetailServiceService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(SALON_HAIR_ENTITY.Entities.BookingDetailService bookingCustomerService)
        {
            bookingCustomerService.Updated = DateTime.Now;

            base.Edit(bookingCustomerService);
        }
        public async new Task<int> EditAsync(SALON_HAIR_ENTITY.Entities.BookingDetailService bookingCustomerService)
        {            
            bookingCustomerService.Updated = DateTime.Now;         
            return await base.EditAsync(bookingCustomerService);
        }
        public new async Task<int> AddAsync(SALON_HAIR_ENTITY.Entities.BookingDetailService bookingCustomerService)
        {
            bookingCustomerService.Created = DateTime.Now;
            return await base.AddAsync(bookingCustomerService);
        }
        public new void Add(SALON_HAIR_ENTITY.Entities.BookingDetailService bookingCustomerService)
        {
            bookingCustomerService.Created = DateTime.Now;
            base.Add(bookingCustomerService);
        }
        public new void Delete(SALON_HAIR_ENTITY.Entities.BookingDetailService bookingCustomerService)
        {
            bookingCustomerService.Status = "DELETED";
            base.Edit(bookingCustomerService);
        }
        public new async Task<int> DeleteAsync(SALON_HAIR_ENTITY.Entities.BookingDetailService bookingCustomerService)
        {
            bookingCustomerService.Status = "DELETED";
            return await base.EditAsync(bookingCustomerService);
        }
    }
}
    