

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class PaymentBankingService: GenericRepository<PaymentBanking> ,IPaymentBanking
    {
        private salon_hairContext _salon_hairContext;
        public PaymentBankingService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(PaymentBanking paymentBanking)
        {
            paymentBanking.Updated = DateTime.Now;

            base.Edit(paymentBanking);
        }
        public async new Task<int> EditAsync(PaymentBanking paymentBanking)
        {            
            paymentBanking.Updated = DateTime.Now;         
            return await base.EditAsync(paymentBanking);
        }
        public new async Task<int> AddAsync(PaymentBanking paymentBanking)
        {
            paymentBanking.Created = DateTime.Now;
            return await base.AddAsync(paymentBanking);
        }
        public new void Add(PaymentBanking paymentBanking)
        {
            paymentBanking.Created = DateTime.Now;
            base.Add(paymentBanking);
        }
        public new void Delete(PaymentBanking paymentBanking)
        {
            paymentBanking.Status = "DELETED";
            base.Edit(paymentBanking);
        }
        public new async Task<int> DeleteAsync(PaymentBanking paymentBanking)
        {
            paymentBanking.Status = "DELETED";
            return await base.EditAsync(paymentBanking);
        }
    }
}
    