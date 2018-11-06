using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.ViewModels
{
    public class BaseViewModel
    {
        public int ErrorCode { get; set; }
        public string ErrorDesc { get; set; }
        public MetaViewModel Meta { get; set; }
        public object Data { get; set; }
    }
}
