

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class SalonService: GenericRepository<Salon> ,ISalon
    {
        private salon_hairContext _salon_hairContext;
        public SalonService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(Salon salon)
        {
            salon.Updated = DateTime.Now;

            base.Edit(salon);
        }
        public async new Task<int> EditAsync(Salon salon)
        {            
            salon.Updated = DateTime.Now;         
            return await base.EditAsync(salon);
        }
        public new async Task<int> AddAsync(Salon salon)
        {
            salon.Created = DateTime.Now;
            return await base.AddAsync(salon);
        }
        public new void Add(Salon salon)
        {
            salon.Created = DateTime.Now;
            base.Add(salon);
        }
        public new void Delete(Salon salon)
        {
            salon.Status = "DELETED";
            base.Edit(salon);
        }
        public new async Task<int> DeleteAsync(Salon salon)
        {
            salon.Status = "DELETED";
            return await base.EditAsync(salon);
        }
    }
}
    