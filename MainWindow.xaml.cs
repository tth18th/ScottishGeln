using System;
using System.Windows;
using static ScottishGeln.Login;

namespace ScottishGeln
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
             
            InitializeComponent();

            // Check if the user is authenticated during MainWindow initialization
            if (!IsUserAuthenticated())
            {
                // If not authenticated, show the login window
                //Windowlog();
            }
        }
        private bool IsUserAuthenticated()
        {
            // Check if the user is authenticated (e.g., by checking the session)
            // Return true if authenticated, false otherwise
            return Login.SessionManager.IsUserLoggedIn;
        }

             
        public void Windowlog()
        {
            // Show the Login window as a dialog
            Login login = new Login();
            bool? loginResult = login.ShowDialog();
            if (loginResult == true)
            {
                // If successful, continue initializing the MainWindow
                InitializeComponent();
                Show();

            }
            else
            {
                // If login fails, you might want to exit the application or take appropriate action
                Application.Current.Shutdown();
            }

        }

        private void Assign_Click(object sender, RoutedEventArgs e)
        {
            Department da = new Department();
            da.Show();
            this.Close();
        }
        // this button shows assets window

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Assets a = new Assets();
            a.Show();
            this.Close();
        }
        // this button shows Employee
        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            Employee ea = new Employee();
            ea.Show();
            this.Close();

        }
        //this show softwere asset
        private void Softwere_Click(object sender, RoutedEventArgs e)
        {
            Softwere_Asset sa = new Softwere_Asset();
            sa.Show();
            this.Close();

        }
    }
}
