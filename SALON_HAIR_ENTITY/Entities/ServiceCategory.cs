using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class ServiceCategory
    {
        public ServiceCategory()
        {
            Commission = new HashSet<Commission>();
            Service = new HashSet<Service>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public long SalonId { get; set; }
        public long? SalonBranchId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<Commission> Commission { get; set; }
        public ICollection<Service> Service { get; set; }
    }
}
