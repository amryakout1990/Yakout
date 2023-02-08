using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Yakout.Commands;
using System.Data.SqlClient;
using System.Data;
using Yakout.Stores;
using Yakout.Utilities;

namespace Yakout.ViewModels
{
    class CategoriesVM : Utilities.ViewModelBase
    {

        private NavigationStore _navigationStore1;

        public NavigationStore _navigationStore
        {
            get { return _navigationStore1; }
            set { _navigationStore1 = value; OnPropertyChanged(); }
        }

        private string _categoryId;

        public string CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; OnPropertyChanged(); }
        }

        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; OnPropertyChanged(); }
        }

        public ICommand SaveCategories { get; }

        public ICommand ShowCategories { get; }

        public ICommand NavigateSetUP { get; }

        private DataTable table;

        private DataTable _gridTable;

        public DataTable GridTable
        {
            get { return _gridTable; }
            set { _gridTable = value; OnPropertyChanged(); }
        }

        private DataRowView _selectedCategory;

        public DataRowView SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; }
        }

        private SqlCommand command;

        public CategoriesVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            CategoryLoad();

            SaveCategories = new ActionCommand(SaveOrUpdate);

            ShowCategories = new ActionCommand(ShowCategory);

            NavigateSetUP = new ActionCommand(NavigateSetUpView);

        }

        private void NavigateSetUpView()
        {
            _navigationStore.CurrentViewModel=new SetUpVM(_navigationStore);
        }

        private void ShowCategory()
        {
            CategoryId = SelectedCategory[0].ToString();

            CategoryName = SelectedCategory[1].ToString();
        }

        private void SaveOrUpdate()
        {
            if (!string.IsNullOrEmpty(CategoryName))
            {
                using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
                {
                    connection.Open();
                    string sql = "select * from Categories where Id='" + CategoryId + "'";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                    {
                        using (table = new DataTable())
                        {
                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                using (command = new SqlCommand("", connection))
                                {
                                    command.CommandText = "update Categories set Name=@1 where Id='" + CategoryId + "'";
                                    command.Parameters.Clear();
                                    command.Parameters.Add("@1", SqlDbType.NVarChar).Value = CategoryName;
                                    command.ExecuteNonQuery();
                                    int NewCategoryId = Convert.ToInt32(CategoryId);
                                    NewCategoryId++;
                                    CategoryId = NewCategoryId + "";
                                    MessageBox.Show("updated Sucessfully");
                                    CategoryName = "";
                                    GetDataGridData();

                                }
                            }
                            else
                            {

                                using (command = new SqlCommand("", connection))
                                {
                                    command.CommandText = "insert into Categories values (@1,@2)";
                                    command.Parameters.Clear();
                                    command.Parameters.Add("@1", SqlDbType.NVarChar).Value = CategoryId;
                                    command.Parameters.Add("@2", SqlDbType.NVarChar).Value = CategoryName;
                                    command.ExecuteNonQuery();
                                    int NewCategoryId = Convert.ToInt32(CategoryId);
                                    NewCategoryId++;
                                    CategoryId = NewCategoryId + "";
                                    MessageBox.Show("Saved Sucessfully");
                                    CategoryName = "";
                                    GetDataGridData();

                                }
                            }


                        }
                    }


                }

            }
            else
            {
                MessageBox.Show("Enter Category Name ");
            }
        }

        private void CategoryLoad()
        {
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                connection.Open();

                string sql = "select max(Id) from Categories";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    using (table = new DataTable())
                    {
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            int NewCategoryId = Convert.ToInt32(table.Rows[0][0].ToString());
                            NewCategoryId++;
                            CategoryId = NewCategoryId + "";
                            CategoryName = "";
                        }
                        else
                        {
                            CategoryId = "1";
                            CategoryName = "";
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

                string sqlAll = "select * from Categories";

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
