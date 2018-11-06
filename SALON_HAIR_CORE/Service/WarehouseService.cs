

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class WarehouseService: GenericRepository<Warehouse> ,IWarehouse
    {
        private salon_hairContext _salon_hairContext;
        public WarehouseService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(Warehouse warehouse)
        {
            warehouse.Updated = DateTime.Now;

            base.Edit(warehouse);
        }
        public async new Task<int> EditAsync(Warehouse warehouse)
        {            
            warehouse.Updated = DateTime.Now;         
            return await base.EditAsync(warehouse);
        }
        public new async Task<int> AddAsync(Warehouse warehouse)
        {
            warehouse.Created = DateTime.Now;
            return await base.AddAsync(warehouse);
        }
        public new void Add(Warehouse warehouse)
        {
            warehouse.Created = DateTime.Now;
            base.Add(warehouse);
        }
        public new void Delete(Warehouse warehouse)
        {
            warehouse.Status = "DELETED";
            base.Edit(warehouse);
        }
        public new async Task<int> DeleteAsync(Warehouse warehouse)
        {
            warehouse.Status = "DELETED";
            return await base.EditAsync(warehouse);
        }
    }
}
    