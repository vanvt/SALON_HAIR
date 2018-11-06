

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ProductService: GenericRepository<Product> ,IProduct
    {
        private SALON_HAIR_ENTITY.Entities.salon_hairContext _salon_hairContext;
        public ProductService(SALON_HAIR_ENTITY.Entities.salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(Product product)
        {
            product.Updated = DateTime.Now;

            base.Edit(product);
        }
        public async new Task<int> EditAsync(Product product)
        {            
            product.Updated = DateTime.Now;         
            return await base.EditAsync(product);
        }
        public new async Task<int> AddAsync(Product product)
        {
            product.Created = DateTime.Now;
            return await base.AddAsync(product);
        }
        public new void Add(Product product)
        {
            product.Created = DateTime.Now;
            base.Add(product);
        }
        public new void Delete(Product product)
        {
            product.Status = "DELETED";
            base.Edit(product);
        }
        public new async Task<int> DeleteAsync(Product product)
        {
            product.Status = "DELETED";
            return await base.EditAsync(product);
        }
    }
}
    