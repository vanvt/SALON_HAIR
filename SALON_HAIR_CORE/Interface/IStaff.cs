
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Interface
{
    public interface IStaff: IGenericRepository<Staff>
    {
        Task<int> EditMany2ManyAsync(Staff staff);

      
    }
}

