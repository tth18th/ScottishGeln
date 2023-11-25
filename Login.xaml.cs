using System;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace ScottishGeln
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public string Password { get; set; }

        public Login()
        {
            InitializeComponent();

        }

        public class AuthenticationManager
        {
            public bool Authenticate(string user, string password)
            {
                // Perform authentication logic here (check against database, etc.)
                // Return true if authentication succeeds, false otherwise.
                database db = new database();

                try
                {
                    string query = "SELECT * FROM admin WHERE username = @user AND password = @password";
                    using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@password", password);


                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if there is a row in the result set.
                            return reader.HasRows;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return false;
                }
            }
        }

        public static class SessionManager
        {
            private static string currentUser;

            public static string CurrentUser
            {
                get { return currentUser; }
                private set { currentUser = value; }
            }

            public static bool IsUserLoggedIn
            {
                get { return !string.IsNullOrEmpty(CurrentUser); }
            }

            public static void LogIn(string username)
            {
                CurrentUser = username;
            }

            public static void LogOut()
            {
                CurrentUser = null;
            }
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string user = UserTextBox.Text;
            string password = PasswordBox.Password;

            AuthenticationManager authManager = new AuthenticationManager();
            
               
                // Your existing code for MainWindow initialization...
                bool isLoginSuccessful = authManager.Authenticate(user, password);
                if (isLoginSuccessful)
                {
                    SessionManager.LogIn(user);
                    DialogResult = true;
                    Close();
                }
                          
            else
            {

                MessageBox.Show("Invalid username or password.");
            }
          }

    }

    }
