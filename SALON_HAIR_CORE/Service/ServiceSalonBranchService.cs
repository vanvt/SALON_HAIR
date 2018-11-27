

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ServiceSalonBranchService: GenericRepository<ServiceSalonBranch> ,IServiceSalonBranch
    {
        private salon_hairContext _salon_hairContext;
        public ServiceSalonBranchService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(ServiceSalonBranch serviceSalonBranch)
        {
            serviceSalonBranch.Updated = DateTime.Now;

            base.Edit(serviceSalonBranch);
        }
        public async new Task<int> EditAsync(ServiceSalonBranch serviceSalonBranch)
        {            
            serviceSalonBranch.Updated = DateTime.Now;         
            return await base.EditAsync(serviceSalonBranch);
        }
        public new async Task<int> AddAsync(ServiceSalonBranch serviceSalonBranch)
        {
            serviceSalonBranch.Created = DateTime.Now;
            return await base.AddAsync(serviceSalonBranch);
        }
        public new void Add(ServiceSalonBranch serviceSalonBranch)
        {
            serviceSalonBranch.Created = DateTime.Now;
            base.Add(serviceSalonBranch);
        }
        public new void Delete(ServiceSalonBranch serviceSalonBranch)
        {
            serviceSalonBranch.Status = "DELETED";
            base.Edit(serviceSalonBranch);
        }
        public new async Task<int> DeleteAsync(ServiceSalonBranch serviceSalonBranch)
        {
            serviceSalonBranch.Status = "DELETED";
            return await base.EditAsync(serviceSalonBranch);
        }
    }
}
    