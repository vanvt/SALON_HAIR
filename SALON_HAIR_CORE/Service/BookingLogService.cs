

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class BookingLogService: GenericRepository<BookingLog> ,IBookingLog
    {
        private salon_hairContext _salon_hairContext;
        public BookingLogService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(BookingLog bookingLog)
        {
            bookingLog.Updated = DateTime.Now;

            base.Edit(bookingLog);
        }
        public async new Task<int> EditAsync(BookingLog bookingLog)
        {            
            bookingLog.Updated = DateTime.Now;         
            return await base.EditAsync(bookingLog);
        }
        public new async Task<int> AddAsync(BookingLog bookingLog)
        {
            bookingLog.Created = DateTime.Now;
            return await base.AddAsync(bookingLog);
        }
        public new void Add(BookingLog bookingLog)
        {
            bookingLog.Created = DateTime.Now;
            base.Add(bookingLog);
        }
        public new void Delete(BookingLog bookingLog)
        {
            bookingLog.Status = "DELETED";
            base.Edit(bookingLog);
        }
        public new async Task<int> DeleteAsync(BookingLog bookingLog)
        {
            bookingLog.Status = "DELETED";
            return await base.EditAsync(bookingLog);
        }
    }
}
    