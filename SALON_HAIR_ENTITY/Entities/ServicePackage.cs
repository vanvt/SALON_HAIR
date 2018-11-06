using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class ServicePackage
    {
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long ServiceId { get; set; }
        public long PackageId { get; set; }

        public Package Package { get; set; }
        public Service Service { get; set; }
    }
}
