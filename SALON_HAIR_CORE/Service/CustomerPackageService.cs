

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CustomerPackageService: GenericRepository<CustomerPackage> ,ICustomerPackage
    {
        private salon_hairContext _salon_hairContext;
        public CustomerPackageService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CustomerPackage customerPackage)
        {
            customerPackage.Updated = DateTime.Now;

            base.Edit(customerPackage);
        }
        public async new Task<int> EditAsync(CustomerPackage customerPackage)
        {            
            customerPackage.Updated = DateTime.Now;         
            return await base.EditAsync(customerPackage);
        }
        public new async Task<int> AddAsync(CustomerPackage customerPackage)
        {
            customerPackage.Created = DateTime.Now;
            return await base.AddAsync(customerPackage);
        }
        public new void Add(CustomerPackage customerPackage)
        {
            customerPackage.Created = DateTime.Now;
            base.Add(customerPackage);
        }
        public new void Delete(CustomerPackage customerPackage)
        {
            customerPackage.Status = "DELETED";
            base.Edit(customerPackage);
        }
        public new async Task<int> DeleteAsync(CustomerPackage customerPackage)
        {
            customerPackage.Status = "DELETED";
            return await base.EditAsync(customerPackage);
        }
    }
}
    