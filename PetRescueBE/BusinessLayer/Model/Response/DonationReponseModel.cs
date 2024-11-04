using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Response
{
    public class DonationReponseModel
    {
        public Guid DonationId { get; set; }
        public Guid? EventId { get; set; }
        public Guid ShelterId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonationDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = null!;
    }
}
