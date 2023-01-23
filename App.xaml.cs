using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Yakout.Stores;
using Yakout.ViewModels;

namespace Yakout
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();

            ///initiation of the first VM
            
            navigationStore.CurrentViewModel = new MainBackGroundVM();

            MainWindow = new MainWindow()
            {
                ///inistiation of the main with passing the CurrentViewModel to the mainVM
                DataContext = new MainVM(navigationStore)
            };
             MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
