

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class CashBookService: GenericRepository<CashBook> ,ICashBook
    {
        private salon_hairContext _salon_hairContext;
        public CashBookService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(CashBook cashBook)
        {
            cashBook.Updated = DateTime.Now;

            base.Edit(cashBook);
        }
        public async new Task<int> EditAsync(CashBook cashBook)
        {            
            cashBook.Updated = DateTime.Now;         
            return await base.EditAsync(cashBook);
        }
        public new async Task<int> AddAsync(CashBook cashBook)
        {
            cashBook.Created = DateTime.Now;
            return await base.AddAsync(cashBook);
        }
        public new void Add(CashBook cashBook)
        {
            cashBook.Created = DateTime.Now;
            base.Add(cashBook);
        }
        public new void Delete(CashBook cashBook)
        {
            cashBook.Status = "DELETED";
            base.Edit(cashBook);
        }
        public new async Task<int> DeleteAsync(CashBook cashBook)
        {
            cashBook.Status = "DELETED";
            return await base.EditAsync(cashBook);
        }
    }
}
    