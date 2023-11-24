using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;

namespace ScottishGeln
{
    /// <summary>
    /// Interaction logic for Department.xaml
    /// </summary>
    public partial class Department : Window
    {
        public Department()
        {
            InitializeComponent();
            

        }
        public class DataItem
        {

            public string Column1 { get; set; }
            public string Column2 { get; set; }
            public string Column3 { get; set; }
            public string Column4 { get; set; }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<DataItem> data = new List<DataItem>();
            database d = new database();

            string query = "SELECT id, Asset_name, Employees_name, department FROM Assaigne_asset";
            using (MySqlCommand cmd = new MySqlCommand(query, d.GetConnection()))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataItem item = new DataItem
                        {
                            Column1 = reader["id"].ToString(),
                            Column2 = reader["Asset_name"].ToString(),
                            Column3 = reader["Employees_name"].ToString(),
                            Column4 = reader["department"].ToString(),

                        };
                        data.Add(item);
                    }
                }
            }


              AssetListView.ItemsSource = data;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
    }
}
