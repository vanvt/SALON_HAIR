

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class AuthorityRouterService: GenericRepository<AuthorityRouter> ,IAuthorityRouter
    {
        private salon_hairContext _salon_hairContext;
        public AuthorityRouterService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(AuthorityRouter authorityRouter)
        {
            authorityRouter.Updated = DateTime.Now;

            base.Edit(authorityRouter);
        }
        public async new Task<int> EditAsync(AuthorityRouter authorityRouter)
        {            
            authorityRouter.Updated = DateTime.Now;         
            return await base.EditAsync(authorityRouter);
        }
        public new async Task<int> AddAsync(AuthorityRouter authorityRouter)
        {
            authorityRouter.Created = DateTime.Now;
            return await base.AddAsync(authorityRouter);
        }
        public new void Add(AuthorityRouter authorityRouter)
        {
            authorityRouter.Created = DateTime.Now;
            base.Add(authorityRouter);
        }
        public new void Delete(AuthorityRouter authorityRouter)
        {
            authorityRouter.Status = "DELETED";
            base.Edit(authorityRouter);
        }
        public new async Task<int> DeleteAsync(AuthorityRouter authorityRouter)
        {
            authorityRouter.Status = "DELETED";
            return await base.EditAsync(authorityRouter);
        }
    }
}
    