

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class InvoiceStatusService: GenericRepository<InvoiceStatus> ,IInvoiceStatus
    {
        private salon_hairContext _salon_hairContext;
        public InvoiceStatusService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(InvoiceStatus invoiceStatus)
        {
            invoiceStatus.Updated = DateTime.Now;

            base.Edit(invoiceStatus);
        }
        public async new Task<int> EditAsync(InvoiceStatus invoiceStatus)
        {            
            invoiceStatus.Updated = DateTime.Now;         
            return await base.EditAsync(invoiceStatus);
        }
        public new async Task<int> AddAsync(InvoiceStatus invoiceStatus)
        {
            invoiceStatus.Created = DateTime.Now;
            return await base.AddAsync(invoiceStatus);
        }
        public new void Add(InvoiceStatus invoiceStatus)
        {
            invoiceStatus.Created = DateTime.Now;
            base.Add(invoiceStatus);
        }
        public new void Delete(InvoiceStatus invoiceStatus)
        {
            invoiceStatus.Status = "DELETED";
            base.Edit(invoiceStatus);
        }
        public new async Task<int> DeleteAsync(InvoiceStatus invoiceStatus)
        {
            invoiceStatus.Status = "DELETED";
            return await base.EditAsync(invoiceStatus);
        }
    }
}
    