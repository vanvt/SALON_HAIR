

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class WarehouseTransactionDetailService: GenericRepository<WarehouseTransactionDetail> ,IWarehouseTransactionDetail
    {
        private salon_hairContext _salon_hairContext;
        public WarehouseTransactionDetailService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(WarehouseTransactionDetail warehouseTransactionDetail)
        {
            warehouseTransactionDetail.Updated = DateTime.Now;

            base.Edit(warehouseTransactionDetail);
        }
        public async new Task<int> EditAsync(WarehouseTransactionDetail warehouseTransactionDetail)
        {            
            warehouseTransactionDetail.Updated = DateTime.Now;         
            return await base.EditAsync(warehouseTransactionDetail);
        }
        public new async Task<int> AddAsync(WarehouseTransactionDetail warehouseTransactionDetail)
        {
            warehouseTransactionDetail.Created = DateTime.Now;
            return await base.AddAsync(warehouseTransactionDetail);
        }
        public new void Add(WarehouseTransactionDetail warehouseTransactionDetail)
        {
            warehouseTransactionDetail.Created = DateTime.Now;
            base.Add(warehouseTransactionDetail);
        }
        public new void Delete(WarehouseTransactionDetail warehouseTransactionDetail)
        {
            warehouseTransactionDetail.Status = "DELETED";
            base.Edit(warehouseTransactionDetail);
        }
        public new async Task<int> DeleteAsync(WarehouseTransactionDetail warehouseTransactionDetail)
        {
            warehouseTransactionDetail.Status = "DELETED";
            return await base.EditAsync(warehouseTransactionDetail);
        }
    }
}
    