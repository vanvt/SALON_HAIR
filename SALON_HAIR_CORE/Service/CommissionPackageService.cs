

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace SALON_HAIR_CORE.Service
{
    public class CommissionPackageService: GenericRepository<CommissionPackage> , ICommissionPackage
    {
        private salon_hairContext _salon_hairContext;
        public CommissionPackageService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CommissionPackage commissionPackge)
        {
            commissionPackge.Updated = DateTime.Now;

            base.Edit(commissionPackge);
        }
        public async new Task<int> EditAsync(CommissionPackage commissionPackge)
        {            
            commissionPackge.Updated = DateTime.Now;         
            return await base.EditAsync(commissionPackge);
        }
        public new async Task<int> AddAsync(CommissionPackage commissionPackge)
        {
            commissionPackge.Created = DateTime.Now;
            return await base.AddAsync(commissionPackge);
        }
        public new void Add(CommissionPackage commissionPackge)
        {
            commissionPackge.Created = DateTime.Now;
            base.Add(commissionPackge);
        }
        public new void Delete(CommissionPackage commissionPackge)
        {
            commissionPackge.Status = "DELETED";
            base.Edit(commissionPackge);
        }
        public new async Task<int> DeleteAsync(CommissionPackage commissionPackge)
        {
            commissionPackge.Status = "DELETED";
            return await base.EditAsync(commissionPackge);
        }

        public async Task EditCustomAsync(CommissionPackage commissionPackge)
        {
             await base.EditAsync(commissionPackge);
            //Edit level package
            //Edit level Group
            //Edit leve branch
        }

        public async Task EditLevelBranchAsync(CommissionPackage commissionPackge)
        {
            var listCommissionProduct = _salon_hairContext.CommissionPackage.Where(e => e.SalonBranchId == commissionPackge.SalonBranchId);
            listCommissionProduct.ToList().ForEach(e =>
            {
                e.CommissionUnit = commissionPackge.CommissionUnit;
                e.CommissionValue = commissionPackge.CommissionValue;
            });
            _salon_hairContext.CommissionPackage.UpdateRange(listCommissionProduct);
            await _salon_hairContext.SaveChangesAsync();
        }
    }
}
    