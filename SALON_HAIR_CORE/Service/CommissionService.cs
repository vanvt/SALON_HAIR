

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace SALON_HAIR_CORE.Service
{
    public class CommissionServiceService : GenericRepository<CommissionService> , ICommissionService
    {
        private salon_hairContext _salon_hairContext;
        public CommissionServiceService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CommissionService commission)
        {
            commission.Updated = DateTime.Now;

            base.Edit(commission);
        }
        public async new Task<int> EditAsync(CommissionService commission)
        {            
            commission.Updated = DateTime.Now;         
            return await base.EditAsync(commission);
        }
        public new async Task<int> AddAsync(CommissionService commission)
        {
            commission.Created = DateTime.Now;
            return await base.AddAsync(commission);
        }
        public new void Add(CommissionService commission)
        {
            commission.Created = DateTime.Now;
            base.Add(commission);
        }
        public new void Delete(CommissionService commission)
        {
            commission.Status = "DELETED";
            base.Edit(commission);
        }
        public new async Task<int> DeleteAsync(CommissionService commission)
        {
            commission.Status = "DELETED";
            return await base.EditAsync(commission);
        }

        public async Task EditLevelGroupAsync(CommissionService commissionService, long serviceCategoryId)
        {
           
            var listCommissionProduct = _salon_hairContext.CommissionService.Where(e => e.Service.ServiceCategoryId == serviceCategoryId);
            if (commissionService.StaffId != 0)
            {
                listCommissionProduct = listCommissionProduct.Where(e => e.StaffId == commissionService.StaffId);
            }
                listCommissionProduct.ToList().ForEach(e =>
            {
                e.CommisonUnitId = commissionService.CommisonUnitId;
                e.CommisonValue = commissionService.CommisonValue;
            });
            _salon_hairContext.CommissionService.UpdateRange(listCommissionProduct);
            await _salon_hairContext.SaveChangesAsync();
        }
        //public async Task EditLevelGroupAsync(long salonBranch,long staffId,long commisonUnitId,decimal commisonValue, long serviceCategoryId)
        //{

        //    var listCommissionProduct = _salon_hairContext.CommissionService.Where(e => e.Service.ServiceCategoryId == serviceCategoryId);
        //    if (commissionService.StaffId != 0)
        //    {
        //        listCommissionProduct = listCommissionProduct.Where(e => e.StaffId == commissionService.StaffId);
        //    }
        //    listCommissionProduct.ToList().ForEach(e =>
        //    {
        //        e.CommisonUnitId = commissionService.CommisonUnitId;
        //        e.CommisonValue = commissionService.CommisonValue;
        //    });
        //    _salon_hairContext.CommissionService.UpdateRange(commissionService);
        //    await _salon_hairContext.SaveChangesAsync();
        //}
        public async Task EditLevelBranchAsync(CommissionService commissionService)
        {
            var listCommissionProduct = _salon_hairContext.CommissionService.Where(e => e.SalonBranchId == commissionService.SalonBranchId);
            if (commissionService.StaffId != 0)
            {
                listCommissionProduct = listCommissionProduct.Where(e => e.StaffId == commissionService.StaffId);
            }
            listCommissionProduct.ToList().ForEach(e =>
            {
                e.CommisonUnitId = commissionService.CommisonUnitId;
                e.CommisonValue = commissionService.CommisonValue;
            });
            _salon_hairContext.CommissionService.UpdateRange(listCommissionProduct);
            await _salon_hairContext.SaveChangesAsync();
        }
     
    }
}
    