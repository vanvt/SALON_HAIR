

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class StaffServiceService: GenericRepository<SALON_HAIR_ENTITY.Entities.StaffService> ,IStaffService
    {
        private salon_hairContext _salon_hairContext;
        public StaffServiceService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
       
        public new async Task<int> AddAsync(SALON_HAIR_ENTITY.Entities.StaffService staffMajor)
        {
            staffMajor.Created = DateTime.Now;
            return await base.AddAsync(staffMajor);
        }
        public new void Add(SALON_HAIR_ENTITY.Entities.StaffService staffMajor)
        {
            staffMajor.Created = DateTime.Now;
            base.Add(staffMajor);
        }
        public new void Delete(SALON_HAIR_ENTITY.Entities.StaffService staffMajor)
        {
            staffMajor.Status = "DELETED";
            base.Edit(staffMajor);
        }
        public new async Task<int> DeleteAsync(SALON_HAIR_ENTITY.Entities.StaffService staffMajor)
        {
            staffMajor.Status = "DELETED";
            return await base.EditAsync(staffMajor);
        }
    }
}
    