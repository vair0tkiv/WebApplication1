using System;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class Users
    {
        public int IdUser { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
    }
}
