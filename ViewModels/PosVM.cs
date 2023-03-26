using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using Yakout.Commands;
using Yakout.Models;
using Yakout.Stores;
using Yakout.Utilities;

namespace Yakout.ViewModels
{
    class PosVM : Utilities.ViewModelBase
    {

        private ObservableCollection<RadioButton> menuButtons2;
        private CollectionViewSource MenuItemsCollection2;
        public ICollectionView SourceCollection2 => MenuItemsCollection2?.View;
        private RadioButton menuButtonCategories;

        private ObservableCollection<RadioButton> menuButtons;
        private CollectionViewSource MenuItemsCollection;
        private ICollectionView _sourceCollection;
        public ICollectionView SourceCollection
        {
            get { return _sourceCollection; }
            set { _sourceCollection = value; OnPropertyChanged(); }
        }

        private RadioButton menuButtonItems;

        private DataTable _table;
        public DataTable Table
        {
            get { return _table; }
            set { _table = value; OnPropertyChanged(nameof(Table)); }
        }

        private DataTable _tablepay;

        public DataTable Tablepay
        {
            get { return _tablepay; }
            set { _tablepay = value; OnPropertyChanged(nameof(Tablepay)); }
        }

        private SqlCommand _command;
        public SqlCommand Command
        {
            get { return _command; }
            set { _command = value; OnPropertyChanged(nameof(Command)); }
        }

        public ICommand ShowCalculatorCommand { get; private set; }
        public ICommand HideCalculatorCommand { get; private set; }

        private string _paymentId;
        public string PaymentId
        {
            get { return _paymentId; }
            set { _paymentId = value; OnPropertyChanged(); }
        }


        private DataTable _gridBoundDataTable;
        public DataTable GridBoundDataTable
        {
            get
            {
                if (_gridBoundDataTable != null)
                {
                    return _gridBoundDataTable;
                }
                else return new DataTable();
            }
            set { _gridBoundDataTable = value; OnPropertyChanged(); }
        }

        private DataTable _CodeBehindDataTable;
        public DataTable CodeBehindDataTable
        {
            get { return _CodeBehindDataTable; }
            set { _CodeBehindDataTable = value; OnPropertyChanged(); }
        }

        public NavigationStore _navigationStore { get; }


        public ICommand Calculator1Command { get; private set; }
        public ICommand Calculator2Command { get; private set; }
        public ICommand Calculator3Command { get; private set; }
        public ICommand Calculator4Command { get; private set; }
        public ICommand Calculator5Command { get; private set; }
        public ICommand Calculator6Command { get; private set; }
        public ICommand Calculator7Command { get; private set; }
        public ICommand Calculator8Command { get; private set; }
        public ICommand Calculator9Command { get; private set; }
        public ICommand Calculator0Command { get; private set; }
        public ICommand CalculatorClearCommand { get; private set; }
        public ICommand CalculatorPointCommand { get; private set; }
        public ICommand CalculatorEnterCommand { get; private set; }
        public ICommand CalculatorCancelCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand PayCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand SaveWithPrintCommand { get; private set; }
        public ICommand SaveWithoutPrintCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private string _qty;
        public string Qty
        {
            get { return _qty; }
            set { _qty = value; OnPropertyChanged(nameof(Qty)); }
        }

        private string _allPrice;

        public string AllPrice
        {
            get { return _allPrice; }
            set { _allPrice = value; OnPropertyChanged(nameof(AllPrice)); }
        }


        private string _total;
        public string Total
        {
            get { return _total; }
            set { _total = value;
                if (Convert.ToInt32(Total) > 0 && Convert.ToInt32(Paied) >= 0 )
                {
                    Change = (Convert.ToInt32(Total) - Convert.ToInt32(Paied)).ToString();
                    OnPropertyChanged(nameof(Change));
                }
                OnPropertyChanged(nameof(Total)); }
        }

        private bool _showCalculator;
        public bool ShowCalculator
        {
            get { return _showCalculator; }
            set { _showCalculator = value; OnPropertyChanged(nameof(ShowCalculator)); }
        }

        private bool _showSave;

        public bool ShowSave
        {
            get { return _showSave; }
            set { _showSave = value;OnPropertyChanged(); }
        }

        private bool _showPrint;

        public bool ShowPrint
        {
            get { return _showPrint; }
            set { _showPrint = value; OnPropertyChanged(); }
        }


        private int _gridIndex;
        public int GridIndex
        {
            get { return _gridIndex; }
            set { _gridIndex = value; OnPropertyChanged(nameof(GridIndex)); }
        }


        private string _change;
        public string Change
        {
            get {
                if (Convert.ToInt32(Total) > 0 && Convert.ToInt32(Paied) > 0)
                {
                    return (Convert.ToInt32(Total) - Convert.ToInt32(Paied)).ToString();
                }
                return _change;
            }
            set { _change = value;OnPropertyChanged(nameof( Change)); }
        }

        private string _paied;
        public string Paied
        {
            get { return _paied; }
            set {
                _paied = value;
                if (Convert.ToInt32(Total) > 0 && Convert.ToInt32(Paied) >= 0)
                {
                    Change = (Convert.ToInt32(Total) - Convert.ToInt32(Paied)).ToString();
                    OnPropertyChanged(nameof(Change));
                }

                OnPropertyChanged(nameof( Paied)); 
            }
        }
        private LocalReport _Report;
        private ReportViewer _reportviewer;


        private int id;

        public PosVM(NavigationStore navigationStore)
        {
            FillCategoriesIntoButtons();
            ActivateCommands();
            loadPaymemnts();
            ClearAll();
            _navigationStore=navigationStore;
        }

        private void Cancel()
        {
            ShowSave = false;
        }
        private void SaveWithPrint()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("", connection))
                {
                    command1.CommandText = "select max(InvoiceId) from Invoice";
                    Table = new DataTable();
                    Table.Load(command1.ExecuteReader());
                    if (Table.Rows.Count > 0 && !string.IsNullOrEmpty(Table.Rows[0][0].ToString()))
                    {
                        id = Convert.ToInt32(Table.Rows[0][0].ToString());
                        id++;
                    }
                    else
                    {
                        id = 1;
                    }

                    using (SqlCommand command = new SqlCommand("", connection))
                    {
                        command.CommandText = "Insert Into Invoice Values(@1,@2,@3)";
                        command.Parameters.Add("@1", SqlDbType.Int).Value = id;
                        command.Parameters.Add("@2", SqlDbType.NVarChar).Value = Total;
                        command.Parameters.Add("@3", SqlDbType.NVarChar).Value = PaymentId;
                        command.ExecuteNonQuery();
                        foreach (DataRow row in GridBoundDataTable.Rows)
                        {
                            using (SqlCommand command2 = new SqlCommand("", connection))
                            {
                                command2.CommandText = "Insert Into InvoiceDetails Values(@1,@2,@3,@4,@5,@6)";
                                command2.Parameters.Add("@1", SqlDbType.Int).Value = id;
                                command2.Parameters.Add("@2", SqlDbType.NVarChar).Value = row[0].ToString();
                                command2.Parameters.Add("@3", SqlDbType.NVarChar).Value = row[1].ToString();
                                command2.Parameters.Add("@4", SqlDbType.NVarChar).Value = row[2].ToString();
                                command2.Parameters.Add("@5", SqlDbType.NVarChar).Value = row[3].ToString();
                                command2.Parameters.Add("@6", SqlDbType.NVarChar).Value = row[4].ToString();
                                command2.ExecuteNonQuery();
                            }

                        }
                        ShowSave = false;
                        ClearAll();

                        ////////////
                        ShowPrint = true;
                        using (SqlCommand command3 = new SqlCommand("", connection))
                        {
                            DataSet ds = new DataSet();
                            ds.Tables.Add("t1");
                            command3.CommandType = CommandType.StoredProcedure;
                            command3.CommandText = "Invoice_Details";
                            command3.Parameters.Clear();
                            command3.Parameters.Add("@InvoiceId", SqlDbType.Int).Value = 2;
                            ds.Tables["t1"].Load(command3.ExecuteReader());
                            MessageBox.Show(ds.Tables["t1"].Rows[4][4]+"");
                            //_reportviewer.LocalReport.DataSources.Clear();
                            //var rpds_model = new ReportDataSource() { Name = "DataSet1", Value = ds.Tables[0] };
                            //_reportviewer.LocalReport.DataSources.Add(rpds_model);
                            //_reportviewer.LocalReport.EnableExternalImages = true;
                            //_reportviewer.SetDisplayMode(DisplayMode.PrintLayout);
                            //_reportviewer.Refresh();
                            //_reportviewer.RefreshReport();

                        }


                    }

}

            }
        }

        private void SaveWithoutPrint()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("", connection))
                {
                    command1.CommandText = "select max(InvoiceId) from Invoice";
                    Table = new DataTable();
                    Table.Load(command1.ExecuteReader());
                    if (Table.Rows.Count > 0 && !string.IsNullOrEmpty(Table.Rows[0][0].ToString()))
                    {
                        id = Convert.ToInt32(Table.Rows[0][0].ToString());
                        id++;
                    }
                    else
                    {
                        id = 1;
                    }

                    using (SqlCommand command = new SqlCommand("", connection))
                    {
                        command.CommandText = "Insert Into Invoice Values(@1,@2,@3)";
                        command.Parameters.Add("@1", SqlDbType.Int).Value = id;
                        command.Parameters.Add("@2", SqlDbType.NVarChar).Value = Total;
                        command.Parameters.Add("@3", SqlDbType.NVarChar).Value = PaymentId;
                        command.ExecuteNonQuery();
                        foreach (DataRow row in GridBoundDataTable.Rows)
                        {
                            using (SqlCommand command2 = new SqlCommand("", connection))
                            {
                                command2.CommandText = "Insert Into InvoiceDetails Values(@1,@2,@3,@4,@5,@6)";
                                command2.Parameters.Add("@1", SqlDbType.Int).Value = id;
                                command2.Parameters.Add("@2", SqlDbType.NVarChar).Value = row[0].ToString();
                                command2.Parameters.Add("@3", SqlDbType.NVarChar).Value = row[1].ToString();
                                command2.Parameters.Add("@4", SqlDbType.NVarChar).Value = row[2].ToString();
                                command2.Parameters.Add("@5", SqlDbType.NVarChar).Value = row[3].ToString();
                                command2.Parameters.Add("@6", SqlDbType.NVarChar).Value = row[4].ToString();
                                command2.ExecuteNonQuery();
                            }

                        }
                        ShowSave = false;
                        ClearAll();
                    }

                }

            }
        }


        private void Pay()
        {
            ShowSave = true;

        }

        private void loadPaymemnts()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                string sql = "select * from Payments";
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    Tablepay = new DataTable();
                    adapter.Fill(Tablepay);
                }
            }
        }

        private void AddColumns()
        {
            GridBoundDataTable = new DataTable();
            GridBoundDataTable.Columns.Add("ItemId",typeof(string));
            GridBoundDataTable.Columns.Add("ItemName", typeof(string));
            GridBoundDataTable.Columns.Add("Qty", typeof(string));
            GridBoundDataTable.Columns.Add("Price", typeof(string));
            GridBoundDataTable.Columns.Add("AllPrice", typeof(string));

        }
        private DataTable GetCategoriesFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (Command = new SqlCommand("", connection))
                {
                    Command.CommandText = "select * from Categories";
                    using (Table = new DataTable())
                    {
                        Table.Load(Command.ExecuteReader());
                        return Table;
                    }
                }
            }
        }
        private void FillCategoriesIntoButtons()
        {
            using (Table = new DataTable())
            {
                Table = GetCategoriesFromDatabase();
                menuButtons2 = new ObservableCollection<RadioButton>();
                menuButtons2.Clear();
                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    menuButtonCategories = new RadioButton();
                    menuButtonCategories.Content = Table.Rows[i][1].ToString();
                    menuButtonCategories.Tag = Table.Rows[i][0].ToString();
                    //menuButtonCategories.Width = 200;
                    //menuButtonCategories.Height = 100;
                    menuButtonCategories.Name = "btn" + Table.Rows[i][1].ToString();
                    menuButtonCategories.Style = (Style)menuButtonCategories.FindResource("PosCategoriesButtonStyle");
                    menuButtonCategories.Click += menuButtonCategories_Click;
                    menuButtons2.Add(menuButtonCategories);
                }
                MenuItemsCollection2 = new CollectionViewSource { Source = menuButtons2 };
            }

        }
        private void menuButtonCategories_Click(object sender, RoutedEventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            FillItemsIntoButtons(btn.Tag.ToString());
        }

        private void FillItemsIntoButtons(string id)
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (Command = new SqlCommand("", connection))
                {
                    Command.CommandText = "select ItemId,ItemName,Price from Items where CategoryId='" + id + "' ";
                    using (CodeBehindDataTable = new DataTable())
                    {
                        CodeBehindDataTable.Load(Command.ExecuteReader());
                        if (CodeBehindDataTable.Rows.Count > 0)
                        {
                            using (Table = new DataTable())
                            {
                                Table = CodeBehindDataTable;
                                menuButtons = new ObservableCollection<RadioButton>();
                                menuButtons.Clear();
                                for (int i = 0; i < Table.Rows.Count; i++)
                                {
                                    menuButtonItems = new RadioButton();
                                    menuButtonItems.Content = Table.Rows[i][1].ToString();
                                    menuButtonItems.Width = 100;
                                    menuButtonItems.Height = 100;
                                    menuButtonItems.Tag = Table.Rows[i][0].ToString();
                                    menuButtonItems.Name = "btn" + Table.Rows[i][0].ToString();
                                    menuButtonItems.Style = (Style)menuButtonItems.FindResource("PosButtonStyle");
                                    menuButtonItems.Click += menuButtonItems_Click;
                                    menuButtons.Add(menuButtonItems);
                                }

                                MenuItemsCollection = new CollectionViewSource { Source = menuButtons };
                                SourceCollection = MenuItemsCollection.View;
                            }

                        }
                    }
                }
            }


        }
        private void menuButtonItems_Click(object sender, RoutedEventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            if (GridBoundDataTable.Columns.Count < 3)
            {
                AddColumns();
            }
            GetItem(btn.Tag.ToString());
            UpdateTotal();
        }
        private void GetItem(string id)
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (Command = new SqlCommand("", connection))
                {
                    Command.CommandText = "select ItemId,ItemName,Price from Items where ItemId=" +Convert.ToInt32( id)+ " ";
                    using (Table = new DataTable())
                    {
                        Table.Load(Command.ExecuteReader());
                        if (Table.Rows.Count > 0)
                        {
                            //ItemId = Table.Rows[0][0].ToString();
                            DataRow Row = GridBoundDataTable.NewRow();
                            Row["ItemId"] = Table.Rows[0][0].ToString();
                            Row["ItemName"] = Table.Rows[0][1].ToString();
                            Row["Qty"] = "1";
                            Row["Price"] = Table.Rows[0][2].ToString();
                            Row["AllPrice"] = Table.Rows[0][2].ToString();
                            GridBoundDataTable.Rows.Add(Row);

                        }
                    }
                }
            }

        }
        private void ActivateCommands()
        {
            Calculator1Command = new ActionCommand(Calculator1);
            Calculator2Command = new ActionCommand(Calculator2);
            Calculator3Command = new ActionCommand(Calculator3);
            Calculator4Command = new ActionCommand(Calculator4);
            Calculator5Command = new ActionCommand(Calculator5);
            Calculator6Command = new ActionCommand(Calculator6);
            Calculator7Command = new ActionCommand(Calculator7);
            Calculator8Command = new ActionCommand(Calculator8);
            Calculator9Command = new ActionCommand(Calculator9);
            Calculator0Command = new ActionCommand(Calculator0);
            CalculatorClearCommand = new ActionCommand(CalculatorClear);
            CalculatorPointCommand = new ActionCommand(CalculatorPoint);
            ShowCalculatorCommand = new ActionCommand(ShowCal);
            HideCalculatorCommand = new ActionCommand(HideCal);
            CalculatorEnterCommand = new ActionCommand(EnterCal);
            CalculatorCancelCommand = new ActionCommand(CanelCal);
            RemoveCommand = new ActionCommand(RemoveIndex);
            ClearCommand = new ActionCommand(ClearAll);
            ChangeCommand = new ActionCommand(ChangeAll);
            PayCommand = new ActionCommand(Pay);
            BackCommand = new ActionCommand(Back);
            SaveWithPrintCommand = new ActionCommand(SaveWithPrint);
            SaveWithoutPrintCommand = new ActionCommand(SaveWithoutPrint);
            CancelCommand = new ActionCommand(Cancel);

            
        }


        private void Back()
        {
            _navigationStore.CurrentViewModel = new MainBackGroundVM();
        }

        private void ChangeAll()
        {
            Qty = "0";
            GridIndex = -1;
            ShowCalculator = true;
        }

        private void ClearAll()
        {
            while (GridBoundDataTable.Rows.Count>0)
            {
                GridBoundDataTable.Rows.RemoveAt(0);
            }
            Qty = "0";
            Total = "0";
            Paied = "0";
            Change = "0";
            PaymentId = "0";
            GridIndex = -1;
        }

        private void RemoveIndex()
        {
            if (GridIndex >0)
            {
                GridBoundDataTable.Rows.RemoveAt(GridIndex);
                UpdateTotal();
            }
            else if (GridIndex == 0)
            {
                GridBoundDataTable.Rows.RemoveAt(GridIndex);
                UpdateTotal();
                GridIndex = -1;
            }

        }
        private void CanelCal()
        {
            ShowCalculator= false;
        }

        private void EnterCal()
        {
            if (GridIndex == -1)
            {
                Paied = Qty;
                ShowCalculator = false;

            }
            else
            {
                if (Qty != "0")
                {
                    GridBoundDataTable.Rows[GridIndex][2] = Qty;
                    AllPrice = (Convert.ToInt32(Qty) * Convert.ToInt32(GridBoundDataTable.Rows[GridIndex][3])).ToString();
                    GridBoundDataTable.Rows[GridIndex][4] = AllPrice;
                    ShowCalculator = false;
                    UpdateTotal();
                }
            }

        }

        private void UpdateTotal()
        {
            var ttt = (from DataRow y in GridBoundDataTable.Rows
                       where y[4].ToString() != string.Empty
                       select Convert.ToDouble(y[4].ToString())
           ).Sum();
            Total = ttt.ToString();

        }
        private void HideCal()
        {
            ShowCalculator = false;
        }
        private void ShowCal()
        {
            Qty = "0";
            if (GridIndex >0)
            {
                ShowCalculator = true;
            }
            else if (GridIndex==0)
            {
               ShowCalculator = true;
            }
        }
        private string GetCalculatorNumber(string number)
        {
            if (Qty == "0")
            {
                Qty = string.Empty;
                Qty = Qty + number;
            }
            else
            {
                Qty = Qty + number;
            }

            return Qty;
        }
        private void Calculator1()
        {
            GetCalculatorNumber("1");
        }
        private void Calculator2()
        {
            GetCalculatorNumber("2");
        }
        private void Calculator3()
        {
            GetCalculatorNumber("3");
        }
        private void Calculator4()
        {
            GetCalculatorNumber("4");
        }
        private void Calculator5()
        {
            GetCalculatorNumber("5");
        }
        private void Calculator6()
        {
            GetCalculatorNumber("6");
        }
        private void Calculator7()
        {
            GetCalculatorNumber("7");
        }
        private void Calculator8()
        {
            GetCalculatorNumber("8");
        }
        private void Calculator9()
        {
            GetCalculatorNumber("9");
        }
        private void Calculator0()
        {
            GetCalculatorNumber("0");
        }
        private void CalculatorClear()
        {
            Qty = "0";
        }
        private void CalculatorPoint()
        {
            if (!Qty.Contains("."))
            {
                GetCalculatorNumber(".");
            }
        }
    }
}
