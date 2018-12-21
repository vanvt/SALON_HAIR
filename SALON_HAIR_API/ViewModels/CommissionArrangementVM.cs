using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.ViewModels
{


    public class CommissionArrangementVM
    {
        public long Id { get; set; }
        public IQueryable<CommissionArrangement> InvoiceStaffArrangements { get; set; }
        public string Note { get; set; }
    }
    public class CommissionArrangementVMPut
    {
        public long Id { get; set; }
        public ICollection<CommissionArrangement> InvoiceStaffArrangements { get; set; }
        public string Note { get; set; }
    }
}
