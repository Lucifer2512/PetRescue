namespace DataAccessLayer.Entity
{
    public partial class User
    {
        public User()
        {
            AdoptionApplications = new HashSet<AdoptionApplication>();
            Donations = new HashSet<Donation>();
            Shelters = new HashSet<Shelter>();
        }

        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string PasswordHash { get; set; } = null!;
        public Guid RoleId { get; set; }
        public string Status { get; set; } = null!;
        public byte[]? Image { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<AdoptionApplication> AdoptionApplications { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }
    }
}
