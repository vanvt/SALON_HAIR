

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class UserSalonBranchService: GenericRepository<UserSalonBranch> ,IUserSalonBranch
    {
        private salon_hairContext _salon_hairContext;
        public UserSalonBranchService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(UserSalonBranch userSalonBranch)
        {
            userSalonBranch.Updated = DateTime.Now;

            base.Edit(userSalonBranch);
        }
        public async new Task<int> EditAsync(UserSalonBranch userSalonBranch)
        {            
            userSalonBranch.Updated = DateTime.Now;         
            return await base.EditAsync(userSalonBranch);
        }
        public new async Task<int> AddAsync(UserSalonBranch userSalonBranch)
        {
            userSalonBranch.Created = DateTime.Now;
            return await base.AddAsync(userSalonBranch);
        }
        public new void Add(UserSalonBranch userSalonBranch)
        {
            userSalonBranch.Created = DateTime.Now;
            base.Add(userSalonBranch);
        }
      
    }
}
    