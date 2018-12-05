

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
    public class StaffService: GenericRepository<Staff> ,IStaff
    {
        private salon_hairContext _salon_hairContext;
        public StaffService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(Staff staff)
        {
         
            staff.Updated = DateTime.Now;

            base.Edit(staff);
        }
        public async Task<int> EditMany2ManyAsync(Staff staff)
        {
            //Remove Service
            var listOldStaffService =
                _salon_hairContext.StaffService.Where(e => e.StaffId == staff.Id).AsNoTracking().ToList();
            _salon_hairContext.StaffService.RemoveRange(listOldStaffService);
            var listnewUserSalonBranch = staff.StaffService.Select(e => new SALON_HAIR_ENTITY.Entities.StaffService
            {
              StaffId = staff.Id,
              ServiceId = e.ServiceId,
                Created = DateTime.Now,               
            });
            _salon_hairContext.StaffService.AddRange(listnewUserSalonBranch);
            //Remove Authority
            //Remove SalonBranch
            var listOldStaffSalonBranch =
                _salon_hairContext.StaffSalonBranch.Where(e => e.StaffId == staff.Id).AsNoTracking().ToList();
            _salon_hairContext.StaffSalonBranch.RemoveRange(listOldStaffSalonBranch);
            var listnewUseAuthority = staff.StaffSalonBranch.Select(e => new StaffSalonBranch
            {
              StaffId = staff.Id,
              SalonBranchId = e.SalonBranchId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            });
            _salon_hairContext.StaffSalonBranch.AddRange(listnewUseAuthority);
            _salon_hairContext.Entry(staff).State = EntityState.Modified;
            return await _salon_hairContext.SaveChangesAsync();
        }     
        public new void Add(Staff staff)
        {
            staff.Created = DateTime.Now;
            base.Add(staff);
        }
        public new void Delete(Staff staff)
        {
            staff.Status = "DELETED";
            base.Edit(staff);
        }
        public new async Task<int> DeleteAsync(Staff staff)
        {
            staff.Status = "DELETED";
            return await base.EditAsync(staff);
        }

        public async Task<int> ChangeStatusAsync(Staff oldStaff)
        {
            _salon_hairContext.Update(oldStaff);
            return await _salon_hairContext.SaveChangesAsync();
        }

     
    }
}
    