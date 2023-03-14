using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Yakout.Models;

namespace Yakout.Archives
{
    class Class
    {
        private CollectionViewSource MenuItemsCollection;

        public ICollectionView SourceCollection => MenuItemsCollection.View;

        public Class()
        {
            //ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>
            //{
            //    //new MenuItems { MenuName = "POS"},
            //    //new MenuItems { MenuName = "Set Up " },
            //    //new MenuItems { MenuName = "Reports" },
            //    //new MenuItems { MenuName = "Options" },
            //    //new MenuItems { MenuName = "Log Out" },
            //};

            //MenuItemsCollection = new CollectionViewSource { Source = menuItems };

        }
        // Switch Views
        public void SwitchViews(object parameter)
        {
            //switch (parameter)
            //{
            //    case "Home":
            //        _selectedViewModel = new HomeViewModel();
            //        break;
            //    case "Desktop":
            //        SelectedViewModel = new DesktopViewModel();
            //        break;
            //    case "Documents":
            //        SelectedViewModel = new DocumentViewModel();
            //        break;
            //    case "Downloads":
            //        SelectedViewModel = new DownloadViewModel();
            //        break;
            //    case "Pictures":
            //        SelectedViewModel = new PictureViewModel();
            //        break;
            //    case "Music":
            //        SelectedViewModel = new MusicViewModel();
            //        break;
            //    case "Movies":
            //        SelectedViewModel = new MovieViewModel();
            //        break;
            //    case "Trash":
            //        SelectedViewModel = new TrashViewModel();
            //        break;
            //    default:
            //        SelectedViewModel = new HomeViewModel();
            //        break;
            //}
        }

        // Menu Button Command
        private ICommand _menucommand;
        public ICommand MenuCommand
        {
            get
            {
                if (_menucommand == null)
                {
                    _menucommand = new Utilities.RelayCommand(param => SwitchViews(param));
                }
                return _menucommand;
            }
        }
    }

    //private ObservableCollection<Button> menuButtons;

    //private CollectionViewSource _MenuItemsCollection;
    //public CollectionViewSource MenuItemsCollection
    //{
    //    get { return _MenuItemsCollection; }
    //    set { _MenuItemsCollection = value; OnPropertyChanged(); }
    //}

    //public ICollectionView SourceCollection => MenuItemsCollection.View;
    //menuButtons = new ObservableCollection<Button>();

}
