

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CustomerDebtTransactionPaymentService: GenericRepository<CustomerDebtTransactionPayment> ,ICustomerDebtTransactionPayment
    {
        private salon_hairContext _salon_hairContext;
        public CustomerDebtTransactionPaymentService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CustomerDebtTransactionPayment customerDebtTransactionPayment)
        {
            customerDebtTransactionPayment.Updated = DateTime.Now;

            base.Edit(customerDebtTransactionPayment);
        }
        public async new Task<int> EditAsync(CustomerDebtTransactionPayment customerDebtTransactionPayment)
        {            
            customerDebtTransactionPayment.Updated = DateTime.Now;         
            return await base.EditAsync(customerDebtTransactionPayment);
        }
        public new async Task<int> AddAsync(CustomerDebtTransactionPayment customerDebtTransactionPayment)
        {
            customerDebtTransactionPayment.Created = DateTime.Now;
            return await base.AddAsync(customerDebtTransactionPayment);
        }
        public new void Add(CustomerDebtTransactionPayment customerDebtTransactionPayment)
        {
            customerDebtTransactionPayment.Created = DateTime.Now;
            base.Add(customerDebtTransactionPayment);
        }
        public new void Delete(CustomerDebtTransactionPayment customerDebtTransactionPayment)
        {
            customerDebtTransactionPayment.Status = "DELETED";
            base.Edit(customerDebtTransactionPayment);
        }
        public new async Task<int> DeleteAsync(CustomerDebtTransactionPayment customerDebtTransactionPayment)
        {
            customerDebtTransactionPayment.Status = "DELETED";
            return await base.EditAsync(customerDebtTransactionPayment);
        }
    }
}
    