using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Response
{
    public class AdoptionApplicationResponseModel
    {
        public string? UserName { get; set; }
        public string? PetName { get; set; }
        public DateTime RequestDate { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }
}
