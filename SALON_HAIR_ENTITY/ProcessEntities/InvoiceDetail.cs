﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SALON_HAIR_ENTITY.Entities;
namespace SALON_HAIR_ENTITY.ProcessEntities
{
    public partial class InvoiceDetail
    {
        public InvoiceDetail()
        {
            InvoiceStaffArrangement = new HashSet<InvoiceStaffArrangement>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? InvoiceId { get; set; }
        public long? ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string ObjectCode { get; set; }
        public decimal ObjectPrice { get; set; }
        public long DiscountUnitId { get; set; }
        public long? DiscountValue { get; set; }
        public decimal? Total { get; set; }
        public int? Quantity { get; set; }
        public bool? IsPaid { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Invoice Invoice { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<InvoiceStaffArrangement> InvoiceStaffArrangement { get; set; }
    }
}
