

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ProductSourceService: GenericRepository<ProductSource> ,IProductSource
    {
        private salon_hairContext _salon_hairContext;
        public ProductSourceService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(ProductSource productSource)
        {
            productSource.Updated = DateTime.Now;

            base.Edit(productSource);
        }
        public async new Task<int> EditAsync(ProductSource productSource)
        {            
            productSource.Updated = DateTime.Now;         
            return await base.EditAsync(productSource);
        }
        public new async Task<int> AddAsync(ProductSource productSource)
        {
            productSource.Created = DateTime.Now;
            return await base.AddAsync(productSource);
        }
        public new void Add(ProductSource productSource)
        {
            productSource.Created = DateTime.Now;
            base.Add(productSource);
        }
        public new void Delete(ProductSource productSource)
        {
            productSource.Status = "DELETED";
            base.Edit(productSource);
        }
        public new async Task<int> DeleteAsync(ProductSource productSource)
        {
            productSource.Status = "DELETED";
            return await base.EditAsync(productSource);
        }
    }
}
    