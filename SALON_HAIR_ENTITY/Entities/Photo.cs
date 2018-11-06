using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Photo
    {
        public Photo()
        {
            Customer = new HashSet<Customer>();
            Product = new HashSet<Product>();
            ProductPictures = new HashSet<ProductPictures>();
            Staff = new HashSet<Staff>();
            StaffCommisonGroup = new HashSet<StaffCommisonGroup>();
        }

        public long Id { get; set; }
        public byte[] Data { get; set; }
        public string DataContentType { get; set; }
        public float? Height { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public float? Width { get; set; }

        public ICollection<Customer> Customer { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<ProductPictures> ProductPictures { get; set; }
        public ICollection<Staff> Staff { get; set; }
        public ICollection<StaffCommisonGroup> StaffCommisonGroup { get; set; }
    }
}
