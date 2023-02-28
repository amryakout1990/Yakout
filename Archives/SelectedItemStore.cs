using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yakout.Models;

namespace Yakout.Stores
{
    public class SelectedItemStore
    {
        private ItemStore _SelectedItem;

        public ItemStore SelectedItem
        {
            get
            {
                if (_SelectedItem == null)
                {
                    _SelectedItem = new ItemStore();
                }
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                OnSelectedItemChanged();
            }
        }

        private void OnSelectedItemChanged()
        {
            SelectedItemChanged?.Invoke();
        }

        public event Action SelectedItemChanged;

        //when firing it  will grap the property
    }

}
