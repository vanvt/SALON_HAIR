

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class InvoiceDetailService: GenericRepository<InvoiceDetail> ,IInvoiceDetail
    {
        private salon_hairContext _salon_hairContext;
        public InvoiceDetailService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.Updated = DateTime.Now;

            base.Edit(invoiceDetail);
        }
        public async new Task<int> EditAsync(InvoiceDetail invoiceDetail)
        {            
            invoiceDetail.Updated = DateTime.Now;         
            return await base.EditAsync(invoiceDetail);
        }
        public new async Task<int> AddAsync(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.Created = DateTime.Now;
            return await base.AddAsync(invoiceDetail);
        }
        public new void Add(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.Created = DateTime.Now;
            base.Add(invoiceDetail);
        }
        public new void Delete(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.Status = "DELETED";
            base.Edit(invoiceDetail);
        }
        public new async Task<int> DeleteAsync(InvoiceDetail invoiceDetail)
        {
            invoiceDetail.Status = "DELETED";
            return await base.EditAsync(invoiceDetail);
        }
    }
}
    