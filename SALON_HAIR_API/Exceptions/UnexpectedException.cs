using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALON_HAIR_ENTITY.Entities;

namespace SALON_HAIR_API.Exceptions
{
    public class UnexpectedException : Exception
    {
        public Exception exception { get; set; }
        public int HttpCode { get; set; }
        public object DataLog { get; set; }
            

        public UnexpectedException(object data, Exception e)
        {
            HttpCode = 500;
            DataLog = data;
            exception = e;
        }
    }
}
