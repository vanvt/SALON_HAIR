using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class StaffService
    {
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long ServiceId { get; set; }
        public long StaffId { get; set; }

        public Service Service { get; set; }
        public Staff Staff { get; set; }
    }
}
