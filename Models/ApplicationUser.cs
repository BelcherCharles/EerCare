﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EerCare.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
        //public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneExt { get; set; }


    }
}
