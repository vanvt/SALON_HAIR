﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.BackupEntities
{
    public partial class Product
    {
        public Product()
        {
            ProductPictures = new HashSet<ProductPictures>();
            ServiceProduct = new HashSet<ServiceProduct>();
            Warehouse = new HashSet<Warehouse>();
        }

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
        public ProductUnit Unit { get; set; }
        public ICollection<ProductPictures> ProductPictures { get; set; }
        public ICollection<ServiceProduct> ServiceProduct { get; set; }
        public ICollection<Warehouse> Warehouse { get; set; }
    }
}
