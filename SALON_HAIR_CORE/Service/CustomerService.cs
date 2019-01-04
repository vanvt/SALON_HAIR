

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CustomerService: GenericRepository<Customer> ,ICustomer
    {
        private salon_hairContext _salon_hairContext;
        public CustomerService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(Customer customer)
        {
            customer.Updated = DateTime.Now;

            base.Edit(customer);
        }
        public async new Task<int> EditAsync(Customer customer)
        {            
            customer.Updated = DateTime.Now;         
            return await base.EditAsync(customer);
        }
        public new async Task<int> AddAsync(Customer customer)
        {
            customer.Created = DateTime.Now;
            return await base.AddAsync(customer);
        }
        public new void Add(Customer customer)
        {
            customer.Created = DateTime.Now;
            base.Add(customer);
        }
        public new void Delete(Customer customer)
        {
            customer.Status = "DELETED";
            base.Edit(customer);
        }
        public new async Task<int> DeleteAsync(Customer customer)
        {
            customer.Status = "DELETED";
            return await base.EditAsync(customer);
        }
    }
}
