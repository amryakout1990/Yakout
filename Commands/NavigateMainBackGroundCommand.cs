using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Yakout.Stores;
using Yakout.Utilities;
using Yakout.ViewModels;

namespace Yakout.Commands
{
    class NavigateMainBackGroundCommand : Utilities.CommandBase
    {
        /// <summary>
        /// حاليا مفيش احتياج لاوامر دي 
        /// </summary>

        private readonly NavigationService<MainBackGroundVM> _navigationService;

        private readonly MainBackGroundVM _mainBackGroundVM;

        public NavigateMainBackGroundCommand(MainBackGroundVM mainBackGroundVM,  NavigationService<MainBackGroundVM>  navigationService)
        {
            _navigationService = navigationService;
            _mainBackGroundVM = mainBackGroundVM;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
