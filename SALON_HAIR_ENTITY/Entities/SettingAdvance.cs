using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class SettingAdvance
    {
        public long Id { get; set; }
        public long SalonId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Enum { get; set; }
        public string Group { get; set; }

        public Salon Salon { get; set; }
    }
}
