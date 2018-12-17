

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CashBookTransactionService: GenericRepository<CashBookTransaction> ,ICashBookTransaction
    {
        private salon_hairContext _salon_hairContext;
        public CashBookTransactionService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CashBookTransaction cashBookTransaction)
        {
            cashBookTransaction.Updated = DateTime.Now;

            base.Edit(cashBookTransaction);
        }
        public async new Task<int> EditAsync(CashBookTransaction cashBookTransaction)
        {            
            cashBookTransaction.Updated = DateTime.Now;         
            return await base.EditAsync(cashBookTransaction);
        }
        public new async Task<int> AddAsync(CashBookTransaction cashBookTransaction)
        {
            cashBookTransaction.Created = DateTime.Now;
            return await base.AddAsync(cashBookTransaction);
        }
        public new void Add(CashBookTransaction cashBookTransaction)
        {
            cashBookTransaction.Created = DateTime.Now;
            base.Add(cashBookTransaction);
        }
        public new void Delete(CashBookTransaction cashBookTransaction)
        {
            cashBookTransaction.Status = "DELETED";
            base.Edit(cashBookTransaction);
        }
        public new async Task<int> DeleteAsync(CashBookTransaction cashBookTransaction)
        {
            cashBookTransaction.Status = "DELETED";
            return await base.EditAsync(cashBookTransaction);
        }
    }
}
    