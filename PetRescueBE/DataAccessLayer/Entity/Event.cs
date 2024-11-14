namespace DataAccessLayer.Entity
{
    public partial class Event
    {
        public Event()
        {
            Donations = new HashSet<Donation>();
        }

        public Guid EventId { get; set; }
        public Guid? ShelterId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string? Location { get; set; }
        public string Status { get; set; } = null!;
        public string EventType { get; set; } = null!;
        public string? Goal { get; set; }
        public byte[]? Image { get; set; }

        public virtual Shelter? Shelter { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
    }
}
