
using System.Threading.Tasks;
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;

namespace SALON_HAIR_CORE.Interface
{
    public interface IStaff : IGenericRepository<Staff>
    {
        Task<int> EditMany2ManyAsync(Staff staff);
        Task<int> AddMany2ManyAsync(Staff staff);
    }
}

