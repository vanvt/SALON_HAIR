using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class SysScheduleJob
    {
        public long Id { get; set; }
        public string EventName { get; set; }
        public bool? IsRun { get; set; }
        public DateTime? Created { get; set; }
        public string Result { get; set; }
    }
}
