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
            User = new HashSet<User>();
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
        public string Status { get; set; }

        public ICollection<Customer> Customer { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<ProductPictures> ProductPictures { get; set; }
        public ICollection<User> User { get; set; }
    }
}
