using System;
﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Response
{
    public class AdoptionApplicationResponseModel
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public Guid PetId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
