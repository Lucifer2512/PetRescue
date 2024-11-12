using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity
{
    public partial class Pet
    {
        public Pet()
        {
            AdoptionApplications = new HashSet<AdoptionApplication>();
        }

        public Guid PetId { get; set; }
        public Guid ShelterId { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Description { get; set; }
        public string Species { get; set; } = null!;
        public string? Status { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public byte[]? Image { get; set; }

        public virtual Shelter Shelter { get; set; } = null!;
        public virtual ICollection<AdoptionApplication> AdoptionApplications { get; set; }
    }
}
