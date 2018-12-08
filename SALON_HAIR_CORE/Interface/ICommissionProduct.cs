
using System.Linq;
using System.Threading.Tasks;
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;

namespace SALON_HAIR_CORE.Interface
{
    public interface ICommissionProduct : IGenericRepository<CommissionProduct>
    {
        Task EditLevelGroupAsync(CommissionProduct commissionProduct,long ProductCategoryId);
        Task EditLevelBranchAsync(CommissionProduct commissionProduct);
        Task<IQueryable<CommissionProduct>> EditGetLevelGroupAsync(CommissionProduct commissionProduct, long ProductCategoryId);
        Task<IQueryable<CommissionProduct>> EditGetLevelBranchAsync(CommissionProduct commissionProduct);
    }
}

