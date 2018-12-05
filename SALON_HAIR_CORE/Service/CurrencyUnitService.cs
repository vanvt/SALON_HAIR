

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CurrencyUnitService: GenericRepository<CurrencyUnit> ,ICurrencyUnit
    {
        private salon_hairContext _salon_hairContext;
        public CurrencyUnitService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CurrencyUnit currencyUnit)
        {
            currencyUnit.Updated = DateTime.Now;

            base.Edit(currencyUnit);
        }
        public async new Task<int> EditAsync(CurrencyUnit currencyUnit)
        {            
            currencyUnit.Updated = DateTime.Now;         
            return await base.EditAsync(currencyUnit);
        }
        public new async Task<int> AddAsync(CurrencyUnit currencyUnit)
        {
            currencyUnit.Created = DateTime.Now;
            return await base.AddAsync(currencyUnit);
        }
        public new void Add(CurrencyUnit currencyUnit)
        {
            currencyUnit.Created = DateTime.Now;
            base.Add(currencyUnit);
        }
        public new void Delete(CurrencyUnit currencyUnit)
        {
            currencyUnit.Status = "DELETED";
            base.Edit(currencyUnit);
        }
        public new async Task<int> DeleteAsync(CurrencyUnit currencyUnit)
        {
            currencyUnit.Status = "DELETED";
            return await base.EditAsync(currencyUnit);
        }
    }
}
    