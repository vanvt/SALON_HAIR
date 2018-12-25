

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CashBookTransactionCategoryService: GenericRepository<CashBookTransactionCategory> ,ICashBookTransactionCategory
    {
        private salon_hairContext _salon_hairContext;
        public CashBookTransactionCategoryService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CashBookTransactionCategory cashBookTransactionCategory)
        {
            cashBookTransactionCategory.Updated = DateTime.Now;

            base.Edit(cashBookTransactionCategory);
        }
        public async new Task<int> EditAsync(CashBookTransactionCategory cashBookTransactionCategory)
        {            
            cashBookTransactionCategory.Updated = DateTime.Now;         
            return await base.EditAsync(cashBookTransactionCategory);
        }
        public new async Task<int> AddAsync(CashBookTransactionCategory cashBookTransactionCategory)
        {
            cashBookTransactionCategory.Created = DateTime.Now;
            return await base.AddAsync(cashBookTransactionCategory);
        }
        public new void Add(CashBookTransactionCategory cashBookTransactionCategory)
        {
            cashBookTransactionCategory.Created = DateTime.Now;
            base.Add(cashBookTransactionCategory);
        }
        public new void Delete(CashBookTransactionCategory cashBookTransactionCategory)
        {
            cashBookTransactionCategory.Status = "DELETED";
            base.Edit(cashBookTransactionCategory);
        }
        public new async Task<int> DeleteAsync(CashBookTransactionCategory cashBookTransactionCategory)
        {
            cashBookTransactionCategory.Status = "DELETED";
            return await base.EditAsync(cashBookTransactionCategory);
        }
    }
}
    