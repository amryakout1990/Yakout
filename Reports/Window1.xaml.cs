using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Yakout.Archives
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly int _invoiceId;

        public Window1(int invoiceId)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
            Loaded += Window1_Loaded;
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            show();
        }

        private void show()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (SqlCommand command3 = new SqlCommand("", connection))
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add("t1");
                    ds.Tables.Add("t2");
                    command3.CommandType = CommandType.StoredProcedure;
                    command3.CommandText = "Invoice_Details";
                    command3.Parameters.Clear();
                    command3.Parameters.Add("@InvoiceId", SqlDbType.Int).Value = _invoiceId;
                    ds.Tables["t1"].Load(command3.ExecuteReader());
                    using (SqlCommand command4 = new SqlCommand("", connection))
                    {
                        command4.CommandType = CommandType.StoredProcedure;
                        command4.CommandText = "GetOptions";
                        ds.Tables["t2"].Load(command4.ExecuteReader());
                    }
                    reportviewer1.LocalReport.ReportEmbeddedResource = "Yakout.Reports.Invoice.rdlc";
                    reportviewer1.LocalReport.DataSources.Clear();
                    ReportDataSource source = new ReportDataSource();
                    source.Name = "DataSet1";
                    source.Value = ds.Tables[0];
                    reportviewer1.LocalReport.DataSources.Add(source);
                    ReportDataSource source1 = new ReportDataSource();
                    source1.Name = "DataSet2";
                    source1.Value = ds.Tables[1];
                    reportviewer1.LocalReport.DataSources.Add(source);
                    reportviewer1.LocalReport.DataSources.Add(source1);
                    reportviewer1.LocalReport.EnableExternalImages = true;
                    reportviewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    reportviewer1.Refresh();
                    reportviewer1.RefreshReport();

                }

            }
        }
    }
}
