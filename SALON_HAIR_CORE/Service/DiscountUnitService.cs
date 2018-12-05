

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class DiscountUnitService: GenericRepository<DiscountUnit> ,IDiscountUnit
    {
        private salon_hairContext _salon_hairContext;
        public DiscountUnitService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(DiscountUnit discountUnit)
        {
            discountUnit.Updated = DateTime.Now;

            base.Edit(discountUnit);
        }
        public async new Task<int> EditAsync(DiscountUnit discountUnit)
        {            
            discountUnit.Updated = DateTime.Now;         
            return await base.EditAsync(discountUnit);
        }
        public new async Task<int> AddAsync(DiscountUnit discountUnit)
        {
            discountUnit.Created = DateTime.Now;
            return await base.AddAsync(discountUnit);
        }
        public new void Add(DiscountUnit discountUnit)
        {
            discountUnit.Created = DateTime.Now;
            base.Add(discountUnit);
        }
        public new void Delete(DiscountUnit discountUnit)
        {
            discountUnit.Status = "DELETED";
            base.Edit(discountUnit);
        }
        public new async Task<int> DeleteAsync(DiscountUnit discountUnit)
        {
            discountUnit.Status = "DELETED";
            return await base.EditAsync(discountUnit);
        }
    }
}
    