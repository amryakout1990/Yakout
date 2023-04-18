using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yakout.Archives;
using Yakout.Views;

namespace Yakout.ViewModels
{
    class PrintInvoiceVM:Utilities.ViewModelBase
    {
        private LocalReport _Report;
        private ReportViewer reportviewer1;
        public PrintInvoice ppp;

        public PrintInvoiceVM()
        {
            SaveWithPrint();
            
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
                    //reportviewer1.LocalReport.ReportEmbeddedResource = "Yakout.Reports.Invoice.rdlc";
                    //reportviewer1.LocalReport.DataSources.Clear();
                    //ReportDataSource source = new ReportDataSource();
                    //source.Name = "DataSet1";
                    //source.Value = ds.Tables[0];
                    //reportviewer1.LocalReport.DataSources.Add(source);
                    //reportviewer1.LocalReport.EnableExternalImages = true;
                    //reportviewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    //reportviewer1.Refresh();
                    //reportviewer1.RefreshReport();

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
