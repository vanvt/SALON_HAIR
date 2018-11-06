using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_AUTHENTICATION.Models
{
    public class AccountTokens
    {
        public long Id { get; set; }
        public long? AccountId { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public string AccessIp { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ExpDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
