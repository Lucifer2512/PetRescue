namespace DataAccessLayer.Entity
{
    public partial class Shelter
    {
        public Shelter()
        {
            Donations = new HashSet<Donation>();
            Events = new HashSet<Event>();
            Pets = new HashSet<Pet>();
        }

        public Guid ShelterId { get; set; }
        public string ShelterName { get; set; } = null!;
        public string ShelterAddress { get; set; } = null!;
        public string ShelterPhoneNumber { get; set; } = null!;
        public decimal Balance { get; set; }
        public Guid UsersId { get; set; }
        public byte[]? Image { get; set; }

        public virtual User Users { get; set; } = null!;
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
