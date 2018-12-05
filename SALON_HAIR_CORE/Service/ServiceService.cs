

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace SALON_HAIR_CORE.Service
{
    public class ServiceService: GenericRepository<SALON_HAIR_ENTITY.Entities.Service> ,IService
    {
        private salon_hairContext _salon_hairContext;
        public ServiceService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(SALON_HAIR_ENTITY.Entities.Service service)
        {
            service.Updated = DateTime.Now;

            base.Edit(service);
        }
        public async new Task<int> EditAsync(SALON_HAIR_ENTITY.Entities.Service service)
        {
            service.Updated = DateTime.Now;
            return await base.EditAsync(service);
        }
        public new async Task<int> AddAsync(SALON_HAIR_ENTITY.Entities.Service service)
        {
            service.Created = DateTime.Now;
            service = AddToAllBranch(service);
            return await base.AddAsync(service);
        }
        public new void Add(SALON_HAIR_ENTITY.Entities.Service service)
        {
            service.Created = DateTime.Now;
            base.Add(service);
        }
        public new void Delete(SALON_HAIR_ENTITY.Entities.Service service)
        {
            service.Status = "DELETED";
            base.Edit(service);
        }
        public new async Task<int> DeleteAsync(SALON_HAIR_ENTITY.Entities.Service service)
        {
            service.Status = "DELETED";
            return await base.EditAsync(service);
        }

        public async Task<int> EditMany2ManyAsync(SALON_HAIR_ENTITY.Entities.Service service)
        {
            var listProduct = service.ServiceProduct.Select(e => e.ProductId);
            var listOldServiceProduct = _salon_hairContext.ServiceProduct.Where(e => e.ServiceId == service.Id).Select(e => new ServiceProduct
            {
                Created = e.Created,
                ServiceId = service.Id,
                CreatedBy = e.CreatedBy,
                ProductId = e.ProductId,
                Quota = e.Quota,
                Status = e.Status,
                Updated = e.Updated,
                UpdatedBy = e.UpdatedBy
            }).AsNoTracking();
            _salon_hairContext.ServiceProduct.RemoveRange(listOldServiceProduct);
            //await _salon_hairContext.SaveChangesAsync();

            var listnewServiceProduct = service.ServiceProduct.Select(e => new ServiceProduct
            {
                Created = e.Created,
                ServiceId = service.Id,
                CreatedBy = e.CreatedBy,
                ProductId = e.ProductId,
                Quota = e.Quota,
                Status = e.Status,
                Updated = e.Updated,
                UpdatedBy = e.UpdatedBy
            });

            _salon_hairContext.ServiceProduct.AddRange(listnewServiceProduct);
            //  await _salon_hairContext.SaveChangesAsync();
            _salon_hairContext.Entry(service).State = EntityState.Modified;
            service.Updated = DateTime.Now;
            return await _salon_hairContext.SaveChangesAsync();
        }

        public async Task<int> AddMany2ManyAsync(SALON_HAIR_ENTITY.Entities.Service service)
        {
            service.Created = DateTime.Now;
            return await base.AddAsync(service);
        }

        public SALON_HAIR_ENTITY.Entities.Service AddToAllBranch(SALON_HAIR_ENTITY.Entities.Service service)
        {
            var listBranch = _salon_hairContext.SalonBranch.Where(e => e.SalonId == service.SalonId).ToList();
            listBranch.ForEach(e =>
            {
                service.ServiceSalonBranch.Add(new ServiceSalonBranch {SalonBranchId = e.Id });
            });
            return service;

        }
        public SALON_HAIR_ENTITY.Entities.Service AddToAllCommision(SALON_HAIR_ENTITY.Entities.Service service)
        {
            throw new NotImplementedException();
        }

        //public SALON_HAIR_ENTITY.Entities.Service AddToAllCommision()
        //{
        //    var listBranch = _salon_hairContext.SalonBranch.Where(e => e.SalonId == service.SalonId).ToList();
        //    listBranch.ForEach(e =>
        //    {
        //        service.ServiceSalonBranch.Add(new ServiceSalonBranch { SalonBranchId = e.SalonId.Value });
        //    });
        //    return service;
        //}

    }
}