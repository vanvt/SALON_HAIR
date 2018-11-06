

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

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
    }
}
    