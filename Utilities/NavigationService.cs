using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yakout.Stores;

namespace Yakout.Utilities
{
    class NavigationService<TViewModel> 
        where TViewModel:ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private readonly Func<TViewModel> _CreateViewModel;

        public NavigationService(NavigationStore navigationStore,Func<TViewModel>CreateViewModel)
        {
            _navigationStore = navigationStore;
            _CreateViewModel = CreateViewModel;
        }
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _CreateViewModel();
        }
    }
}
