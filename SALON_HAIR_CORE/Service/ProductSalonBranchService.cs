

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class ProductSalonBranchService: GenericRepository<ProductSalonBranch> ,IProductSalonBranch
    {
        private salon_hairContext _salon_hairContext;
        public ProductSalonBranchService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(ProductSalonBranch productSalonBranch)
        {
            productSalonBranch.Updated = DateTime.Now;

            base.Edit(productSalonBranch);
        }
        public async new Task<int> EditAsync(ProductSalonBranch productSalonBranch)
        {            
            productSalonBranch.Updated = DateTime.Now;         
            return await base.EditAsync(productSalonBranch);
        }
        public new async Task<int> AddAsync(ProductSalonBranch productSalonBranch)
        {
            productSalonBranch.Created = DateTime.Now;
            return await base.AddAsync(productSalonBranch);
        }
        public new void Add(ProductSalonBranch productSalonBranch)
        {
            productSalonBranch.Created = DateTime.Now;
            base.Add(productSalonBranch);
        }
        public new void Delete(ProductSalonBranch productSalonBranch)
        {
            productSalonBranch.Status = "DELETED";
            base.Edit(productSalonBranch);
        }
        public new async Task<int> DeleteAsync(ProductSalonBranch productSalonBranch)
        {
            productSalonBranch.Status = "DELETED";
            return await base.EditAsync(productSalonBranch);
        }
    }
}
    