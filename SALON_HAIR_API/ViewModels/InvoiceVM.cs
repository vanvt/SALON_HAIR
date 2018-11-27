using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.ViewModels
{
    public class InvoiceVM: Invoice
    {
       public IQueryable<PackgeAvailable> PackgeAvailables { get; set; }
    }
    public class PackgeAvailable
    {
        public Package Package { get; set; }
        public int? NumberOfPayed { get; set; }
        public int? NumberOfUsed { get; set; }
        public int? NumberRemaining { get; set; }
    }
}
