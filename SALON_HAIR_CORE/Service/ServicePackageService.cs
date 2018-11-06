

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ServicePackageService: GenericRepository<ServicePackage> ,IServicePackage
    {
        private salon_hairContext _salon_hairContext;
        public ServicePackageService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(ServicePackage servicePackage)
        {
            servicePackage.Updated = DateTime.Now;

            base.Edit(servicePackage);
        }
        public async new Task<int> EditAsync(ServicePackage servicePackage)
        {            
            servicePackage.Updated = DateTime.Now;         
            return await base.EditAsync(servicePackage);
        }
        public new async Task<int> AddAsync(ServicePackage servicePackage)
        {
            servicePackage.Created = DateTime.Now;
            return await base.AddAsync(servicePackage);
        }
        public new void Add(ServicePackage servicePackage)
        {
            servicePackage.Created = DateTime.Now;
            base.Add(servicePackage);
        }
        public new void Delete(ServicePackage servicePackage)
        {
            servicePackage.Status = "DELETED";
            base.Edit(servicePackage);
        }
        public new async Task<int> DeleteAsync(ServicePackage servicePackage)
        {
            servicePackage.Status = "DELETED";
            return await base.EditAsync(servicePackage);
        }
    }
}
    