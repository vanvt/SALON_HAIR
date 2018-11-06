

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SALON_HAIR_CORE.Service
{
    public class CommissionService: GenericRepository<Commission> ,ICommission
    {
        private salon_hairContext _salon_hairContext;
        public CommissionService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(Commission commission)
        {
            commission.Updated = DateTime.Now;

            base.Edit(commission);
        }
        public async new Task<int> EditAsync(Commission commission)
        {
           
            var   oldCommission = await _salon_hairContext.Commission.FindAsync(commission.Id);
            oldCommission.LimitCommisionUnitId = commission.LimitCommisionUnitId.Value;
            oldCommission.LimitCommisionValue = commission.LimitCommisionValue.Value;
            oldCommission.RetailCommisionUnitId = commission.RetailCommisionUnitId.Value;
            oldCommission.RetailCommisionValue = commission.RetailCommisionValue;
            oldCommission.WholesaleCommisionUnitId = commission.WholesaleCommisionUnitId.Value;
            oldCommission.WholesaleCommisionValue = commission.WholesaleCommisionValue;
            commission.Updated = DateTime.Now;
            var listCommisionDetail = _salon_hairContext.CommissionDetail.Where(e => e.CommissionId == commission.Id);
            listCommisionDetail.ToList().ForEach(e =>
            {
                e.Updated = DateTime.Now;
                e.LimitCommisionUnitId = commission.LimitCommisionUnitId.Value ;
                e.LimitCommisionValue = commission.LimitCommisionValue.Value;             
                e.RetailCommisionUnitId = commission.RetailCommisionUnitId.Value;
                e.RetailCommisionValue = commission.RetailCommisionValue;            
                e.WholesaleCommisionUnitId = commission.WholesaleCommisionUnitId.Value;
                e.WholesaleCommisionValue = commission.WholesaleCommisionValue;
            });

            _salon_hairContext.Commission.Update(oldCommission);
            _salon_hairContext.CommissionDetail.UpdateRange(listCommisionDetail);
            return await _salon_hairContext.SaveChangesAsync();
        }
        public new async Task<int> AddAsync(Commission commission)
        {
            var limitCommisionUnitId = commission.LimitCommisionUnitId.Value;

            commission.Created = DateTime.Now;
            var listService = _salon_hairContext.Service.Where(e => e.ServiceCategoryId == commission.ServiceCategoryId)
                .Where(e => !e.Status.Equals("DELETED"));
            List<CommissionDetail> listCommisionDetail = new List<CommissionDetail>();
            listService.ToList().ForEach(e =>
            {
                listCommisionDetail.Add(new CommissionDetail
                {
                    Created = DateTime.Now,
                    CreatedBy = commission.CreatedBy,
                    LimitCommisionUnitId = commission.LimitCommisionUnitId.Value,
                    LimitCommisionValue = commission.LimitCommisionValue.Value,
                    //LimitCommisionValue = 50,
                    //LimitCommisionId = 1,
                    //LimitCommisionValue = 50,
                    RetailCommisionUnitId = commission.RetailCommisionUnitId.Value,
                    RetailCommisionValue = commission.RetailCommisionValue,
                    ServiceId = e.Id,
                    //LimitCommisionUnitId = limitCommisionUnitId,
                    WholesaleCommisionUnitId = commission.WholesaleCommisionUnitId.Value,
                    WholesaleCommisionValue = commission.WholesaleCommisionValue,
                });
            });
            var test = listCommisionDetail;
            commission.CommissionDetail = listCommisionDetail;
            await _salon_hairContext.Commission.AddAsync(commission);
            return await base.AddAsync(commission);
        }
        public new void Add(Commission commission)
        {
            commission.Created = DateTime.Now;
            base.Add(commission);
        }
        public new void Delete(Commission commission)
        {
            commission.Status = "DELETED";
            base.Edit(commission);
        }
        public new async Task<int> DeleteAsync(Commission commission)
        {
            commission.Status = "DELETED";
            return await base.EditAsync(commission);
        }
    }
}
    