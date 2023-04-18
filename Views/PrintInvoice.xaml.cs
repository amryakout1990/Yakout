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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yakout.Archives;

namespace Yakout.Views
{
    /// <summary>
    /// Interaction logic for PrintInvoice.xaml
    /// </summary>
    public partial class PrintInvoice : UserControl
    {
        public PrintInvoice()
        {
            InitializeComponent();
            Loaded += PrintInvoice_Loaded;
            //SaveWithPrint();
        }

        private void PrintInvoice_Loaded(object sender, RoutedEventArgs e)
        {

            //Window1 www = new Window1();
            //www.ShowDialog();
        }

        private void SaveWithPrint()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();
                using (SqlCommand command3 = new SqlCommand("", connection))
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add("t1");
                    command3.CommandType = CommandType.StoredProcedure;
                    command3.CommandText = "Invoice_Details";
                    command3.Parameters.Clear();
                    command3.Parameters.Add("@InvoiceId", SqlDbType.Int).Value = 2;
                    ds.Tables["t1"].Load(command3.ExecuteReader());
                    reportviewer1.LocalReport.ReportEmbeddedResource = "Yakout.Reports.Invoice.rdlc";
                    reportviewer1.LocalReport.DataSources.Clear();
                    ReportDataSource source = new ReportDataSource();
                    source.Name = "DataSet1";
                    source.Value = ds.Tables[0];
                    reportviewer1.LocalReport.DataSources.Add(source);
                    reportviewer1.LocalReport.EnableExternalImages = true;
                    reportviewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    reportviewer1.Refresh();
                    reportviewer1.RefreshReport();

                    //ppp.reportviewer1.LocalReport.ReportEmbeddedResource = "Yakout.Reports.Invoice.rdlc";
                    //ppp.reportviewer1.LocalReport.DataSources.Clear();
                    //ReportDataSource source = new ReportDataSource();
                    //source.Name = "DataSet1";
                    //source.Value = ds.Tables[0];
                    //ppp.reportviewer1.LocalReport.DataSources.Add(source);
                    //ppp.reportviewer1.LocalReport.EnableExternalImages = true;
                    //ppp.reportviewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    //ppp.reportviewer1.Refresh();
                    //ppp.reportviewer1.RefreshReport();

                }



            }
        }

    }
}
