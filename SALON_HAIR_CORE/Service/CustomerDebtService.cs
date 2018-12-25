

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CustomerDebtService: GenericRepository<CustomerDebt> ,ICustomerDebt
    {
        private salon_hairContext _salon_hairContext;
        public CustomerDebtService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CustomerDebt customerDebt)
        {
            customerDebt.Updated = DateTime.Now;

            base.Edit(customerDebt);
        }
        public async new Task<int> EditAsync(CustomerDebt customerDebt)
        {            
            customerDebt.Updated = DateTime.Now;         
            return await base.EditAsync(customerDebt);
        }
        public new async Task<int> AddAsync(CustomerDebt customerDebt)
        {
            customerDebt.Created = DateTime.Now;
            return await base.AddAsync(customerDebt);
        }
        public new void Add(CustomerDebt customerDebt)
        {
            customerDebt.Created = DateTime.Now;
            base.Add(customerDebt);
        }
        public new void Delete(CustomerDebt customerDebt)
        {
            customerDebt.Status = "DELETED";
            base.Edit(customerDebt);
        }
        public new async Task<int> DeleteAsync(CustomerDebt customerDebt)
        {
            customerDebt.Status = "DELETED";
            return await base.EditAsync(customerDebt);
        }
    }
}
    