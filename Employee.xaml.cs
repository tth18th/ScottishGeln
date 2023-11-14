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
using static ScottishGeln.Assets;
using MySql.Data.MySqlClient;

namespace ScottishGeln
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
            dataListView.SelectionChanged += ListView_SelectionChanged;
        }
        public class DataItem
        {

            public string Column1 { get; set; }
            public string Column2 { get; set; }
            public string Column3 { get; set; }
            public string Column4 { get; set; }
            public string Column5 { get; set; }
           
        }

        private void Show_Click_2(object sender, RoutedEventArgs e)
        {
            List<DataItem> data = new List<DataItem>();


            string connectionString = "server=lochnagar.abertay.ac.uk;username=sql2100258;password=reduces dump risk baths;database=sql2100258;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id,firstname,lastname,email,department FROM staff";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataItem item = new DataItem
                            {
                                Column1 = reader["id"].ToString(),
                                Column2 = reader["firstname"].ToString(),
                                Column3 = reader["lastname"].ToString(),
                                Column4 = reader["email"].ToString(),
                                Column5 = reader["department"].ToString(),
                               
                            };
                            data.Add(item);
                        }
                    }
                }
            }

            dataListView.ItemsSource = data;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataListView.SelectedItem != null)
            {
                DataItem selectedData = (DataItem)dataListView.SelectedItem;

                idTextbox.Text = selectedData.Column1;
                fnTextbox.Text = selectedData.Column2;
                LnTextbox.Text = selectedData.Column3;
                emTextbox.Text = selectedData.Column4;  
                depTextbox.Text = selectedData.Column5; 

            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
