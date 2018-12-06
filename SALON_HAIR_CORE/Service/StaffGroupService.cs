

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class StaffGroupService: GenericRepository<StaffGroup> ,IStaffGroup
    {
        private salon_hairContext _salon_hairContext;
        public StaffGroupService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(StaffGroup staffGroup)
        {
            staffGroup.Updated = DateTime.Now;

            base.Edit(staffGroup);
        }
        public async new Task<int> EditAsync(StaffGroup staffGroup)
        {            
            staffGroup.Updated = DateTime.Now;         
            return await base.EditAsync(staffGroup);
        }
        public new async Task<int> AddAsync(StaffGroup staffGroup)
        {
            staffGroup.Created = DateTime.Now;
            return await base.AddAsync(staffGroup);
        }
        public new void Add(StaffGroup staffGroup)
        {
            staffGroup.Created = DateTime.Now;
            base.Add(staffGroup);
        }
        public new void Delete(StaffGroup staffGroup)
        {
            staffGroup.Status = "DELETED";
            base.Edit(staffGroup);
        }
        public new async Task<int> DeleteAsync(StaffGroup staffGroup)
        {
            staffGroup.Status = "DELETED";
            return await base.EditAsync(staffGroup);
        }
    }
}
    