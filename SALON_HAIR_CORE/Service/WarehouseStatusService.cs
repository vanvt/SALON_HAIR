

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class WarehouseStatusService: GenericRepository<WarehouseStatus> ,IWarehouseStatus
    {
        private salon_hairContext _salon_hairContext;
        public WarehouseStatusService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(WarehouseStatus warehouseStatus)
        {
            warehouseStatus.Updated = DateTime.Now;

            base.Edit(warehouseStatus);
        }
        public async new Task<int> EditAsync(WarehouseStatus warehouseStatus)
        {            
            warehouseStatus.Updated = DateTime.Now;         
            return await base.EditAsync(warehouseStatus);
        }
        public new async Task<int> AddAsync(WarehouseStatus warehouseStatus)
        {
            warehouseStatus.Created = DateTime.Now;
            return await base.AddAsync(warehouseStatus);
        }
        public new void Add(WarehouseStatus warehouseStatus)
        {
            warehouseStatus.Created = DateTime.Now;
            base.Add(warehouseStatus);
        }
        public new void Delete(WarehouseStatus warehouseStatus)
        {
            warehouseStatus.Status = "DELETED";
            base.Edit(warehouseStatus);
        }
        public new async Task<int> DeleteAsync(WarehouseStatus warehouseStatus)
        {
            warehouseStatus.Status = "DELETED";
            return await base.EditAsync(warehouseStatus);
        }
    }
}
    