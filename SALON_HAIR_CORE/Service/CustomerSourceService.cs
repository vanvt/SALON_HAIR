

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CustomerSourceService: GenericRepository<CustomerSource> ,ICustomerSource
    {
        private salon_hairContext _salon_hairContext;
        public CustomerSourceService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CustomerSource customerSource)
        {
            customerSource.Updated = DateTime.Now;

            base.Edit(customerSource);
        }
        public async new Task<int> EditAsync(CustomerSource customerSource)
        {            
            customerSource.Updated = DateTime.Now;         
            return await base.EditAsync(customerSource);
        }
        public new async Task<int> AddAsync(CustomerSource customerSource)
        {
            customerSource.Created = DateTime.Now;
            return await base.AddAsync(customerSource);
        }
        public new void Add(CustomerSource customerSource)
        {
            customerSource.Created = DateTime.Now;
            base.Add(customerSource);
        }
        public new void Delete(CustomerSource customerSource)
        {
            customerSource.Status = "DELETED";
            base.Edit(customerSource);
        }
        public new async Task<int> DeleteAsync(CustomerSource customerSource)
        {
            customerSource.Status = "DELETED";
            return await base.EditAsync(customerSource);
        }
    }
}
    