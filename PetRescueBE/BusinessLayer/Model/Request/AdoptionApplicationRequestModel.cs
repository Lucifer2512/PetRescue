using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Request
{
    public class AdoptionApplicationRequestModel
    {
        public string Status { get; set; } = null!;
        public Guid PetId { get; set; }
        public string? Notes { get; set; }
    }

}
