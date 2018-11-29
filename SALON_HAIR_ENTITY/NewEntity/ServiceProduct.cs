using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class ServiceProduct
    {
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long ServiceId { get; set; }
        public long ProductId { get; set; }
        public long? Quota { get; set; }

        public Product Product { get; set; }
        public Service Service { get; set; }
    }
}
