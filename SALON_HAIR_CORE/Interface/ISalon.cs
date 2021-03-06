
using System.Threading.Tasks;
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;

namespace SALON_HAIR_CORE.Interface
{
    public interface ISalon : IGenericRepository<Salon>
    {
        Task AddAsRegisterAsync(Salon salon);
    }
}

