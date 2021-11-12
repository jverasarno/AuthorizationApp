using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Common
{
    public class LoggedUser
    {

        public int UserID { get; set; }
        public string UserName { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public string Role { get; set; }

        public bool MustChangePwd { get; set; }


    }
}
