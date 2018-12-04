
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Interface
{
    public interface IService: IGenericRepository<SALON_HAIR_ENTITY.Entities.Service>
    {
        Task<int> EditMany2ManyAsync(SALON_HAIR_ENTITY.Entities.Service service);
        SALON_HAIR_ENTITY.Entities.Service AddToAllBranch(SALON_HAIR_ENTITY.Entities.Service service);
        SALON_HAIR_ENTITY.Entities.Service AddToAllCommision(SALON_HAIR_ENTITY.Entities.Service service);

    }
}

