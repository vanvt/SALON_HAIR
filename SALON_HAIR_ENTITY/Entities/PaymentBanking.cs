using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class PaymentBanking
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Name { get; set; }
        public long? SalonId { get; set; }
        public long? PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public Salon Salon { get; set; }
    }
}
