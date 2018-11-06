using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.ViewModels
{
    public class ProductUnitVM
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
