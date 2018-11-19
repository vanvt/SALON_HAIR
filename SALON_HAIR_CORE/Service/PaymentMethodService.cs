

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class PaymentMethodService: GenericRepository<PaymentMethod> ,IPaymentMethod
    {
        private salon_hairContext _salon_hairContext;
        public PaymentMethodService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(PaymentMethod paymentMethod)
        {
            paymentMethod.Updated = DateTime.Now;

            base.Edit(paymentMethod);
        }
        public async new Task<int> EditAsync(PaymentMethod paymentMethod)
        {            
            paymentMethod.Updated = DateTime.Now;         
            return await base.EditAsync(paymentMethod);
        }
        public new async Task<int> AddAsync(PaymentMethod paymentMethod)
        {
            paymentMethod.Created = DateTime.Now;
            return await base.AddAsync(paymentMethod);
        }
        public new void Add(PaymentMethod paymentMethod)
        {
            paymentMethod.Created = DateTime.Now;
            base.Add(paymentMethod);
        }
        public new void Delete(PaymentMethod paymentMethod)
        {
            paymentMethod.Status = "DELETED";
            base.Edit(paymentMethod);
        }
        public new async Task<int> DeleteAsync(PaymentMethod paymentMethod)
        {
            paymentMethod.Status = "DELETED";
            return await base.EditAsync(paymentMethod);
        }
    }
}
    