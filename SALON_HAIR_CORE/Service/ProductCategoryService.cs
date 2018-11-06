

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ProductCategoryService: GenericRepository<ProductCategory> ,IProductCategory
    {
        private salon_hairContext _salon_hairContext;
        public ProductCategoryService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(ProductCategory productCategory)
        {
            productCategory.Updated = DateTime.Now;

            base.Edit(productCategory);
        }
        public async new Task<int> EditAsync(ProductCategory productCategory)
        {            
            productCategory.Updated = DateTime.Now;         
            return await base.EditAsync(productCategory);
        }
        public new async Task<int> AddAsync(ProductCategory productCategory)
        {
            productCategory.Created = DateTime.Now;
            return await base.AddAsync(productCategory);
        }
        public new void Add(ProductCategory productCategory)
        {
            productCategory.Created = DateTime.Now;
            base.Add(productCategory);
        }
        public new void Delete(ProductCategory productCategory)
        {
            productCategory.Status = "DELETED";
            base.Edit(productCategory);
        }
        public new async Task<int> DeleteAsync(ProductCategory productCategory)
        {
            productCategory.Status = "DELETED";
            return await base.EditAsync(productCategory);
        }
    }
}
    