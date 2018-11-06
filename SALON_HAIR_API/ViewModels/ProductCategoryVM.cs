using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.ViewModels
{
    public class ProductCategoryVM
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public long SpaId { get; set; }
        public long SalonBranchId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public IQueryable<ProductVM> Product { get; set; }
    }
}
