

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class PackageSalonBranchService: GenericRepository<PackageSalonBranch> ,IPackageSalonBranch
    {
        private salon_hairContext _salon_hairContext;
        public PackageSalonBranchService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(PackageSalonBranch packageSalonBranch)
        {
            packageSalonBranch.Updated = DateTime.Now;

            base.Edit(packageSalonBranch);
        }
        public async new Task<int> EditAsync(PackageSalonBranch packageSalonBranch)
        {            
            packageSalonBranch.Updated = DateTime.Now;         
            return await base.EditAsync(packageSalonBranch);
        }
        public new async Task<int> AddAsync(PackageSalonBranch packageSalonBranch)
        {
            packageSalonBranch.Created = DateTime.Now;
            return await base.AddAsync(packageSalonBranch);
        }
        public new void Add(PackageSalonBranch packageSalonBranch)
        {
            packageSalonBranch.Created = DateTime.Now;
            base.Add(packageSalonBranch);
        }
        public new void Delete(PackageSalonBranch packageSalonBranch)
        {
            packageSalonBranch.Status = "DELETED";
            base.Edit(packageSalonBranch);
        }
        public new async Task<int> DeleteAsync(PackageSalonBranch packageSalonBranch)
        {
            packageSalonBranch.Status = "DELETED";
            return await base.EditAsync(packageSalonBranch);
        }
    }
}
    