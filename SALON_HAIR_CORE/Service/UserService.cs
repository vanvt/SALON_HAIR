

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public async Task<int> EditMany2ManyAsync(User user)
        {
            //Remove SalonBranch
            var listOldUserSalonBranch = 
                _salon_hairContext.UserSalonBranch.Where(e => e.UserId == user.Id).AsNoTracking().ToList();
            _salon_hairContext.UserSalonBranch.RemoveRange(listOldUserSalonBranch);
            var listnewUserSalonBranch = user.UserSalonBranch.Select(e => new UserSalonBranch
            {
                UserId = user.Id,
                SpaBranchId = e.SpaBranchId,
                Created = DateTime.Now,
                Updated = DateTime.Now
            });
            _salon_hairContext.UserSalonBranch.AddRange(listnewUserSalonBranch);
            //Remove Authority
            //Remove SalonBranch
            var listOldUseAuthority =
                _salon_hairContext.UserAuthority.Where(e => e.UserId == user.Id).AsNoTracking().ToList();
            _salon_hairContext.UserAuthority.RemoveRange(listOldUseAuthority);
            var listnewUseAuthority = user.UserAuthority.Select(e => new UserAuthority
            {
                UserId = user.Id,
                AuthorityId = e.AuthorityId,
                Created = DateTime.Now,
                Updated = DateTime.Now,                
            });
            _salon_hairContext.UserAuthority.AddRange(listnewUseAuthority);
            _salon_hairContext.Entry(user).State = EntityState.Modified;          
            return await _salon_hairContext.SaveChangesAsync();
        }
    }
}
    