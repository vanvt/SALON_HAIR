

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ProductCountUnitService: GenericRepository<ProductCountUnit> ,IProductCountUnit
    {
        private salon_hairContext _salon_hairContext;
        public ProductCountUnitService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(ProductCountUnit productCountUnit)
        {
            productCountUnit.Updated = DateTime.Now;

            base.Edit(productCountUnit);
        }
        public async new Task<int> EditAsync(ProductCountUnit productCountUnit)
        {            
            productCountUnit.Updated = DateTime.Now;         
            return await base.EditAsync(productCountUnit);
        }
        public new async Task<int> AddAsync(ProductCountUnit productCountUnit)
        {
            productCountUnit.Created = DateTime.Now;
            return await base.AddAsync(productCountUnit);
        }
        public new void Add(ProductCountUnit productCountUnit)
        {
            productCountUnit.Created = DateTime.Now;
            base.Add(productCountUnit);
        }
        public new void Delete(ProductCountUnit productCountUnit)
        {
            productCountUnit.Status = "DELETED";
            base.Edit(productCountUnit);
        }
        public new async Task<int> DeleteAsync(ProductCountUnit productCountUnit)
        {
            productCountUnit.Status = "DELETED";
            return await base.EditAsync(productCountUnit);
        }
    }
}
    