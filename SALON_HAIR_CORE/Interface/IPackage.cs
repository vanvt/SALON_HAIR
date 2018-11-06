
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Interface
{
    public interface IPackage: IGenericRepository<Package>
    {
        Task<int> EditMany2ManyAsync(Package package);
        Task<int> AddMany2ManyAsync(Package package);
    }
}

