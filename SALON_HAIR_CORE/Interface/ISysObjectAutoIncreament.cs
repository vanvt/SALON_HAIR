
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Interface
{
    public interface ISysObjectAutoIncreament: IGenericRepository<SysObjectAutoIncreament>
    {
          Task<SysObjectAutoIncreament> GetCodeByObjectAsync(string objectName, long salonId);
    }
}
