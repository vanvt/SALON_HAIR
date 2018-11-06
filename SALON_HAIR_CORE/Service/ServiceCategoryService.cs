

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ServiceCategoryService: GenericRepository<ServiceCategory> ,IServiceCategory
    {
        private salon_hairContext _salon_hairContext;
        public ServiceCategoryService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(ServiceCategory serviceCategory)
        {
            serviceCategory.Updated = DateTime.Now;

            base.Edit(serviceCategory);
        }
        public async new Task<int> EditAsync(ServiceCategory serviceCategory)
        {            
            serviceCategory.Updated = DateTime.Now;         
            return await base.EditAsync(serviceCategory);
        }
        public new async Task<int> AddAsync(ServiceCategory serviceCategory)
        {
            serviceCategory.Created = DateTime.Now;
            return await base.AddAsync(serviceCategory);
        }
        public new void Add(ServiceCategory serviceCategory)
        {
            serviceCategory.Created = DateTime.Now;
            base.Add(serviceCategory);
        }
        public new void Delete(ServiceCategory serviceCategory)
        {
            serviceCategory.Status = "DELETED";
            base.Edit(serviceCategory);
        }
        public new async Task<int> DeleteAsync(ServiceCategory serviceCategory)
        {
            serviceCategory.Status = "DELETED";
            return await base.EditAsync(serviceCategory);
        }
    }
}
    