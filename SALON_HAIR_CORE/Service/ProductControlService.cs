

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ProductControlService: GenericRepository<ProductControl> ,IProductControl
    {
        private salon_hairContext _salon_hairContext;
        public ProductControlService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(ProductControl productControl)
        {
            productControl.Updated = DateTime.Now;

            base.Edit(productControl);
        }
        public async new Task<int> EditAsync(ProductControl productControl)
        {            
            productControl.Updated = DateTime.Now;         
            return await base.EditAsync(productControl);
        }
        public new async Task<int> AddAsync(ProductControl productControl)
        {
            productControl.Created = DateTime.Now;
            return await base.AddAsync(productControl);
        }
        public new void Add(ProductControl productControl)
        {
            productControl.Created = DateTime.Now;
            base.Add(productControl);
        }
        public new void Delete(ProductControl productControl)
        {
            productControl.Status = "DELETED";
            base.Edit(productControl);
        }
        public new async Task<int> DeleteAsync(ProductControl productControl)
        {
            productControl.Status = "DELETED";
            return await base.EditAsync(productControl);
        }
    }
}
    