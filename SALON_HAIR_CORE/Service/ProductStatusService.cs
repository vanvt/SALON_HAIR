

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ProductStatusService: GenericRepository<ProductStatus> ,IProductStatus
    {
        private salon_hairContext _salon_hairContext;
        public ProductStatusService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(ProductStatus productStatus)
        {
            productStatus.Updated = DateTime.Now;

            base.Edit(productStatus);
        }
        public async new Task<int> EditAsync(ProductStatus productStatus)
        {            
            productStatus.Updated = DateTime.Now;         
            return await base.EditAsync(productStatus);
        }
        public new async Task<int> AddAsync(ProductStatus productStatus)
        {
            productStatus.Created = DateTime.Now;
            return await base.AddAsync(productStatus);
        }
        public new void Add(ProductStatus productStatus)
        {
            productStatus.Created = DateTime.Now;
            base.Add(productStatus);
        }
        public new void Delete(ProductStatus productStatus)
        {
            productStatus.Status = "DELETED";
            base.Edit(productStatus);
        }
        public new async Task<int> DeleteAsync(ProductStatus productStatus)
        {
            productStatus.Status = "DELETED";
            return await base.EditAsync(productStatus);
        }
    }
}
    