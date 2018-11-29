using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.ViewModels
{
    public class ProductIdsVM
    {
        public List<long> Ids { get; set; }
        public string StatusCode { get; set; }
    }
    public class ProductIdsDelete
    {
        public List<long> Ids { get; set; }      
    }
}
