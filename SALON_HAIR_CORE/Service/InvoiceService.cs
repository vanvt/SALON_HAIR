

using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace SALON_HAIR_CORE.Service
{
    public class InvoiceService: GenericRepository<Invoice> ,IInvoice
    {
        private salon_hairContext _salon_hairContext;
        public InvoiceService(salon_hairContext salon_hairContext) : base(salon_hairContext)
        {
            _salon_hairContext = salon_hairContext;
        }        
        public new void Edit(Invoice invoice)
        {
            invoice.Updated = DateTime.Now;

            base.Edit(invoice);
        }
        public async new Task<int> EditAsync(Invoice invoice)
        {            
            invoice.Updated = DateTime.Now;         
            return await base.EditAsync(invoice);
        }
        public new async Task<int> AddAsync(Invoice invoice)
        {
            var indexObject = _salon_hairContext.SysObjectAutoIncreament.Where(e => e.SpaId == invoice.SalonId && e.ObjectName.Equals("Invoice")).FirstOrDefault();
         
            if(indexObject == null)
            {
                indexObject = new SysObjectAutoIncreament
                {
                    SpaId = invoice.SalonId,
                    ObjectIndex = 1,
                    ObjectName = "Invoice"
                };
                await _salon_hairContext.SysObjectAutoIncreament.AddAsync(
                 indexObject
                    );
            }
            else
            {
                indexObject.ObjectIndex++;
                _salon_hairContext.SysObjectAutoIncreament.Update(indexObject);
            }
            invoice.Created = DateTime.Now;
            invoice.Code = "ES" + indexObject.ObjectIndex.ToString("000000");
            //invoice = LoadAllReference(invoice);
            await  _salon_hairContext.Invoice.AddAsync(invoice);
            return await _salon_hairContext.SaveChangesAsync();
        }
        public new void Add(Invoice invoice)
        {
            invoice.Created = DateTime.Now;
            base.Add(invoice);
        }
        public new void Delete(Invoice invoice)
        {
            invoice.Status = "DELETED";
            base.Edit(invoice);
        }
        public new async Task<int> DeleteAsync(Invoice invoice)
        {
            invoice.Status = "DELETED";
            return await base.EditAsync(invoice);
        }
    }
}
    