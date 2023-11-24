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

           windowlog();
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

        // this button shoews assets window
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Assets a = new Assets();
            a.Show();
            this.Close();

        }
        // this button shoews Employee
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Employee ea = new Employee();
            ea.Show();
            this.Close();
        }
        // this button shoews
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Department da = new Department();
            da.Show();
            this.Close();
        }
    }
}
