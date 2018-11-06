

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class UserService: GenericRepository<User> ,IUser
    {
        private salon_hairContext _salon_hairContext;
        public UserService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
    
        public new void Delete(User user)
        {
            user.Status = "DELETED";
            base.Edit(user);
        }
        public new async Task<int> DeleteAsync(User user)
        {
            user.Status = "DELETED";
            return await base.EditAsync(user);
        }
    }
}
    