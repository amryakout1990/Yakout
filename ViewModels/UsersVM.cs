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
using Yakout.Models;
using Yakout.Stores;
using Yakout.Utilities;
using Yakout.Views;

namespace Yakout.ViewModels
{
    class UsersVM : Utilities.ViewModelBase
    {
        private readonly SelectedUserStore _selectedUserStore;
        private readonly NavigationStore _navigationStore;
        private DataTable table;
        private int index;
        private SqlCommand command;
        string New_id;


        private string _userName;
        public string UserName 
        {
            get { return _userName; }   
            set 
                {
                   if (!string.Equals(_userName, value))
                   {
                    _userName = value;
                      OnPropertyChanged();
                   }
                }
        }

        private string _password;
        public string Password
        {
            get 
            {
                    return _password;               
            }
            set
            {
                if (!string.Equals(_password, value))
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (!string.Equals(_fullName, value))
                {
                    _fullName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _jobDes;
        public string JobDes
        {
            get { return _jobDes; }
            set
            {
                if (!string.Equals(_jobDes, value))
                {
                    _jobDes = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (!string.Equals(_email, value))
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (!string.Equals(_phone, value))
                {
                    _phone = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand NavigateSetUpCommand { get; }
        public ICommand NavigateUsersSelectCommand { get; }

        public ICommand UsersNewButtonCommand => new UsersButtonsCommand(NewButton);
        public ICommand UsersFirstButtonCommand => new UsersButtonsCommand(getDataFirst);
        public ICommand UsersSaveButtonCommand => new UsersSave(this,getDataSave);
        public ICommand UsersBackButtonCommand => new UsersButtonsCommand(getDataBack);
        public ICommand UsersNextButtonCommand => new UsersButtonsCommand(getDataNext);
        public ICommand UsersLastButtonCommand => new UsersButtonsCommand(getDataLast);

        public UsersVM(NavigationStore navigationStore,SelectedUserStore selectedUserStore)
        {
            index = 1;

            _navigationStore = navigationStore;
            _selectedUserStore = selectedUserStore;
            _userName = _selectedUserStore.SelectedUser.UserName;
            _password = _selectedUserStore.SelectedUser.Password;
            _fullName = _selectedUserStore.SelectedUser.FullName;
            _jobDes = _selectedUserStore.SelectedUser.JobDes;
            _email = _selectedUserStore.SelectedUser.Email;
            _phone = _selectedUserStore.SelectedUser.Phone;

            NavigateSetUpCommand = new NavigateCommand<SetUpVM>(new NavigationService<SetUpVM>(navigationStore, () => new SetUpVM(_navigationStore)));
            NavigateUsersSelectCommand = new NavigateCommand<UserSelectVM>(new NavigationService<UserSelectVM>(navigationStore, () => new UserSelectVM(_navigationStore)));

            _selectedUserStore.SelectedUserChanged += _selectedUserStore_SelectedUserChanged;
        }

        private void _selectedUserStore_SelectedUserChanged()
        {
            //OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(FullName));
            OnPropertyChanged(nameof(JobDes));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Phone));
        }

        public override void Dispose()
        {
            _selectedUserStore.SelectedUserChanged -= _selectedUserStore_SelectedUserChanged;
            base.Dispose();
        }
        private void getDataSave(object ooo)
        {
            if (index != 0)
            {
                using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter("select max(id)+1 from Users", connection))
                    {
                        table = new DataTable();
                        adapter.Fill(table);
                        int id;
                         New_id = table.Rows[0][0].ToString();
                        if (string.IsNullOrEmpty(New_id))
                        {
                            id = 1;
                            inOrUpdate(connection, id);
                        }
                        else
                        {
                            id = Convert.ToInt32(New_id);
                            inOrUpdate(connection, id);
                        }

                    }


                }

            }
            else
            {
                using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
                {
                    inOrUpdate(connection, index);
                }
            }

        }
        private void inOrUpdate(SqlConnection connection, int id)
        {
            using (command = new SqlCommand("", connection))
            {
                if (index==0)
                {
                    using (command = new SqlCommand("", connection))
                    {
                    command.CommandText = "insert into Users values(@1,@2,@3,@4,@5,@6,@7)";
                    command.Parameters.Clear();
                    command.Parameters.Add("@1", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@2", SqlDbType.NVarChar).Value = UserName;
                    command.Parameters.Add("@3", SqlDbType.NVarChar).Value = Password;
                    command.Parameters.Add("@4", SqlDbType.NVarChar).Value = FullName;
                    command.Parameters.Add("@5", SqlDbType.NVarChar).Value = JobDes;
                    command.Parameters.Add("@6", SqlDbType.NVarChar).Value = Email;
                    command.Parameters.Add("@7", SqlDbType.NVarChar).Value = Phone;
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Saved Successfuly");
                    }
                }
                else
                {
                    using (command = new SqlCommand("", connection))
                    {
                    command.CommandText = "update Users set userName = @2 , password =@3 , fullName = @4 , jobDes = @5 , email = @6 , phone = @7 where id = " +Convert.ToInt32(New_id)+"";
                    command.Parameters.Clear();
                    command.Parameters.Add("@2", SqlDbType.NVarChar).Value = UserName;
                    command.Parameters.Add("@3", SqlDbType.NVarChar).Value = Password;
                    command.Parameters.Add("@4", SqlDbType.NVarChar).Value = FullName;
                    command.Parameters.Add("@5", SqlDbType.NVarChar).Value = JobDes;
                    command.Parameters.Add("@6", SqlDbType.NVarChar).Value = Email;
                    command.Parameters.Add("@7", SqlDbType.NVarChar).Value = Phone;
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("updated Successfuly");
                    MessageBox.Show(UserName);
                    MessageBox.Show(New_id);
                    }

                }
            }
        }

        private void NewButton(object ooo)
        {
            index = 0;

            UserName = "";
            Password = "";
            FullName = "";
            JobDes = "";
            Email = "";
            Phone = "";

        }

        private void getDataFirst(object ooo)
        {
            index = 1;
            table = new DataTable();
            using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users where id=" + index + "", connection))
                {
                    table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        UserName = table.Rows[0][1].ToString();
                        Password = table.Rows[0][2].ToString();
                        FullName = table.Rows[0][3].ToString();
                        JobDes = table.Rows[0][4].ToString();
                        Email = table.Rows[0][5].ToString();
                        Phone = table.Rows[0][6].ToString();
                    }

                }
            }
        }

        private void getDataLast(object ooo)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users", Models.connectionString.cs))
            {
                table = new DataTable();
                adapter.Fill(table);
                index = table.Rows.Count;
                using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
                {
                    using (SqlDataAdapter adapter1 = new SqlDataAdapter("select * from Users where id=" + index + "", connection))
                    {
                        table = new DataTable();
                        adapter1.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            UserName = table.Rows[0][1].ToString();
                            Password = table.Rows[0][2].ToString();
                            FullName = table.Rows[0][3].ToString();
                            JobDes = table.Rows[0][4].ToString();
                            Email = table.Rows[0][5].ToString();
                            Phone = table.Rows[0][6].ToString();
                        }

                    }
                }
            }

        }

        private void getDataBack(object ooo)
        {
            if (index > 1)
            {
                index--;
                using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
                {
                    using (SqlDataAdapter adapter1 = new SqlDataAdapter("select * from Users where id=" + index + "", connection))
                    {
                        table = new DataTable();
                        adapter1.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            UserName = table.Rows[0][1].ToString();
                            Password = table.Rows[0][2].ToString();
                            FullName = table.Rows[0][3].ToString();
                            JobDes = table.Rows[0][4].ToString();
                            Email = table.Rows[0][5].ToString();
                            Phone = table.Rows[0][6].ToString();
                        }

                    }
                }
            }

        }

        private void getDataNext(object ooo)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter("select * from Users", Models.connectionString.cs))
            {
                table = new DataTable();
                adapter.Fill(table);
                if (index < table.Rows.Count && index > 0)
                {
                    index++;
                    using (SqlConnection connection = new SqlConnection(Models.connectionString.cs))
                    {
                        using (SqlDataAdapter adapter1 = new SqlDataAdapter("select * from Users where id=" + index + "", connection))
                        {
                            table = new DataTable();
                            adapter1.Fill(table);
                            if (table.Rows.Count > 0)
                            {
                                UserName = table.Rows[0][1].ToString();
                                Password = table.Rows[0][2].ToString();
                                FullName = table.Rows[0][3].ToString();
                                JobDes = table.Rows[0][4].ToString();
                                Email = table.Rows[0][5].ToString();
                                Phone = table.Rows[0][6].ToString();
                            }

                        }
                    }
                }
            }

        }


    }
}
