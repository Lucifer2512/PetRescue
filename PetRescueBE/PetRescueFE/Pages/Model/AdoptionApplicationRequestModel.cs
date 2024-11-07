﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetRescueFE.Pages.Model
{
    public class AdoptionApplicationRequestModel
    {
        public Guid ApplicationId { get; set; }
        public Guid PetId { get; set; }
        public Guid UserId { get; set; }
        public string? Notes { get; set; }
    }

    public class AdoptionApplicationRequestModelForUpdate
    {
        public Guid ApplicationId { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }

}
