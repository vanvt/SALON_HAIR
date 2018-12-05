
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Interface
{
    public interface IUser: IGenericRepository<User>
    {
        Task<int> EditMany2ManyAsync(User user);
    }
}

