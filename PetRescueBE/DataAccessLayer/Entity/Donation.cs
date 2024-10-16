namespace DataAccessLayer.Entity
{
    public partial class Donation
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

        public virtual Event? Event { get; set; }
        public virtual Shelter Shelter { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
