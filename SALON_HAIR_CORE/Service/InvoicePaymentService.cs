

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class InvoicePaymentService: GenericRepository<InvoicePayment> ,IInvoicePayment
    {
        private salon_hairContext _salon_hairContext;
        public InvoicePaymentService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(InvoicePayment invoicePayment)
        {
            invoicePayment.Updated = DateTime.Now;

            base.Edit(invoicePayment);
        }
        public async new Task<int> EditAsync(InvoicePayment invoicePayment)
        {            
            invoicePayment.Updated = DateTime.Now;         
            return await base.EditAsync(invoicePayment);
        }
        public new async Task<int> AddAsync(InvoicePayment invoicePayment)
        {
            invoicePayment.Created = DateTime.Now;
            return await base.AddAsync(invoicePayment);
        }
        public new void Add(InvoicePayment invoicePayment)
        {
            invoicePayment.Created = DateTime.Now;
            base.Add(invoicePayment);
        }
        public new void Delete(InvoicePayment invoicePayment)
        {
            invoicePayment.Status = "DELETED";
            base.Edit(invoicePayment);
        }
        public new async Task<int> DeleteAsync(InvoicePayment invoicePayment)
        {
            invoicePayment.Status = "DELETED";
            return await base.EditAsync(invoicePayment);
        }
    }
}
    