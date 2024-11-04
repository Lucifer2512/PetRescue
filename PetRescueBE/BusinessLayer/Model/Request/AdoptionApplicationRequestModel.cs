using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Request
{
    public class AdoptionApplicationRequestModel
    {
   //     public Guid UserId { get; set; }
        public Guid PetId { get; set; }
      //  public DateTime RequestDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
