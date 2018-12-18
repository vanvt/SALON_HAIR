
using System.Threading.Tasks;
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;

namespace SALON_HAIR_CORE.Interface
{
    public interface IBooking : IGenericRepository<Booking>
    {
        Task EditAsyncOnetoManyAsync(Booking booking);
        Task AddRemoveNoNeedAsync(Booking booking);
        Task EditAsyncCheckinAsync(Booking booking);
        Task EditAsyncCheckoutAsync(Booking booking);
    }
}

