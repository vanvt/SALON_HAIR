using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class CommissionUnit
    {
        public CommissionUnit()
        {
            CommissionDetailLimitCommisionUnit = new HashSet<CommissionDetail>();
            CommissionDetailRetailCommisionUnit = new HashSet<CommissionDetail>();
            CommissionDetailWholesaleCommisionUnit = new HashSet<CommissionDetail>();
            CommissionLimitCommisionUnit = new HashSet<Commission>();
            CommissionRetailCommisionUnit = new HashSet<Commission>();
            CommissionWholesaleCommisionUnit = new HashSet<Commission>();
            Invoice = new HashSet<Invoice>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<CommissionDetail> CommissionDetailLimitCommisionUnit { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<CommissionDetail> CommissionDetailRetailCommisionUnit { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<CommissionDetail> CommissionDetailWholesaleCommisionUnit { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Commission> CommissionLimitCommisionUnit { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Commission> CommissionRetailCommisionUnit { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Commission> CommissionWholesaleCommisionUnit { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Invoice> Invoice { get; set; }
    }
}
