

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Service
{
    public class InvoiceStaffArrangementService: GenericRepository<InvoiceStaffArrangement> ,IInvoiceStaffArrangement
    {
        private salon_hairContext _salon_hairContext;
        public InvoiceStaffArrangementService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(InvoiceStaffArrangement invoiceStaffArrangement)
        {
            invoiceStaffArrangement.Updated = DateTime.Now;

            base.Edit(invoiceStaffArrangement);
        }
        public async new Task<int> EditAsync(InvoiceStaffArrangement invoiceStaffArrangement)
        {            
            invoiceStaffArrangement.Updated = DateTime.Now;         
            return await base.EditAsync(invoiceStaffArrangement);
        }
        public new async Task<int> AddAsync(InvoiceStaffArrangement invoiceStaffArrangement)
        {
            invoiceStaffArrangement.Created = DateTime.Now;
            return await base.AddAsync(invoiceStaffArrangement);
        }
        public new void Add(InvoiceStaffArrangement invoiceStaffArrangement)
        {
            invoiceStaffArrangement.Created = DateTime.Now;
            base.Add(invoiceStaffArrangement);
        }
        public new void Delete(InvoiceStaffArrangement invoiceStaffArrangement)
        {
            invoiceStaffArrangement.Status = "DELETED";
            base.Edit(invoiceStaffArrangement);
        }
        public new async Task<int> DeleteAsync(InvoiceStaffArrangement invoiceStaffArrangement)
        {
            invoiceStaffArrangement.Status = "DELETED";
            return await base.EditAsync(invoiceStaffArrangement);
        }
    }
}
    