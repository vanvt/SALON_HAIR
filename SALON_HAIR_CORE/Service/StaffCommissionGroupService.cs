

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class StaffCommissionGroupService: GenericRepository<StaffCommisonGroup> , IStaffCommissionGroup
    {
        private salon_hairContext _salon_hairContext;
        public StaffCommissionGroupService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(StaffCommisonGroup commissionGroup)
        {
            commissionGroup.Updated = DateTime.Now;

            base.Edit(commissionGroup);
        }
        public async new Task<int> EditAsync(StaffCommisonGroup commissionGroup)
        {            
            commissionGroup.Updated = DateTime.Now;         
            return await base.EditAsync(commissionGroup);
        }
        public new async Task<int> AddAsync(StaffCommisonGroup commissionGroup)
        {
            commissionGroup.Created = DateTime.Now;
            return await base.AddAsync(commissionGroup);
        }
        public new void Add(StaffCommisonGroup commissionGroup)
        {
            commissionGroup.Created = DateTime.Now;
            base.Add(commissionGroup);
        }
        public new void Delete(StaffCommisonGroup commissionGroup)
        {
            commissionGroup.Status = "DELETED";
            base.Edit(commissionGroup);
        }
        public new async Task<int> DeleteAsync(StaffCommisonGroup commissionGroup)
        {
            commissionGroup.Status = "DELETED";
            return await base.EditAsync(commissionGroup);
        }
    }
}
    