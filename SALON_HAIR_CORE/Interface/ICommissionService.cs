
using System.Linq;
using System.Threading.Tasks;
using SALON_HAIR_CORE.Repository;
using SALON_HAIR_ENTITY.Entities;

namespace SALON_HAIR_CORE.Interface
{
    public interface ICommissionService : IGenericRepository<CommissionService>
    {
        Task EditLevelGroupAsync(CommissionService commissionService, long serviceCategoryId);
        Task EditLevelBranchAsync(CommissionService commissionService);
        //Task EditLevelServiceAsync(CommissionService  commissionService);
        Task<IQueryable<CommissionService>> EditGetLevelGroupAsync(CommissionService commissionService, long serviceCategoryId);
        Task<IQueryable<CommissionService>> EditGetLevelBranchAsync(CommissionService commissionService);
    }
}

