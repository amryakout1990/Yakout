using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yakout.Models
{
    public class UsersStore 
    {
        public int id { get; set; }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        private string _FullName;
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        private string _JobDes;
        public string JobDes
        {
            get { return _JobDes; }
            set { _JobDes = value; }
        }
        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Phone;

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }



    }
}
