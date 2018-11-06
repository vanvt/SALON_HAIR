

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CommissionUnitService: GenericRepository<CommissionUnit> ,ICommissionUnit
    {
        private salon_hairContext _salon_hairContext;
        public CommissionUnitService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CommissionUnit commissionUnit)
        {
            commissionUnit.Updated = DateTime.Now;

            base.Edit(commissionUnit);
        }
        public async new Task<int> EditAsync(CommissionUnit commissionUnit)
        {            
            commissionUnit.Updated = DateTime.Now;         
            return await base.EditAsync(commissionUnit);
        }
        public new async Task<int> AddAsync(CommissionUnit commissionUnit)
        {
            commissionUnit.Created = DateTime.Now;
            return await base.AddAsync(commissionUnit);
        }
        public new void Add(CommissionUnit commissionUnit)
        {
            commissionUnit.Created = DateTime.Now;
            base.Add(commissionUnit);
        }
        public new void Delete(CommissionUnit commissionUnit)
        {
            commissionUnit.Status = "DELETED";
            base.Edit(commissionUnit);
        }
        public new async Task<int> DeleteAsync(CommissionUnit commissionUnit)
        {
            commissionUnit.Status = "DELETED";
            return await base.EditAsync(commissionUnit);
        }
    }
}
    