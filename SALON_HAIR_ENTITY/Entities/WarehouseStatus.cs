using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class WarehouseStatus
    {
        public WarehouseStatus()
        {
            Warehouse = new HashSet<Warehouse>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Code { get; set; }

        public ICollection<Warehouse> Warehouse { get; set; }
    }
}
