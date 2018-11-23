using Newtonsoft.Json;
using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SALON_HAIR_ENTITY.ProcessEntities
{
    public partial class InvoiceStaffArrangement
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? InvoiceDetailId { get; set; }
        public long? InvoiceId { get; set; }
        public long? ServiceId { get; set; }
        public long? StaffId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Invoice Invoice { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public InvoiceDetail InvoiceDetail { get; set; }
        public Service Service { get; set; }
        public Staff Staff { get; set; }
    }
}
