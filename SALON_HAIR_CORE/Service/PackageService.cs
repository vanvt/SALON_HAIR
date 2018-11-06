

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SALON_HAIR_CORE.Service
{
    public class PackageService: GenericRepository<Package> ,IPackage
    {
        private salon_hairContext _salon_hairContext;
        public PackageService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(Package package)
        {
            package.Updated = DateTime.Now;

            base.Edit(package);
        }
        public async new Task<int> EditAsync(Package package)
        {
            package.Updated = DateTime.Now;
            return await base.EditAsync(package);
        }
        public new async Task<int> AddAsync(Package package)
        {
            package.Created = DateTime.Now;
            return await base.AddAsync(package);
        }
        public new void Add(Package package)
        {
            package.Created = DateTime.Now;
            base.Add(package);
        }
        public new void Delete(Package package)
        {
            package.Status = "DELETED";
            base.Edit(package);
        }
        public new async Task<int> DeleteAsync(Package package)
        {
            package.Status = "DELETED";
            return await base.EditAsync(package);
        }

        public async Task<int> EditMany2ManyAsync(Package package)
        {
            package.Updated = DateTime.Now;
            var listService = package.ServicePackage.Select(e => e.ServiceId);
            var listOldServiceProduct = _salon_hairContext.ServicePackage.Where(e => e.PackageId == package.Id).AsNoTracking();
            var listNewServiceProduct = package.ServicePackage.Select(e => new ServicePackage
            {
                Created = e.Created,
                CreatedBy = e.CreatedBy,
                PackageId = package.Id,
                ServiceId = e.ServiceId,
                Status = e.Status,
                Updated = e.Updated,
                UpdatedBy = e.UpdatedBy
            });
            _salon_hairContext.ServicePackage.RemoveRange(listOldServiceProduct);

            _salon_hairContext.ServicePackage.AddRange(listNewServiceProduct);
            package.Updated = DateTime.Now;
            _salon_hairContext.Entry(package).State = EntityState.Modified;
            return await _salon_hairContext.SaveChangesAsync();
        }

        public async Task<int> AddMany2ManyAsync(Package package)
        {
            package.Created = DateTime.Now;
            return await base.AddAsync(package);
        }
    }
}
    