

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class InvoicePaymentDetailService: GenericRepository<InvoicePaymentDetail> ,IInvoicePaymentDetail
    {
        private salon_hairContext _salon_hairContext;
        public InvoicePaymentDetailService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(InvoicePaymentDetail invoicePaymentDetail)
        {
            invoicePaymentDetail.Updated = DateTime.Now;

            base.Edit(invoicePaymentDetail);
        }
        public async new Task<int> EditAsync(InvoicePaymentDetail invoicePaymentDetail)
        {            
            invoicePaymentDetail.Updated = DateTime.Now;         
            return await base.EditAsync(invoicePaymentDetail);
        }
        public new async Task<int> AddAsync(InvoicePaymentDetail invoicePaymentDetail)
        {
            invoicePaymentDetail.Created = DateTime.Now;
            return await base.AddAsync(invoicePaymentDetail);
        }
        public new void Add(InvoicePaymentDetail invoicePaymentDetail)
        {
            invoicePaymentDetail.Created = DateTime.Now;
            base.Add(invoicePaymentDetail);
        }
        public new void Delete(InvoicePaymentDetail invoicePaymentDetail)
        {
            invoicePaymentDetail.Status = "DELETED";
            base.Edit(invoicePaymentDetail);
        }
        public new async Task<int> DeleteAsync(InvoicePaymentDetail invoicePaymentDetail)
        {
            invoicePaymentDetail.Status = "DELETED";
            return await base.EditAsync(invoicePaymentDetail);
        }
    }
}
    