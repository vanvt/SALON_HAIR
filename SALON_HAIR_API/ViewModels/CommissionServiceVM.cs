using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.ViewModels
{
    public class CommissionServiceVM : CommissionService
    {
        public long ServiceCategoryId { get; set; }
    }
}
