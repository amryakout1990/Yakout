using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Yakout.Commands;
using Yakout.Stores;

namespace Yakout.ViewModels
{
    class PaymentsVM : Utilities.ViewModelBase
    {

        private NavigationStore _navigationStore1;

        public NavigationStore _navigationStore
        {
            get { return _navigationStore1; }
            set { _navigationStore1 = value; OnPropertyChanged(); }
        }

        private string _PaymentId;

        public string PaymentId
        {
            get { return _PaymentId; }
            set { _PaymentId = value; OnPropertyChanged(); }
        }

        private string _PaymentName;

        public string PaymentName
        {
            get { return _PaymentName; }
            set { _PaymentName = value; OnPropertyChanged(); }
        }

        public ICommand SavePayments { get; }

        public ICommand ShowPayments { get; }

        public ICommand NavigateSetUP { get; }

        private DataTable table;

        private DataTable _gridTable;

        public DataTable GridTable
        {
            get { return _gridTable; }
            set { _gridTable = value; OnPropertyChanged(); }
        }

        private DataRowView _selectedPayment;

        public DataRowView SelectedPayment
        {
            get { return _selectedPayment; }
            set { _selectedPayment = value; }
        }

        private SqlCommand command;

        public PaymentsVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            PaymentLoad();

            SavePayments = new ActionCommand(SaveOrUpdate);

            ShowPayments = new ActionCommand(ShowPayment);

            NavigateSetUP = new ActionCommand(NavigateSetUpView);

        }

        private void NavigateSetUpView()
        {
            _navigationStore.CurrentViewModel = new SetUpVM(_navigationStore);
        }

        private void ShowPayment()
        {
            PaymentId = SelectedPayment[0].ToString();

            PaymentName = SelectedPayment[1].ToString();
        }

        private void SaveOrUpdate()
        {
            if (!string.IsNullOrEmpty(PaymentName))
            {
                using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
                {
                    connection.Open();
                    string sql = "select * from Payments where Id='" + PaymentId + "'";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                    {
                        using (table = new DataTable())
                        {
                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                using (command = new SqlCommand("", connection))
                                {
                                    command.CommandText = "update Payments set Name=@1 where Id='" + PaymentId + "'";
                                    command.Parameters.Clear();
                                    command.Parameters.Add("@1", SqlDbType.NVarChar).Value = PaymentName;
                                    command.ExecuteNonQuery();
                                    int NewPaymentId = Convert.ToInt32(PaymentId);
                                    NewPaymentId++;
                                    PaymentId = NewPaymentId + "";
                                    MessageBox.Show("updated Sucessfully");
                                    PaymentName = "";
                                    GetDataGridData();

                                }
                            }
                            else
                            {

                                using (command = new SqlCommand("", connection))
                                {
                                    command.CommandText = "insert into Payments values (@1,@2)";
                                    command.Parameters.Clear();
                                    command.Parameters.Add("@1", SqlDbType.NVarChar).Value = PaymentId;
                                    command.Parameters.Add("@2", SqlDbType.NVarChar).Value = PaymentName;
                                    command.ExecuteNonQuery();
                                    int NewPaymentId = Convert.ToInt32(PaymentId);
                                    NewPaymentId++;
                                    PaymentId = NewPaymentId + "";
                                    MessageBox.Show("Saved Sucessfully");
                                    PaymentName = "";
                                    GetDataGridData();

                                }
                            }


                        }
                    }


                }

            }
            else
            {
                MessageBox.Show("Enter Payment Name ");
            }
        }

        private void PaymentLoad()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();

                string sql = "select max(Id) from Payments";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    using (table = new DataTable())
                    {
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            string sNewPaymentId =table.Rows[0][0].ToString();

                            if (string.IsNullOrEmpty(sNewPaymentId))
                            {
                                PaymentId = "1";
                                PaymentName = "";
                            }
                            else
                            {
                                int NewPaymentId = Convert.ToInt32(sNewPaymentId);
                                NewPaymentId++;
                                PaymentId = NewPaymentId + "";
                                PaymentName = "";
                            }
                        }
                        else
                        {
                            PaymentId = "1";
                            PaymentName = "";
                        }
                    }
                }

                GetDataGridData();
            }
        }

        private void GetDataGridData()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();

                string sqlAll = "select * from Payments";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlAll, connection))
                {
                    using (GridTable = new DataTable())
                    {
                        adapter.Fill(GridTable);
                    }
                }

            }

        }
    }

}
