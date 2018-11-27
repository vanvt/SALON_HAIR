

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CustomerChannelService: GenericRepository<CustomerChannel> ,ICustomerChannel
    {
        private salon_hairContext _salon_hairContext;
        public CustomerChannelService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CustomerChannel customerChannel)
        {
            customerChannel.Updated = DateTime.Now;

            base.Edit(customerChannel);
        }
        public async new Task<int> EditAsync(CustomerChannel customerChannel)
        {            
            customerChannel.Updated = DateTime.Now;         
            return await base.EditAsync(customerChannel);
        }
        public new async Task<int> AddAsync(CustomerChannel customerChannel)
        {
            customerChannel.Created = DateTime.Now;
            return await base.AddAsync(customerChannel);
        }
        public new void Add(CustomerChannel customerChannel)
        {
            customerChannel.Created = DateTime.Now;
            base.Add(customerChannel);
        }
        public new void Delete(CustomerChannel customerChannel)
        {
            customerChannel.Status = "DELETED";
            base.Edit(customerChannel);
        }
        public new async Task<int> DeleteAsync(CustomerChannel customerChannel)
        {
            customerChannel.Status = "DELETED";
            return await base.EditAsync(customerChannel);
        }
    }
}
    