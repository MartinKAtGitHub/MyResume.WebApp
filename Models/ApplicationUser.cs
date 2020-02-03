﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid UserInformationId { get; set; }
        public UserInformation UserInformation { get; set; }
        public List<Achievement> Achievements { get; set; }
    }
}
