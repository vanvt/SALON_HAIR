﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class ProductPictures
    {
        public long ProductsId { get; set; }
        public long PicturesId { get; set; }
        public string Status { get; set; }

        public Photo Pictures { get; set; }
        public Product Products { get; set; }
    }
}
