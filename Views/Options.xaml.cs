using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Yakout.Views
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : UserControl
    {
        DataTable table;
        
        public Options()
        {
            InitializeComponent();
            Loaded += Options_Loaded;
        }

        private void Options_Loaded(object sender, RoutedEventArgs e)
        {

            using (SqlDataAdapter adapter=new SqlDataAdapter("select * from Restaurant",Models.connectionString.cs))
            {
                table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count>0)
                {
                    tx1.Text = table.Rows[0][1].ToString();
                    tx2.Text = table.Rows[0][2].ToString();
                    tx3.Text = table.Rows[0][3].ToString();
                    tx4.Text = table.Rows[0][4].ToString();
                    tx5.Text = table.Rows[0][5].ToString();
                    tx6.Text = table.Rows[0][6].ToString();
                    byte[] b =(byte[]) table.Rows[0][7];
                    MemoryStream memory = new MemoryStream(b);
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = memory;
                    bitmap.EndInit();
                    img.Source = bitmap;
                }

            }
        }

        private void btnBack4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Select Restaurant Image";
            dialog.Filter = "images(*.PNG;*.JPEG;*.JPG)|*.PNG;*.JPEG;*.JPG";
            if (dialog.ShowDialog() == true)
            {
                tx7.Text = dialog.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(dialog.FileName));
                img.Source = bitmap;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tx1.Text.Trim() == "")
            {
                MessageBox.Show("Enter all fields");
            }
            else if (tx2.Text.Trim() == "")
            {
                MessageBox.Show("Enter all fields");
            }
            else if (tx3.Text.Trim() == "")
            {
                MessageBox.Show("Enter all fields");
            }
            else if (tx4.Text.Trim() == "")
            {
                MessageBox.Show("Enter all fields");
            }
            else if (tx5.Text.Trim() == "")
            {
                MessageBox.Show("Enter all fields");
            }
            else if (tx6.Text.Trim() == "")
            {
                MessageBox.Show("Enter all fields");
            }
            else if (img.Source==null)
            {
                MessageBox.Show("Enter all fields");
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(Models.connectionString.cs))
                {
                    using (SqlCommand command = new SqlCommand("select * from Restaurant", conn))
                    {
                        conn.Open();
                        DataTable table = new DataTable();
                        table.Load(command.ExecuteReader());
                        if (table.Rows.Count > 0)
                        {
                            using (SqlCommand command1 = new SqlCommand("", conn))
                            {
                                string nnn = "";
                                command1.Parameters.Clear();
                                command1.Parameters.Add("@1", SqlDbType.NVarChar).Value = tx1.Text;
                                command1.Parameters.Add("@2", SqlDbType.NVarChar).Value = tx2.Text;
                                command1.Parameters.Add("@3", SqlDbType.NVarChar).Value = tx3.Text;
                                command1.Parameters.Add("@4", SqlDbType.NVarChar).Value = tx4.Text;
                                command1.Parameters.Add("@5", SqlDbType.NVarChar).Value = tx5.Text;
                                command1.Parameters.Add("@6", SqlDbType.NVarChar).Value = tx6.Text;
                                if (tx7.Text!="")
                                {
                                    nnn = ",img=@7";
                                    byte[] bb = (byte[])File.ReadAllBytes(tx7.Text);
                                    command1.Parameters.Add("@7", SqlDbType.Image).Value = bb;
                                }
                                command1.CommandText = "update Restaurant set name=@1,address=@2,phone=@3,printer=@4,line1=@5,line2=@6 " + nnn + " where id=" + 1 + " ";
                                command1.ExecuteNonQuery();
                                MessageBox.Show("Updated Successfully");
                            }
                        }
                        else
                        {
                            using (SqlCommand command2 = new SqlCommand("insert into Restaurant values (" + 1 + ",@1,@2,@3,@4,@5,@6,@7)", conn))
                            {
                                command2.Parameters.Clear();
                                command2.Parameters.Add("@1", SqlDbType.NVarChar).Value = tx1.Text;
                                command2.Parameters.Add("@2", SqlDbType.NVarChar).Value = tx2.Text;
                                command2.Parameters.Add("@3", SqlDbType.NVarChar).Value = tx3.Text;
                                command2.Parameters.Add("@4", SqlDbType.NVarChar).Value = tx4.Text;
                                command2.Parameters.Add("@5", SqlDbType.NVarChar).Value = tx5.Text;
                                command2.Parameters.Add("@6", SqlDbType.NVarChar).Value = tx6.Text;
                                byte[] bb =(byte[])File.ReadAllBytes(tx7.Text);
                                command2.Parameters.Add("@7", SqlDbType.Image).Value = bb;
                                command2.ExecuteNonQuery();
                                MessageBox.Show("Saved Successfully");
                            }

                        }
                    }
                }
            }

        }
        
    }
}
