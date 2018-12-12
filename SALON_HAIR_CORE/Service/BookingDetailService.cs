

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class BookingDetailService : GenericRepository<BookingDetail>, IBookingDetail
    {
        private salon_hairContext _salon_hairContext;
        public BookingDetailService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }
        public new void Edit(BookingDetail bookingDetail)
        {
            bookingDetail.Updated = DateTime.Now;

            base.Edit(bookingDetail);
        }
        public async new Task<int> EditAsync(BookingDetail bookingDetail)
        {
            bookingDetail.Updated = DateTime.Now;
            return await base.EditAsync(bookingDetail);
        }
        public new async Task<int> AddAsync(BookingDetail bookingDetail)
        {
            bookingDetail.Created = DateTime.Now;
            return await base.AddAsync(bookingDetail);
        }
        public new void Add(BookingDetail bookingDetail)
        {
            bookingDetail.Created = DateTime.Now;
            base.Add(bookingDetail);
        }
        public new void Delete(BookingDetail bookingDetail)
        {
            bookingDetail.Status = "DELETED";
            base.Edit(bookingDetail);
        }
        public new async Task<int> DeleteAsync(BookingDetail bookingDetail)
        {
            bookingDetail.Status = "DELETED";
            return await base.EditAsync(bookingDetail);
        }
    }
}
    