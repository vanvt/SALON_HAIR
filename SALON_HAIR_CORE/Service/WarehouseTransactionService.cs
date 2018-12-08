

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class WarehouseTransactionService: GenericRepository<WarehouseTransaction> ,IWarehouseTransaction
    {
        private salon_hairContext _salon_hairContext;
        public WarehouseTransactionService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(WarehouseTransaction warehouseDetail)
        {
            warehouseDetail.Updated = DateTime.Now;

            base.Edit(warehouseDetail);
        }
        public async new Task<int> EditAsync(WarehouseTransaction warehouseDetail)
        {            
            warehouseDetail.Updated = DateTime.Now;         
            return await base.EditAsync(warehouseDetail);
        }
        public new async Task<int> AddAsync(WarehouseTransaction warehouseDetail)
        {
            warehouseDetail.Created = DateTime.Now;
            return await base.AddAsync(warehouseDetail);
        }
        public new void Add(WarehouseTransaction warehouseDetail)
        {
            warehouseDetail.Created = DateTime.Now;
            base.Add(warehouseDetail);
        }
        public new void Delete(WarehouseTransaction warehouseDetail)
        {
            warehouseDetail.Status = "DELETED";
            base.Edit(warehouseDetail);
        }
        public new async Task<int> DeleteAsync(WarehouseTransaction warehouseDetail)
        {
            warehouseDetail.Status = "DELETED";
            return await base.EditAsync(warehouseDetail);
        }
    }
}
    