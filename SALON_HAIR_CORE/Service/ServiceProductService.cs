

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ServiceProductService: GenericRepository<ServiceProduct> ,IServiceProduct
    {
        private salon_hairContext _salon_hairContext;
        public ServiceProductService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(ServiceProduct serviceProduct)
        {
            serviceProduct.Updated = DateTime.Now;

            base.Edit(serviceProduct);
        }
        public async new Task<int> EditAsync(ServiceProduct serviceProduct)
        {            
            serviceProduct.Updated = DateTime.Now;         
            return await base.EditAsync(serviceProduct);
        }
        public new async Task<int> AddAsync(ServiceProduct serviceProduct)
        {
            serviceProduct.Created = DateTime.Now;
            return await base.AddAsync(serviceProduct);
        }
        public new void Add(ServiceProduct serviceProduct)
        {
            serviceProduct.Created = DateTime.Now;
            base.Add(serviceProduct);
        }
        public new void Delete(ServiceProduct serviceProduct)
        {
            serviceProduct.Status = "DELETED";
            base.Edit(serviceProduct);
        }
        public new async Task<int> DeleteAsync(ServiceProduct serviceProduct)
        {
            serviceProduct.Status = "DELETED";
            return await base.EditAsync(serviceProduct);
        }
    }
}
    