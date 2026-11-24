using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LabLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
