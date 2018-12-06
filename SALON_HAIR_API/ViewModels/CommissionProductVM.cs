using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.ViewModels
{
    public class CommissionProductVM: CommissionProduct
    {
        public long ProductCategoryId { get; set; }
    }
}
