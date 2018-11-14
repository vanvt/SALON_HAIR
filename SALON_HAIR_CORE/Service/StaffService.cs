

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
        public async  Task<int> EditMany2ManyAsync(Staff staff)
        {
            staff.Updated = DateTime.Now;
            _salon_hairContext.Attach(staff);
           // var d =   _salon_hairContext.Entry(staff);
            


            //var listService = staff.StaffService.Select(e => e.ServiceId);
            //var listOldStaffService = _salon_hairContext.StaffService.Where(e => e.StaffId == staff.Id).AsNoTracking();
            //IEnumerable<SALON_HAIR_ENTITY.Entities.StaffService> listNewStaffService;
            //if (staff.IsWorkAllService.Value)
            //{

            //}
            //else
            //{
                
            //}
            //listNewStaffService = staff.StaffService.Select(e => new SALON_HAIR_ENTITY.Entities.StaffService
            //{
            //    Created = e.Created,
            //    CreatedBy = e.CreatedBy,
            //    StaffId = staff.Id,
            //    ServiceId = e.ServiceId,
            //    Status = e.Status,               
            //    UpdatedBy = e.UpdatedBy,                
            //});
            //_salon_hairContext.StaffService.RemoveRange(listOldStaffService);

            //_salon_hairContext.StaffService.AddRange(listNewStaffService);
            staff.Updated = DateTime.Now;
         
            return await _salon_hairContext.SaveChangesAsync();          
            
        }
        public  async Task<int> AddMany2ManyAsync(Staff staff)
        {
            var list = staff.StaffService.Select(e => e.ServiceId);
            var listService = from a in list
                              select new SALON_HAIR_ENTITY.Entities.StaffService
                              {
                                  Created = DateTime.Now,
                                  CreatedBy = staff.CreatedBy,
                                  ServiceId = a,
                              };
            staff.StaffService = listService.ToList();
            staff.Created = DateTime.Now;
            return await base.AddAsync(staff);
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
    