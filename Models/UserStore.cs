using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yakout.Models
{
    public class UsersStore 
    {
        public int id { get; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string JobDes { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }


    }
}
