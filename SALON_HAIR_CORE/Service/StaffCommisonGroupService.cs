

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class StaffCommisonGroupService: GenericRepository<StaffCommisonGroup> ,IStaffCommisonGroup
    {
        private salon_hairContext _salon_hairContext;
        public StaffCommisonGroupService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(StaffCommisonGroup staffCommisonGroup)
        {
            staffCommisonGroup.Updated = DateTime.Now;

            base.Edit(staffCommisonGroup);
        }
        public async new Task<int> EditAsync(StaffCommisonGroup staffCommisonGroup)
        {            
            staffCommisonGroup.Updated = DateTime.Now;         
            return await base.EditAsync(staffCommisonGroup);
        }
        public new async Task<int> AddAsync(StaffCommisonGroup staffCommisonGroup)
        {
            staffCommisonGroup.Created = DateTime.Now;
            return await base.AddAsync(staffCommisonGroup);
        }
        public new void Add(StaffCommisonGroup staffCommisonGroup)
        {
            staffCommisonGroup.Created = DateTime.Now;
            base.Add(staffCommisonGroup);
        }
        public new void Delete(StaffCommisonGroup staffCommisonGroup)
        {
            staffCommisonGroup.Status = "DELETED";
            base.Edit(staffCommisonGroup);
        }
        public new async Task<int> DeleteAsync(StaffCommisonGroup staffCommisonGroup)
        {
            staffCommisonGroup.Status = "DELETED";
            return await base.EditAsync(staffCommisonGroup);
        }
    }
}
    