using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SALON_HAIR_ENTITY.Entities;

namespace SALON_HAIR_ENTITY.ProcessEntities
{
    public partial class InvoiceStatus
    {
        public InvoiceStatus()
        {
            Invoice = new HashSet<Invoice>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Code { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Display { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Invoice> Invoice { get; set; }
    }
}
