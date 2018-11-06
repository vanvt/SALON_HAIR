

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class StaffTitleService: GenericRepository<StaffTitle> ,IStaffTitle
    {
        private salon_hairContext _salon_hairContext;
        public StaffTitleService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(StaffTitle staffTitle)
        {
            staffTitle.Updated = DateTime.Now;

            base.Edit(staffTitle);
        }
        public async new Task<int> EditAsync(StaffTitle staffTitle)
        {            
            staffTitle.Updated = DateTime.Now;         
            return await base.EditAsync(staffTitle);
        }
        public new async Task<int> AddAsync(StaffTitle staffTitle)
        {
            staffTitle.Created = DateTime.Now;
            return await base.AddAsync(staffTitle);
        }
        public new void Add(StaffTitle staffTitle)
        {
            staffTitle.Created = DateTime.Now;
            base.Add(staffTitle);
        }
        public new void Delete(StaffTitle staffTitle)
        {
            staffTitle.Status = "DELETED";
            base.Edit(staffTitle);
        }
        public new async Task<int> DeleteAsync(StaffTitle staffTitle)
        {
            staffTitle.Status = "DELETED";
            return await base.EditAsync(staffTitle);
        }
    }
}
    