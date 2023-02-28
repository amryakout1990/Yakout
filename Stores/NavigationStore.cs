using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yakout.Utilities;
using Yakout.ViewModels;

namespace Yakout.Stores
{
    public class NavigationStore
    {
        ///the bind property for navigation
        private ViewModelBase _CurrentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set
            {
                _CurrentViewModel?.Dispose();
                _CurrentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action CurrentViewModelChanged;



        private ViewModelBase _CurrentMenuButtons;

        public ViewModelBase CurrentMenuButtons
        {
            get { return _CurrentMenuButtons; }
            set {
                _CurrentMenuButtons?.Dispose();
                  _CurrentMenuButtons = value;
                OnCurrentMenuButtonsChanged();
            }
        }

        private void OnCurrentMenuButtonsChanged()
        {
            CurrentMenuButtonsChanged?.Invoke();
        }

        public event Action CurrentMenuButtonsChanged;

    }
}
