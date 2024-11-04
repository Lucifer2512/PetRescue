using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Response
{
    public class PetResponseModel
    {
        public Guid PetId { get; set; }
        public Guid ShelterId { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Description { get; set; }
        public string Species { get; set; } = null!;
        public string? Status { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
