

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CommissionArrangementService: GenericRepository<CommissionArrangement> ,ICommissionArrangement
    {
        private salon_hairContext _salon_hairContext;
        public CommissionArrangementService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CommissionArrangement commissionArrangement)
        {
            commissionArrangement.Updated = DateTime.Now;

            base.Edit(commissionArrangement);
        }
        public async new Task<int> EditAsync(CommissionArrangement commissionArrangement)
        {            
            commissionArrangement.Updated = DateTime.Now;         
            return await base.EditAsync(commissionArrangement);
        }
        public new async Task<int> AddAsync(CommissionArrangement commissionArrangement)
        {
            commissionArrangement.Created = DateTime.Now;
            return await base.AddAsync(commissionArrangement);
        }
        public new void Add(CommissionArrangement commissionArrangement)
        {
            commissionArrangement.Created = DateTime.Now;
            base.Add(commissionArrangement);
        }
        public new void Delete(CommissionArrangement commissionArrangement)
        {
            commissionArrangement.Status = "DELETED";
            base.Edit(commissionArrangement);
        }
        public new async Task<int> DeleteAsync(CommissionArrangement commissionArrangement)
        {
            commissionArrangement.Status = "DELETED";
            return await base.EditAsync(commissionArrangement);
        }
    }
}
    