using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.ViewModels
{
    public class MetaViewModel
    {
        public int TotalItem { get; set; }
        public int TotalPage { get; set; }
        public int RowPerPage { get; set; }
        public int CurrentPage { get; set; }
        public object Query { get; set; }
    }
}
