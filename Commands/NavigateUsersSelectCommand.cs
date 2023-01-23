using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yakout.Stores;
using Yakout.Utilities;
using Yakout.ViewModels;

namespace Yakout.Commands
{
    class NavigateUsersSelectCommand : Utilities.CommandBase
    {

        /// <summary>
        /// حاليا مفيش احتياج لاوامر دي 
        /// </summary>
        private readonly NavigationService<UserSelectVM> _navigationService;

        private readonly UserSelectVM _userSelectVM;

        public NavigateUsersSelectCommand(UserSelectVM userSelectVM, NavigationService<UserSelectVM> navigationService)
        {
            _navigationService = navigationService;
            _userSelectVM = userSelectVM;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }

    }
}
