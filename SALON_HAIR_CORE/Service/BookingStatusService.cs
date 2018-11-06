

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class BookingStatusService: GenericRepository<BookingStatus> ,IBookingStatus
    {
        private salon_hairContext _salon_hairContext;
        public BookingStatusService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(BookingStatus bookingStatus)
        {
            bookingStatus.Updated = DateTime.Now;

            base.Edit(bookingStatus);
        }
        public async new Task<int> EditAsync(BookingStatus bookingStatus)
        {            
            bookingStatus.Updated = DateTime.Now;         
            return await base.EditAsync(bookingStatus);
        }
        public new async Task<int> AddAsync(BookingStatus bookingStatus)
        {
            bookingStatus.Created = DateTime.Now;
            return await base.AddAsync(bookingStatus);
        }
        public new void Add(BookingStatus bookingStatus)
        {
            bookingStatus.Created = DateTime.Now;
            base.Add(bookingStatus);
        }
        public new void Delete(BookingStatus bookingStatus)
        {
            bookingStatus.Status = "DELETED";
            base.Edit(bookingStatus);
        }
        public new async Task<int> DeleteAsync(BookingStatus bookingStatus)
        {
            bookingStatus.Status = "DELETED";
            return await base.EditAsync(bookingStatus);
        }
    }
}
    