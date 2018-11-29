using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class Setting
    {
        public int Id { get; set; }
        public string Field { get; set; }
        public string Table { get; set; }
        public string FieldDisplay { get; set; }
        public string TableDisplay { get; set; }
        public bool? Searchalbe { get; set; }
        public int Oder { get; set; }
        public bool? IsDisplay { get; set; }
        public string TableDb { get; set; }
        public string FieldDb { get; set; }
        public int? TableId { get; set; }
        public string FieldType { get; set; }
        public string TableType { get; set; }
        public string Status { get; set; }
    }
}
