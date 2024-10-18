using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Request
{
    public class ShelterRequestModel
    {
        public string ShelterName { get; set; } = null!;
        public string ShelterAddress { get; set; } = null!;
        public string ShelterPhoneNumber { get; set; } = null!;
        public decimal Balance { get; set; }
        [DefaultValue("3F21226B-30C1-4274-81A6-2ED9D9E0C54C")]
        public Guid UsersId { get; set; }
    }

    public class ShelterRequestModelForUpdate
    {
        public string ShelterName { get; set; } = null!;
        public string ShelterAddress { get; set; } = null!;
        public string ShelterPhoneNumber { get; set; } = null!;
        public decimal Balance { get; set; }
        [DefaultValue("3F21226B-30C1-4274-81A6-2ED9D9E0C54C")]
        public Guid UsersId { get; set; }
    }
}
