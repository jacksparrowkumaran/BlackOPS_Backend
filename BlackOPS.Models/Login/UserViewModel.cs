using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackOPS.Models.Login
{
    public class UserViewModel : IdentityUser
    {
        public string Name { get; set; }
    }
}
