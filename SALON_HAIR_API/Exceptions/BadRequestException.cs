using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.Exceptions
{
    public class BadRequestException: Exception
    {
        public int HttpCode { get; set; }
        public object DataLog { get; set; }
        public BadRequestException()
        {
            HttpCode = 400;
        }
        public BadRequestException(string message): base(message)
        {
            HttpCode = 400;
        }
        public BadRequestException(string message,object data) : base(message)
        {
            DataLog = data;
            HttpCode = 400;
        }
        public BadRequestException(string message, Exception inner) : base(message, inner)
        {
            HttpCode = 400;
        }
    }
}
