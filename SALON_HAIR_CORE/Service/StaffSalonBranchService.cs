

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class StaffSalonBranchService: GenericRepository<StaffSalonBranch> ,IStaffSalonBranch
    {
        private salon_hairContext _salon_hairContext;
        public StaffSalonBranchService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(StaffSalonBranch staffSalonBranch)
        {
            staffSalonBranch.Updated = DateTime.Now;

            base.Edit(staffSalonBranch);
        }
        public async new Task<int> EditAsync(StaffSalonBranch staffSalonBranch)
        {            
            staffSalonBranch.Updated = DateTime.Now;         
            return await base.EditAsync(staffSalonBranch);
        }
        public new async Task<int> AddAsync(StaffSalonBranch staffSalonBranch)
        {
            staffSalonBranch.Created = DateTime.Now;
            return await base.AddAsync(staffSalonBranch);
        }
        public new void Add(StaffSalonBranch staffSalonBranch)
        {
            staffSalonBranch.Created = DateTime.Now;
            base.Add(staffSalonBranch);
        }
        public new void Delete(StaffSalonBranch staffSalonBranch)
        {
            staffSalonBranch.Status = "DELETED";
            base.Edit(staffSalonBranch);
        }
        public new async Task<int> DeleteAsync(StaffSalonBranch staffSalonBranch)
        {
            staffSalonBranch.Status = "DELETED";
            return await base.EditAsync(staffSalonBranch);
        }
    }
}
    