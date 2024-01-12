using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace LogInForm
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        //here we have the button submit method 
        private void Submit(object sender, RoutedEventArgs e)
        {
            string sqlCon_ = @"Data Source=LAB108PC23\SQLEXPRESS; Initial Catalog=LogIn; Integrated Security=True;";
            SqlConnection sqlCon = new SqlConnection(sqlCon_);

            try
            {
                //if the connection to the database is open
                if (sqlCon.State== ConnectionState.Closed)
                    sqlCon.Open();

                string Query = "SELECT COUNT(1) FROM UserCred Where Username=@Username AND Password=@Password";
                SqlCommand cmd = new SqlCommand(Query, sqlCon);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", passwordBox.Password);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if(count == 1)
                {
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
                 else
                {
                    MessageBox.Show("Password or username not correct.Try again!");
                }


            }
            catch (Exception ex)
            {

               MessageBox.Show(ex.Message);
            }
        }
    }
}
