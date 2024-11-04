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
        public Guid PetId { get; set; }
        public Guid UserId { get; set; }
        public string? Notes { get; set; }
    }

    public class AdoptionApplicationRequestModelForUpdate
    {
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }
}
