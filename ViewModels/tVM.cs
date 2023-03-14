using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yakout.Stores;

namespace Yakout.ViewModels
{
    class tVM:Utilities.ViewModelBase
    {
        private readonly SelectedItemStore _selectedItemStore;

        public string ItemName => _selectedItemStore?.SelectedItem.ItemName ?? "unkown item";

        public tVM(SelectedItemStore selectedItemStore)
        {
            _selectedItemStore = selectedItemStore;

            _selectedItemStore.SelectedItemChanged += _selectedItemStore_SelectedItemChanged;

        }

        private void _selectedItemStore_SelectedItemChanged()
        {
            OnPropertyChanged(nameof(ItemName));
        }


    }
}
