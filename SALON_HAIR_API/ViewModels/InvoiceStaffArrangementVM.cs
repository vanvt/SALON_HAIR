using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.ViewModels
{
    public class InvoiceStaffArrangementVM : Invoice
    {
        public IQueryable<InvoiceStaffArrangement> InvoiceStaffArrangements { get; set; }
    }
    public class InvoiceStaffArrangementVMPut : Invoice
    {
        public ICollection<InvoiceStaffArrangement> InvoiceStaffArrangements { get; set; }
    }
}
