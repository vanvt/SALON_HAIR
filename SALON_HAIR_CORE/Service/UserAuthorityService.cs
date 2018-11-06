

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class UserAuthorityService: GenericRepository<UserAuthority> ,IUserAuthority
    {
        private salon_hairContext _salon_hairContext;
        public UserAuthorityService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(UserAuthority userAuthority)
        {
            userAuthority.Updated = DateTime.Now;

            base.Edit(userAuthority);
        }
        public async new Task<int> EditAsync(UserAuthority userAuthority)
        {            
            userAuthority.Updated = DateTime.Now;         
            return await base.EditAsync(userAuthority);
        }
        public new async Task<int> AddAsync(UserAuthority userAuthority)
        {
            userAuthority.Created = DateTime.Now;
            return await base.AddAsync(userAuthority);
        }
        public new void Add(UserAuthority userAuthority)
        {
            userAuthority.Created = DateTime.Now;
            base.Add(userAuthority);
        }
       
    }
}
    