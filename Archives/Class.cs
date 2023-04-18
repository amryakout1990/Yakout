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


    //private LocalReport _Report;
    //private ReportViewer reportviewer1=>ppp.reportviewer1;
    //public Pos ppp;

    //private PrintInvoiceVM _printInvo;

    //public PrintInvoiceVM PrintInvo
    //{
    //    get { return _printInvo; }
    //    set { _printInvo = value; OnPropertyChanged(); }
    //}

    //using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
    //{
    //    connection.Open();

    //    using (SqlCommand command1 = new SqlCommand("", connection))
    //    {
    //        command1.CommandText = "select max(InvoiceId) from Invoice";
    //        Table = new DataTable();
    //        Table.Load(command1.ExecuteReader());
    //        if (Table.Rows.Count > 0 && !string.IsNullOrEmpty(Table.Rows[0][0].ToString()))
    //        {
    //            id = Convert.ToInt32(Table.Rows[0][0].ToString());
    //            id++;
    //        }
    //        else
    //        {
    //            id = 1;
    //        }

    //        using (SqlCommand command = new SqlCommand("", connection))
    //        {
    //            command.CommandText = "Insert Into Invoice Values(@1,@2,@3)";
    //            command.Parameters.Add("@1", SqlDbType.Int).Value = id;
    //            command.Parameters.Add("@2", SqlDbType.NVarChar).Value = Total;
    //            command.Parameters.Add("@3", SqlDbType.NVarChar).Value = PaymentId;
    //            command.ExecuteNonQuery();
    //            foreach (DataRow row in GridBoundDataTable.Rows)
    //            {
    //                using (SqlCommand command2 = new SqlCommand("", connection))
    //                {
    //                    command2.CommandText = "Insert Into InvoiceDetails Values(@1,@2,@3,@4,@5,@6)";
    //                    command2.Parameters.Add("@1", SqlDbType.Int).Value = id;
    //                    command2.Parameters.Add("@2", SqlDbType.NVarChar).Value = row[0].ToString();
    //                    command2.Parameters.Add("@3", SqlDbType.NVarChar).Value = row[1].ToString();
    //                    command2.Parameters.Add("@4", SqlDbType.NVarChar).Value = row[2].ToString();
    //                    command2.Parameters.Add("@5", SqlDbType.NVarChar).Value = row[3].ToString();
    //                    command2.Parameters.Add("@6", SqlDbType.NVarChar).Value = row[4].ToString();
    //                    command2.ExecuteNonQuery();
    //                }

    //            }
    //            ShowSave = false;
    //            ClearAll();

    //            //////////
    //            ShowPrint = true;
    //            using (SqlCommand command3 = new SqlCommand("", connection))
    //            {
    //                DataSet ds = new DataSet();
    //                ds.Tables.Add("t1");
    //                command3.CommandType = CommandType.StoredProcedure;
    //                command3.CommandText = "Invoice_Details";
    //                command3.Parameters.Clear();
    //                command3.Parameters.Add("@InvoiceId", SqlDbType.Int).Value = 2;
    //                ds.Tables["t1"].Load(command3.ExecuteReader());
    //                ppp.reportviewer1.LocalReport.ReportEmbeddedResource = "Yakout.Reports.Invoice.rdlc";
    //                ppp.reportviewer1.LocalReport.DataSources.Clear();
    //                ReportDataSource source = new ReportDataSource();
    //                source.Name = "DataSet1";
    //                source.Value = ds.Tables[0];
    //                ppp.reportviewer1.LocalReport.DataSources.Add(source);
    //                ppp.reportviewer1.LocalReport.EnableExternalImages = true;
    //                ppp.reportviewer1.SetDisplayMode(DisplayMode.PrintLayout);
    //                ppp.reportviewer1.Refresh();
    //                ppp.reportviewer1.RefreshReport();

    //            }


    //        }

    //    }

    //}

    //class ParametrizedActionCommand : ICommand
    //{
    //    private readonly Action<string> _execute;

    //    public ParametrizedActionCommand(Action<string> execute)
    //    {
    //        _execute = execute;
    //    }

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add { CommandManager.RequerySuggested += value; }
    //        remove { CommandManager.RequerySuggested -= value; }
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public void Execute(object parameter)
    //    {
    //        _execute((string)parameter);

    //    }

    //}

}
