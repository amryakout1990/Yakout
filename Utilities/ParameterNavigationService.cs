using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yakout.Stores;

namespace Yakout.Utilities
{
    class ParameterNavigationService<TParameter, TViewModel>
        where TViewModel:ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TParameter,TViewModel> _createVM;

        public ParameterNavigationService(NavigationStore navigationStore, Func<TParameter, TViewModel> createVM)
        {
            _navigationStore = navigationStore;
            _createVM= createVM;

        }

        public void Navigate(TParameter parameter)
        {
            _navigationStore.CurrentViewModel = _createVM(parameter);
        }
    }
}
