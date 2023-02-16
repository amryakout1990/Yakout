using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using Yakout.Commands;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using Yakout.Stores;
using Yakout.Views;

namespace Yakout.ViewModels
{
    class ItemsVM : Utilities.ViewModelBase
    {
        byte[] databaseByteArray;

        private SqlCommand command;

        private DataTable _table;

        public DataTable table
        {
            get { return _table; }
            set { _table = value; OnPropertyChanged(); }
        }

        private DataTable _tableCategories;

        public DataTable tableCategories
        {
            get { return _tableCategories; }
            set { _tableCategories = value; OnPropertyChanged(); }
        }

        public ICommand OpenFD { get; private set; }
        public ICommand SaveItemCommand { get; private set; }
        public ICommand NewItemCommand { get; private set; }
        public ICommand NavigateSetUpCommand { get; private set; }
        public ICommand firstCommand { get; private set; }
        public ICommand backCommand { get; private set; }
        public ICommand nextCommand { get; private set; }
        public ICommand lastCommand { get; private set; }
        public ICommand NavigateItemsSelectCommand { get; private set; }

        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; OnPropertyChanged(); }
        }

        private string _price;

        public string Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        private string _notes;

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; OnPropertyChanged(); }
        }

        private int _itemId;

        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; OnPropertyChanged(); }
        }

        private DataRowView _categoryId;

        public DataRowView CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; OnPropertyChanged(); }
        }

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged(); }
        }


        private string _selItem;

        public string SelItem
        {
            get { return _selItem; }
            set { _selItem = value; OnPropertyChanged(); }
        }

        private byte[] _imageToByteArray;

        public byte[] ImageToByteArray
        {
            get
            {
                if (ImagePath != null)
                    return File.ReadAllBytes(ImagePath);
                else
                    return _imageToByteArray;
            }
            set { _imageToByteArray = value; OnPropertyChanged(); }
        }

        private BitmapSource _bitmapSource;

        public BitmapSource bitmapSource
        {
            get { return _bitmapSource; }
            set { _bitmapSource = value; OnPropertyChanged(); }
        }

        private readonly NavigationStore _navigationStore;

        public ItemsVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            LoadCategories();

            LoadItems();

            OpenFD = new ActionCommand(OpenFiDi);

            SaveItemCommand = new ActionCommand(SaveOrUpdateItem);

            NewItemCommand = new ActionCommand(NewItem);

            firstCommand = new ActionCommand(first);
            backCommand = new ActionCommand(back);
            nextCommand = new ActionCommand(next);
            lastCommand = new ActionCommand(last); ;
            NavigateItemsSelectCommand = new ActionCommand(NavigateItemsSelect);
            NavigateSetUpCommand = new ActionCommand(NavigateSetUp);
        }

        private void NavigateItemsSelect()
        {
            _navigationStore.CurrentViewModel=new ItemsSelectVM();
        }

        private void first()
        {
            ItemId = 1;
            ShowItem(ItemId);
        }

        private void back()
        {
            int NewItemId =ItemId;
            if (ItemId > 1)
            {
                ItemId--;
                ShowItem(ItemId);
            }
        }
        private void next()
        {
            int max =Convert.ToInt32( getMaxId());
            if (ItemId != max)
            {
                ItemId++;
                ShowItem(ItemId);
            }
        }
        private void last()
        {
            ItemId = Convert.ToInt32(getMaxId());
            ShowItem(ItemId);
            
        }


        private BitmapSource source(Bitmap bitmap)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                                                         BitmapSizeOptions.FromEmptyOptions());
        }

        private void BitmapSourceToByteArray(BitmapSource bitmapSource)
        {
            //Bitmap map = new Bitmap(;
        }

        private void NavigateSetUp()
        {
            _navigationStore.CurrentViewModel = new SetUpVM(_navigationStore);
        }

        private void NewItem()
        {
            clearItem();
        }

        private void LoadItems()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();

                string sql = "select max(ItemId) from Items";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    using (table = new DataTable())
                    {
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            string sNewItemId = table.Rows[0][0].ToString();

                            if (string.IsNullOrEmpty(sNewItemId))
                            {
                                ItemId = 1;
                            }
                            else
                            {
                                ItemId = 1;
                                ShowItem(ItemId);
                            }
                        }
                        else
                        {
                            ItemId = 1;
                        }
                    }
                }

            }
        }

        private string getMaxId()
        {
            string sNewItemId;

            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();

                string sql = "select max(ItemId) from Items";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    using (table = new DataTable())
                    {
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            sNewItemId = table.Rows[0][0].ToString();

                            if (string.IsNullOrEmpty(sNewItemId))
                            {
                                sNewItemId = "1";
                            }
                            else
                            {
                                sNewItemId = table.Rows[0][0].ToString();
                            }
                        }
                        else
                        {
                            sNewItemId = "1";
                        }
                    }
                }

            }
            return  sNewItemId;

        }
        private void ShowItem(int itemId)
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                string sql1 = "select * from Items where ItemId=" + itemId + "";

                using (SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, connection))
                {
                    using (table = new DataTable())
                    {
                        adapter1.Fill(table);

                        if (table.Rows.Count>0)
                        {
                            ItemId = Convert.ToInt32(table.Rows[0][0]);

                            ItemName = table.Rows[0][1].ToString();

                            SelItem = table.Rows[0][2].ToString();

                            Price = table.Rows[0][3].ToString();

                            Notes = table.Rows[0][4].ToString();


                            databaseByteArray = (byte[])table.Rows[0][5];

                            MemoryStream memory = new MemoryStream(databaseByteArray);

                            bitmapSource = source(new Bitmap(memory));

                        }



                    }
                }

            }
        }


        private void LoadCategories()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                string sql = "select * from Categories";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    tableCategories = new DataTable();

                    adapter.Fill(tableCategories);
                }
            }

        }

        private void OpenFiDi()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images|*.Png;*.jpg;*.jpeg";
            if (dialog.ShowDialog() == true)
            {
                ImagePath = dialog.FileName;

                bitmapSource = source(new Bitmap(ImagePath));

                Bitmap bitmap = new Bitmap(ImagePath);

                MemoryStream memory = new MemoryStream();

                
            }
        }

        private void clearItem()
        {
            ItemName = "";

            Price = "";

            Notes = "";

            ImagePath = "";

            bitmapSource = null;

            databaseByteArray = null;

            ImagePath = "";

            SelItem = "0";

            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();

                string sql = "select max(ItemId) from Items";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    using (table = new DataTable())
                    {
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            string sNewItemId2 = table.Rows[0][0].ToString();

                            if (string.IsNullOrEmpty(sNewItemId2))
                            {
                                ItemId = 1;
                            }
                            else
                            {
                                ItemId = Convert.ToInt32(sNewItemId2);
                                ItemId++;

                            }
                        }
                        else
                        {
                            ItemId = 1;
                        }
                    }
                }

            }


        }

        private void SaveOrUpdateItem()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
                {
                    connection.Open();

                    string sql = "select * from Items where ItemId='" + ItemId + "'";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                    {
                        using (table = new DataTable())
                        {
                            adapter.Fill(table);

                        if (table.Rows.Count> 0)
                            {
                                using (command = new SqlCommand("", connection))
                                {
                                command.CommandText = "update Items set ItemName=@1,CategoryId=@2,Price=@3,Notes=@4,Image=@5 where ItemId='" + ItemId + "'";
                                command.Parameters.Clear();
                                command.Parameters.Add("@1", SqlDbType.NVarChar).Value = ItemName;
                                command.Parameters.Add("@2", SqlDbType.NVarChar).Value = CategoryId[0].ToString();
                                command.Parameters.Add("@3", SqlDbType.NVarChar).Value = Price;
                                command.Parameters.Add("@4", SqlDbType.NVarChar).Value = Notes;
                                if (ImagePath!=null)
                                {
                                databaseByteArray = File.ReadAllBytes(ImagePath);
                                }
                                command.Parameters.Add("@5", SqlDbType.Image).Value = databaseByteArray;
                                command.ExecuteNonQuery();
                                MessageBox.Show("updated Sucessfully");
                                clearItem();

                            }
                            }
                            else
                            {

                                using (command = new SqlCommand("", connection))
                                {
                                    command.CommandText = "insert into Items values (@1,@2,@3,@4,@5,@6)";
                                    command.Parameters.Clear();
                                    command.Parameters.Add("@1", SqlDbType.NVarChar).Value =ItemId ;
                                    command.Parameters.Add("@2", SqlDbType.NVarChar).Value = ItemName;
                                    command.Parameters.Add("@3", SqlDbType.NVarChar).Value = CategoryId[0].ToString();
                                    command.Parameters.Add("@4", SqlDbType.NVarChar).Value = Price;
                                    command.Parameters.Add("@5", SqlDbType.NVarChar).Value = Notes;
                                    command.Parameters.Add("@6", SqlDbType.Image).Value = ImageToByteArray;
                                    command.ExecuteNonQuery();
                                    MessageBox.Show("Saved Sucessfully");
                                    clearItem();

                                }
                            }


                        }
                    }


                }

        }
    }
}
