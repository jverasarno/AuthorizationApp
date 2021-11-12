using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsAdmin { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }

        public bool MustChangePwd { get; set; }
    }
}
