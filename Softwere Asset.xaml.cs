using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;


namespace ScottishGeln
{
    /// <summary>
    /// Interaction logic for Softwere_Asset.xaml
    /// </summary>
    public partial class Softwere_Asset : Window
    {
        public Softwere_Asset()
        {
            InitializeComponent();
            dataListView.SelectionChanged += ListView_SelectionChanged;
        }

        //back button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();

        }

        private void AddToDatabase_Click(object sender, RoutedEventArgs e)
        {
            database db = new database();

                   string Name = osTextBox.Text;
                string Version = versionTextBox.Text;
                string Manufacture = manTextBox.Text;
                
                string insertQuery = "INSERT INTO SoftwareAssets (id,Name,version,Manufacturer) VALUES (FLOOR(10000 + RAND() * 90000),@Name, @Model, @Manufacture)";
                using (MySqlCommand cmd = new MySqlCommand(insertQuery, db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Model", Version);
                    cmd.Parameters.AddWithValue("@Manufacture", Manufacture);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data added to the database.");
                    }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            
        }

        private void DeleteDatabase_Click(object sender, RoutedEventArgs e)
        {
            database d = new database();

           string ID = IdTextBox.Text;

                string insertQuery = "DELETE FROM SoftwareAssets  WHERE ID = @ID";
                using (MySqlCommand cmd = new MySqlCommand(insertQuery, d.GetConnection()))
                {

                    cmd.Parameters.AddWithValue("@ID", ID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data deleted to the database.");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }           
        }

        private void UpdateDatabase_Click(object sender, RoutedEventArgs e)
        {
            database d = new database();
            string ID = IdTextBox.Text;
            string Name = osTextBox.Text;
            string Version = versionTextBox.Text;
            string Manufacture = manTextBox.Text;

            string insertQuery = "UPDATE SoftwareAssets SET Name = @Name, Version = @Version, Manufacturer = @Manufacture  WHERE ID = @ID";
            using (MySqlCommand cmd = new MySqlCommand(insertQuery, d.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Version", Version);
                cmd.Parameters.AddWithValue("@Manufacture", Manufacture);
            
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data updated in the database.");
                }
                else
                {
                    MessageBox.Show("Error");
                 }
            }
        }
        public class DataItem
        {

            public string Column1 { get; set; }
            public string Column2 { get; set; }
            public string Column3 { get; set; }
            public string Column4 { get; set; }
           
        }
        private void ShowDatabase_Click(object sender, RoutedEventArgs e)
        {
            List<DataItem> data = new List<DataItem>();

            database d = new database();

            string query = "SELECT ID,Name,Version,Manufacturer FROM SoftwareAssets";
            using (MySqlCommand cmd = new MySqlCommand(query, d.GetConnection()))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataItem item = new DataItem
                        {
                            Column1 = reader["Id"].ToString(),
                            Column2 = reader["Name"].ToString(),
                            Column3 = reader["Version"].ToString(),
                            Column4 = reader["Manufacturer"].ToString(),
                            
                        };
                        data.Add(item);
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

                IdTextBox.Text = selectedData.Column1;
                osTextBox.Text = selectedData.Column2;
                versionTextBox.Text = selectedData.Column3;
                manTextBox.Text = selectedData.Column4;
                

            }
        }


        private void GetInfo_Click(object sender, RoutedEventArgs e)
        {
            string OsName = Environment.OSVersion.ToString();
            string VerNum = Environment.Version.ToString();
            string Manufacturer = "Microsoft"; // Since Environment class doesn't provide manufacturer, assume Microsoft

            osTextBox.Text = OsName;
            versionTextBox.Text = VerNum;   
            manTextBox.Text = Manufacturer; 
        }

        private void GetClear_Click(object sender, RoutedEventArgs e)
        {
           IdTextBox.Text = string.Empty;
            osTextBox.Text = string.Empty;  
            versionTextBox.Text = string.Empty;
            manTextBox.Text = string.Empty;

        }
    }
}
