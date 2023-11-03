using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
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
    /// Interaction logic for Assets.xaml
    /// </summary>
    public partial class Assets : Window
    {
        public Assets()
        {
            InitializeComponent();
        }

        private void AddToDatabase_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=lochnagar.abertay.ac.uk;username=sql2100258;password=reduces dump risk baths;database=sql2100258;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string name = nameTextBox.Text;
                    string department = (departmentComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                    string description = descriptionTextBox.Text;

                    string insertQuery = "INSERT INTO test (Id, Name, Department, Description) VALUES (FLOOR(10000 + RAND() * 90000),@Name, @Department, @Description)";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Department", department);
                        cmd.Parameters.AddWithValue("@Description", description);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data added to the database.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                connection.Close();
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


            string connectionString = "server=lochnagar.abertay.ac.uk;username=sql2100258;password=reduces dump risk baths;database=sql2100258;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Department, Description FROM test";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataItem item = new DataItem
                            {
                                Column1 = reader["Id"].ToString(),
                                Column2 = reader["Name"].ToString(),
                                Column3 = reader["Department"].ToString(),
                                Column4 = reader["Description"].ToString()

                            };
                            data.Add(item);
                        }
                    }
                }
            }

            dataListView.ItemsSource = data;
        }

        private void GetInfo_Click(object sender, RoutedEventArgs e)
        {
            // Get system information
            string systemName = Environment.MachineName;
            string systemModel = GetSystemModel();
            string systemManufacturer = Environment.OSVersion.Version.ToString();
            string systemType = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
            string ipAddress = GetLocalIPAddress();

            // Fill the textboxes with the system information
            nameTextBox.Text = systemName;
            ModelTextBox.Text = systemModel;
            ManTextBox.Text = systemManufacturer;
            SysTextBox.Text = systemType;
            IpTextBox.Text = ipAddress;
        }
        private string GetSystemModel()
        {
            return Environment.GetEnvironmentVariable("COMPUTERNAME");
        }

        private string GetSystemManufacturer()
        {
            return Environment.GetEnvironmentVariable("USERDOMAIN");
        }

        private string GetLocalIPAddress()
        {
            string localIP = "";
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up && netInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    foreach (UnicastIPAddressInformation address in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (address.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            localIP = address.Address.ToString();
                            break;
                        }
                    }
                }
            }
            return localIP;
        }
    }
}
