using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Yakout.Commands;
using Yakout.Stores;

namespace Yakout.ViewModels
{
    class OptionsVM : Utilities.ViewModelBase
    {
        byte[] databaseByteArray;
        DataTable table;
        private readonly NavigationStore _navigationStore;

        private string _restaurantName;
        public string RestaurantName
        {
            get { return _restaurantName; }
            set { _restaurantName = value;OnPropertyChanged(); }
        }

        private string _restaurantAddress;
        public string RestaurantAddress
        {
            get { return _restaurantAddress; }
            set { _restaurantAddress = value; OnPropertyChanged(); }
        }
        private string _restaurantPhone;
        public string RestaurantPhone
        {
            get { return _restaurantPhone; }
            set { _restaurantPhone = value; OnPropertyChanged(); }
        }

        private string _printer;
        public string Printer
        {
            get { return _printer; }
            set { _printer = value; OnPropertyChanged(); }
        }

        private string _rLine1;
        public string RLine1
        {
            get { return _rLine1; }
            set { _rLine1 = value; OnPropertyChanged(); }
        }

        private string _rLine2;
        public string RLine2
        {
            get { return _rLine2; }
            set { _rLine2 = value; OnPropertyChanged(); }
        }

        private BitmapSource _bitmapSource;
        public BitmapSource bitmapSource
        {
            get { return _bitmapSource; }
            set { _bitmapSource = value; OnPropertyChanged(); }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged(); }
        }

        

        public ICommand btnSelectCommand { get;private set ; }
        public ICommand btnBackCommand { get;private set ; }
        public ICommand btnSaveCommand { get;private set ; }

        public OptionsVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            LoadOptions();
            btnSelectCommand = new ActionCommand(btnSelect);
            btnBackCommand = new ActionCommand(btnBack);
            btnSaveCommand = new ActionCommand(btnsave);
        }

        private void btnsave()
        {
            if (RestaurantName.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("Enter all fields");
            }
            else if (RestaurantAddress.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("Enter all fields");
            }
            else if (RestaurantPhone.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("Enter all fields");
            }
            else if (Printer.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("Enter all fields");
            }
            else if (RLine1.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("Enter all fields");
            }
            else if (RLine2.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("Enter all fields");
            }
            else if (bitmapSource == null)
            {
                System.Windows.Forms.MessageBox.Show("Enter all fields");
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
                                command1.Parameters.Add("@1", SqlDbType.NVarChar).Value = RestaurantName;
                                command1.Parameters.Add("@2", SqlDbType.NVarChar).Value = RestaurantAddress;
                                command1.Parameters.Add("@3", SqlDbType.NVarChar).Value = RestaurantPhone;
                                command1.Parameters.Add("@4", SqlDbType.NVarChar).Value = Printer;
                                command1.Parameters.Add("@5", SqlDbType.NVarChar).Value = RLine1;
                                command1.Parameters.Add("@6", SqlDbType.NVarChar).Value = RLine2 ;
                                if (!string.IsNullOrEmpty(ImagePath) )
                                {
                                    nnn = ",img=@7";
                                    byte[] bb = (byte[])File.ReadAllBytes(ImagePath);
                                    command1.Parameters.Add("@7", SqlDbType.Image).Value = bb;
                                }
                                command1.CommandText = "update Restaurant set name=@1,address=@2,phone=@3,printer=@4,line1=@5,line2=@6 " + nnn + " where id=" + 1 + " ";
                                command1.ExecuteNonQuery();
                                System.Windows.Forms.MessageBox.Show("Updated Successfully");
                            }
                        }
                        else
                        {
                            using (SqlCommand command2 = new SqlCommand("insert into Restaurant values (" + 1 + ",@1,@2,@3,@4,@5,@6,@7)", conn))
                            {
                                command2.Parameters.Clear();
                                command2.Parameters.Add("@1", SqlDbType.NVarChar).Value = RestaurantName;
                                command2.Parameters.Add("@2", SqlDbType.NVarChar).Value = RestaurantAddress;
                                command2.Parameters.Add("@3", SqlDbType.NVarChar).Value = RestaurantPhone;
                                command2.Parameters.Add("@4", SqlDbType.NVarChar).Value = Printer;
                                command2.Parameters.Add("@5", SqlDbType.NVarChar).Value = RLine1;
                                command2.Parameters.Add("@6", SqlDbType.NVarChar).Value = RLine2;
                                byte[] bb = (byte[])File.ReadAllBytes(ImagePath);
                                command2.Parameters.Add("@7", SqlDbType.Image).Value = bb;
                                command2.ExecuteNonQuery();
                                System.Windows.Forms.MessageBox.Show("Saved Successfully");
                            }

                        }
                    }
                }
            }

        }
        private void btnBack()
        {
            _navigationStore.CurrentViewModel = new MainBackGroundVM();
        }

        private BitmapSource source(Bitmap bitmap)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                                                         BitmapSizeOptions.FromEmptyOptions());
        }

        private void LoadOptions()
        {

            using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Restaurant", Models.connectionString.cs))
            {
                table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    RestaurantName = table.Rows[0][1].ToString();
                    RestaurantAddress = table.Rows[0][2].ToString();
                    RestaurantPhone = table.Rows[0][3].ToString();
                    Printer = table.Rows[0][4].ToString();
                    RLine1 = table.Rows[0][5].ToString();
                    RLine2 = table.Rows[0][6].ToString();

                    databaseByteArray = (byte[])table.Rows[0][7];
                    MemoryStream memory = new MemoryStream(databaseByteArray);
                    bitmapSource = source(new Bitmap(memory));
                }

            }
        }

        private void btnSelect()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Select Restaurant Image";
            dialog.Filter = "images(*.PNG;*.JPEG;*.JPG)|*.PNG;*.JPEG;*.JPG";
            if (dialog.ShowDialog() ==DialogResult.OK)
            {
                ImagePath = dialog.FileName;

                bitmapSource = source(new Bitmap(ImagePath));

                Bitmap bitmap = new Bitmap(ImagePath);

                MemoryStream memory = new MemoryStream();
            }
        }

    }
}
