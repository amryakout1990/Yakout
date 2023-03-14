using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yakout.Stores;
using Yakout.Utilities;
using Yakout.ViewModels;

namespace Yakout.Commands
{
    class NavigatePosCommand:Utilities.CommandBase
    {
        private readonly NavigationStore _navigationStore;

        private readonly SelectedItemStore _selectedItemStore;

        public NavigatePosCommand(NavigationStore navigationStore, SelectedItemStore selectedItemStore)
        {
            _navigationStore = navigationStore;

            _selectedItemStore = selectedItemStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new PosVM(_navigationStore, _selectedItemStore);

        }

    }
}
