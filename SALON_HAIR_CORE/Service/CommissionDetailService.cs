

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CommissionDetailService: GenericRepository<CommissionDetail> ,ICommissionDetail
    {
        private salon_hairContext _salon_hairContext;
        public CommissionDetailService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CommissionDetail commissionDetail)
        {
            commissionDetail.Updated = DateTime.Now;

            base.Edit(commissionDetail);
        }
        public async new Task<int> EditAsync(CommissionDetail commissionDetail)
        {            
            commissionDetail.Updated = DateTime.Now;         
            return await base.EditAsync(commissionDetail);
        }
        public new async Task<int> AddAsync(CommissionDetail commissionDetail)
        {
            commissionDetail.Created = DateTime.Now;
            return await base.AddAsync(commissionDetail);
        }
        public new void Add(CommissionDetail commissionDetail)
        {
            commissionDetail.Created = DateTime.Now;
            base.Add(commissionDetail);
        }
        public new void Delete(CommissionDetail commissionDetail)
        {
            commissionDetail.Status = "DELETED";
            base.Edit(commissionDetail);
        }
        public new async Task<int> DeleteAsync(CommissionDetail commissionDetail)
        {
            commissionDetail.Status = "DELETED";
            return await base.EditAsync(commissionDetail);
        }
    }
}
    