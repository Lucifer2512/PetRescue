namespace BusinessLayer.Model.Request
{
    public class DonationRequestModel
    {
        public Guid? EventId { get; set; }
        public Guid ShelterId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        //public DateTime DonationDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = null!;
    }
    public class DonationRequestModelQRCode
    {
        public Guid EventId { get; set; }
        public Guid ShelterId { get; set; }
        public Guid UserId { get; set; }
        public int Amount { get; set; }


    }
}
