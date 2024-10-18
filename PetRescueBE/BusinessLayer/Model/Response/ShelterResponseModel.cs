using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Response
{
    public class ShelterResponseModel
    {
        public Guid ShelterId { get; set; }
        public string ShelterName { get; set; } = null!;
        public string ShelterAddress { get; set; } = null!;
        public string ShelterPhoneNumber { get; set; } = null!;
        public decimal Balance { get; set; }
        public Guid UsersId { get; set; }
    }
}
