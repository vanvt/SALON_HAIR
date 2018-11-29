using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class Router
    {
        public Router()
        {
            AuthorityRouter = new HashSet<AuthorityRouter>();
        }

        public long Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Path { get; set; }
        public string Display { get; set; }
        public string Name { get; set; }
        public int? OrderDisplay { get; set; }
        public bool? IsSystemFunction { get; set; }
        public string Status { get; set; }

        public ICollection<AuthorityRouter> AuthorityRouter { get; set; }
    }
}
