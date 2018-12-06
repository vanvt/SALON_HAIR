
using System.Threading.Tasks;
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;

namespace SALON_HAIR_CORE.Interface
{
    public interface ICommissionPackage : IGenericRepository<CommissionPackage>
    {
        Task EditCustomAsync(CommissionPackage commissionPackge);
        Task EditLevelBranchAsync(CommissionPackage commissionPackge);
    }
}

