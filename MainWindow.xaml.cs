using System.Windows;

namespace ScottishGeln
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            //windowlog();
            InitializeComponent();

        }
        

        public void windowlog()
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
        // this button shoews assets window

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Assets a = new Assets();
            a.Show();
            this.Close();
        }
        // this button shoews Employee
        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            Employee ea = new Employee();
            ea.Show();
            this.Close();

        }

        private void Softwere_Click(object sender, RoutedEventArgs e)
        {
            Softwere_Asset sa = new Softwere_Asset();
            sa.Show();
            this.Close();

        }
    }
}
