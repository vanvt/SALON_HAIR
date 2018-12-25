

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CashBookTransactionDetailService: GenericRepository<CashBookTransactionDetail> ,ICashBookTransactionDetail
    {
        private salon_hairContext _salon_hairContext;
        public CashBookTransactionDetailService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CashBookTransactionDetail cashBookTransactionDetail)
        {
            cashBookTransactionDetail.Updated = DateTime.Now;

            base.Edit(cashBookTransactionDetail);
        }
        public async new Task<int> EditAsync(CashBookTransactionDetail cashBookTransactionDetail)
        {            
            cashBookTransactionDetail.Updated = DateTime.Now;         
            return await base.EditAsync(cashBookTransactionDetail);
        }
        public new async Task<int> AddAsync(CashBookTransactionDetail cashBookTransactionDetail)
        {
            cashBookTransactionDetail.Created = DateTime.Now;
            return await base.AddAsync(cashBookTransactionDetail);
        }
        public new void Add(CashBookTransactionDetail cashBookTransactionDetail)
        {
            cashBookTransactionDetail.Created = DateTime.Now;
            base.Add(cashBookTransactionDetail);
        }
        public new void Delete(CashBookTransactionDetail cashBookTransactionDetail)
        {
            cashBookTransactionDetail.Status = "DELETED";
            base.Edit(cashBookTransactionDetail);
        }
        public new async Task<int> DeleteAsync(CashBookTransactionDetail cashBookTransactionDetail)
        {
            cashBookTransactionDetail.Status = "DELETED";
            return await base.EditAsync(cashBookTransactionDetail);
        }
    }
}
    