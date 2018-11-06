

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ProductUnitService: GenericRepository<ProductUnit> ,IProductUnit
    {
        private salon_hairContext _salon_hairContext;
        public ProductUnitService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(ProductUnit productUnit)
        {
            productUnit.Updated = DateTime.Now;

            base.Edit(productUnit);
        }
        public async new Task<int> EditAsync(ProductUnit productUnit)
        {            
            productUnit.Updated = DateTime.Now;         
            return await base.EditAsync(productUnit);
        }
        public new async Task<int> AddAsync(ProductUnit productUnit)
        {
            productUnit.Created = DateTime.Now;
            return await base.AddAsync(productUnit);
        }
        public new void Add(ProductUnit productUnit)
        {
            productUnit.Created = DateTime.Now;
            base.Add(productUnit);
        }
        public new void Delete(ProductUnit productUnit)
        {
            productUnit.Status = "DELETED";
            base.Edit(productUnit);
        }
        public new async Task<int> DeleteAsync(ProductUnit productUnit)
        {
            productUnit.Status = "DELETED";
            return await base.EditAsync(productUnit);
        }
    }
}
    