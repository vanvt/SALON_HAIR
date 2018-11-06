

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class SalonBranchService: GenericRepository<SalonBranch> ,ISalonBranch
    {
        private salon_hairContext _salon_hairContext;
        public SalonBranchService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(SalonBranch salonBranch)
        {
            salonBranch.Updated = DateTime.Now;

            base.Edit(salonBranch);
        }
        public async new Task<int> EditAsync(SalonBranch salonBranch)
        {            
            salonBranch.Updated = DateTime.Now;         
            return await base.EditAsync(salonBranch);
        }
        public new async Task<int> AddAsync(SalonBranch salonBranch)
        {
            salonBranch.Created = DateTime.Now;
            return await base.AddAsync(salonBranch);
        }
        public new void Add(SalonBranch salonBranch)
        {
            salonBranch.Created = DateTime.Now;
            base.Add(salonBranch);
        }
        public new void Delete(SalonBranch salonBranch)
        {
            salonBranch.Status = "DELETED";
            base.Edit(salonBranch);
        }
        public new async Task<int> DeleteAsync(SalonBranch salonBranch)
        {
            salonBranch.Status = "DELETED";
            return await base.EditAsync(salonBranch);
        }
    }
}
    