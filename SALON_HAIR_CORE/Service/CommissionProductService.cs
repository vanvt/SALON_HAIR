

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace SALON_HAIR_CORE.Service
{
    public class CommissionProductService: GenericRepository<CommissionProduct> ,ICommissionProduct
    {
        private salon_hairContext _salon_hairContext;
        public CommissionProductService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CommissionProduct commissionProduct)
        {
            commissionProduct.Updated = DateTime.Now;

            base.Edit(commissionProduct);
        }
        public async new Task<int> EditAsync(CommissionProduct commissionProduct)
        {            
            commissionProduct.Updated = DateTime.Now;         
            return await base.EditAsync(commissionProduct);
        }
        public new async Task<int> AddAsync(CommissionProduct commissionProduct)
        {
            commissionProduct.Created = DateTime.Now;
            return await base.AddAsync(commissionProduct);
        }
        public new void Add(CommissionProduct commissionProduct)
        {
            commissionProduct.Created = DateTime.Now;
            base.Add(commissionProduct);
        }
        public new void Delete(CommissionProduct commissionProduct)
        {
            commissionProduct.Status = "DELETED";
            base.Edit(commissionProduct);
        }
        public new async Task<int> DeleteAsync(CommissionProduct commissionProduct)
        {
            commissionProduct.Status = "DELETED";
            return await base.EditAsync(commissionProduct);
        }

        public async Task EditLevelGroupAsync(CommissionProduct commissionProduct, long ProductCategoryId)
        {
            var listCommissionProduct = _salon_hairContext.CommissionProduct.Where(e => e.Product.ProductCategoryId == ProductCategoryId);
            if (commissionProduct.StaffId != 0)
            {
                listCommissionProduct = listCommissionProduct.Where(e => e.StaffId == commissionProduct.StaffId);
            }
            listCommissionProduct.ToList().ForEach(e =>
            {
                e.CommisonUnitId = commissionProduct.CommisonUnitId;
                e.CommisonValue = commissionProduct.CommisonValue;
            });
            _salon_hairContext.CommissionProduct.UpdateRange(listCommissionProduct);
          await  _salon_hairContext.SaveChangesAsync();

        }

        public async Task EditLevelBranchAsync(CommissionProduct commissionProduct)
        {
            var listCommissionProduct = _salon_hairContext.CommissionProduct.Where(e => e.SalonBranchId == commissionProduct.SalonBranchId);
            if (commissionProduct.StaffId != 0)
            {
                listCommissionProduct = listCommissionProduct.Where(e => e.StaffId == commissionProduct.StaffId);
            }
            listCommissionProduct.ToList().ForEach(e =>
            {
                e.CommisonUnitId = commissionProduct.CommisonUnitId;
                e.CommisonValue = commissionProduct.CommisonValue;
            });
            _salon_hairContext.CommissionProduct.UpdateRange(listCommissionProduct);
            await _salon_hairContext.SaveChangesAsync();
        }
    }
}
    