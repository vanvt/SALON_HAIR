using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class AuthorityRouter
    {
        public long AuthorityId { get; set; }
        public long RouterId { get; set; }
        public string Status { get; set; }
        public bool? Active { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Created { get; set; }

        public Authority Authority { get; set; }
        public Router Router { get; set; }
    }
}
