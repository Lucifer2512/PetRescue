namespace DataAccessLayer.Entity
{
    public partial class AdoptionApplication
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public Guid PetId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Notes { get; set; }

        public virtual Pet Pet { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
