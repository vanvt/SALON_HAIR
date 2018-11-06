using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.ViewModels
{
    public class ProductVM
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public string PictureContentType { get; set; }
        public decimal Price { get; set; }
        public string PriceText { get; set; }
        public long SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public long? UnitId { get; set; }
        public long? ProductCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsLimit { get; set; }
        public float? Volume { get; set; }
        public long? PhotoId { get; set; }

        public Photo Photo { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public ProductUnitVM Unit { get; set; }
    }
}
