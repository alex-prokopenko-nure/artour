using Artour.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models
{
    public class User
    {
        public Int32 UserId { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public ProfileType ProfileType { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
