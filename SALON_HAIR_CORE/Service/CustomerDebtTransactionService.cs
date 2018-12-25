

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CustomerDebtTransactionService: GenericRepository<CustomerDebtTransaction> ,ICustomerDebtTransaction
    {
        private salon_hairContext _salon_hairContext;
        public CustomerDebtTransactionService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CustomerDebtTransaction customerDebtTransaction)
        {
            customerDebtTransaction.Updated = DateTime.Now;

            base.Edit(customerDebtTransaction);
        }
        public async new Task<int> EditAsync(CustomerDebtTransaction customerDebtTransaction)
        {            
            customerDebtTransaction.Updated = DateTime.Now;         
            return await base.EditAsync(customerDebtTransaction);
        }
        public new async Task<int> AddAsync(CustomerDebtTransaction customerDebtTransaction)
        {
            customerDebtTransaction.Created = DateTime.Now;
            return await base.AddAsync(customerDebtTransaction);
        }
        public new void Add(CustomerDebtTransaction customerDebtTransaction)
        {
            customerDebtTransaction.Created = DateTime.Now;
            base.Add(customerDebtTransaction);
        }
        public new void Delete(CustomerDebtTransaction customerDebtTransaction)
        {
            customerDebtTransaction.Status = "DELETED";
            base.Edit(customerDebtTransaction);
        }
        public new async Task<int> DeleteAsync(CustomerDebtTransaction customerDebtTransaction)
        {
            customerDebtTransaction.Status = "DELETED";
            return await base.EditAsync(customerDebtTransaction);
        }
    }
}
    