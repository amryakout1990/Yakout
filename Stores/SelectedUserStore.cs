using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yakout.Models;

namespace Yakout.Stores
{
    public class SelectedUserStore
    {
        private UsersStore _SelectedUser;

        public UsersStore SelectedUser
        {
            get {
                if (_SelectedUser==null)
                {
                    _SelectedUser = new UsersStore();
                }
                return _SelectedUser; 
                }
            set {
                   _SelectedUser = value;
                   OnSelectedUserChanged();
                }
        }

        private void OnSelectedUserChanged()
        {
            SelectedUserChanged?.Invoke();
        }

        public event Action SelectedUserChanged;

        //when firing it  will grap the property
    }
}
