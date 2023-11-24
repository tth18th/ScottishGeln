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
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string user = UserTextBox.Text;
            string password = PasswordBox.Password;

            AuthenticationManager authManager = new AuthenticationManager();
            if (authManager.Authenticate(user, password))
            {
                // Authentication succeeded, continue with the initialization of the main window.
                InitializeComponent();
                // Your existing code for MainWindow initialization...
                bool isLoginSuccessful = authManager.Authenticate(user, password);

                // Set DialogResult based on the authentication result
                DialogResult = isLoginSuccessful;
                Close();
            }
            // Optionally, close the login window.

            else
            {

                MessageBox.Show("Invalid username or password.");
            }
                    }

    }

    }
