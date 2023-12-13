using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;


namespace ScottishGeln
{
    
    public partial class Assets : Window
    {
        public Assets()
        {
            InitializeComponent();

            dataListView.SelectionChanged += ListView_SelectionChanged;
        }
        // add info to database
        private void AddToDatabase_Click(object sender, RoutedEventArgs e)
        {
           database db = new database();
            
                try
                {                 
                    string name = nameTextBox.Text;
                    string Model = ModelTextBox.Text;
                    string Manufacture = ManTextBox.Text;
                    string Systeminfo = SysTextBox.Text;
                    string IpAddress = IpTextBox.Text;
                    string PDate = DateTextBox.Text;
                    string Department = (departmentComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                    string Note = descriptionTextBox.Text;
                    Calendartxt.SelectedDate = DateTime.Now.AddDays(1);

                    string insertQuery = "INSERT INTO Assets (ID,Name,Model,Manufacture,Systeminfo,IpAddress,PDate,Department,Note) VALUES (FLOOR(10000 + RAND() * 90000),@Name, @Model, @Manufacture,@Systeminfo,@IpAddress,@PDate,@Department,@Note)";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, db.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Model", Model);
                        cmd.Parameters.AddWithValue("@Manufacture", Manufacture);
                        cmd.Parameters.AddWithValue("@Systeminfo", Systeminfo);
                        cmd.Parameters.AddWithValue("@IpAddress", IpAddress);
                        cmd.Parameters.AddWithValue("@PDate", PDate);
                        cmd.Parameters.AddWithValue("@Department", Department);
                        cmd.Parameters.AddWithValue("@Note", Note);

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
                
        }
        // getter and setter for the column where show assets info 
        public class DataItem
        {
            public string Column1 { get; set; }
            public string Column2 { get; set; }
            public string Column3 { get; set; }
            public string Column4 { get; set; }
            public string Column5 { get; set; }
            public string Column6 { get; set; }
            public string Column7 { get; set; }
            public string Column8 { get; set; }
            public string Column9 { get; set; }
        }

        // button shows stored assets to user
        private void ShowDatabase_Click(object sender, RoutedEventArgs e)
        {
            List<DataItem> data = new List<DataItem>();

           database d = new database();
                
                string query = "SELECT ID,Name,Model,Manufacture,Systeminfo,IpAddress,PDate,Department,Note FROM Assets";
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
                                Column3 = reader["Model"].ToString(),
                                Column4 = reader["Manufacture"].ToString(),
                                Column5 = reader["Systeminfo"].ToString(),
                                Column6 = reader["IpAddress"].ToString(),
                                Column7 = reader["PDate"].ToString(),
                                Column8 = reader["Department"].ToString(),
                                Column9 = reader["Note"].ToString()
                             };
                            data.Add(item);
                        }
                    }
                }
                dataListView.ItemsSource = data;
        }
        // Get system information
        private void GetInfo_Click(object sender, RoutedEventArgs e)
        {             
            string systemName = Environment.MachineName;
            string systemModel = "Windows\t " + Environment.OSVersion.Version.ToString();
            string systemManufacturer = $"{systemName}";
            string systemType = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
            string ipAddress = GetLocalIPAddress();

            // Fill the textboxes with the system information
            nameTextBox.Text = systemName;
            ModelTextBox.Text = systemModel;
            ManTextBox.Text = systemManufacturer;
            SysTextBox.Text = systemType;
            IpTextBox.Text = ipAddress;
        }
        
        // get loclal device Ip address
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
                
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            // Check if an item is selected
            if (dataListView.SelectedItem != null)
            {
                // Populate text boxes and combo box with the properties of the selected DataItem
                DataItem selectedData = (DataItem)dataListView.SelectedItem;
                IDTextBox.Text = selectedData.Column1;
                nameTextBox.Text = selectedData.Column2;
                ModelTextBox.Text = selectedData.Column3;
                ManTextBox.Text = selectedData.Column4;
                SysTextBox.Text = selectedData.Column5;
                IpTextBox.Text = selectedData.Column6;
                DateTextBox.Text = selectedData.Column7;
                departmentComboBox.Text = selectedData.Column8;
                descriptionTextBox.Text = selectedData.Column9;

            }
        }
        //update in database
        private void UpdateDatabase_Click(object sender, RoutedEventArgs e)
        {            
            try
            {                               
                Update db = new Update();
                Asset au = new Asset(IDTextBox.Text,
            nameTextBox.Text,
            ModelTextBox.Text,
            ManTextBox.Text,
            SysTextBox.Text,
            IpTextBox.Text,
            DateTextBox.Text,
            (departmentComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
            descriptionTextBox.Text
            );
                db.UpdateAsset(au);
            
             
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
     
        //delete from database
        private void DeleteDatabase_Click(object sender, RoutedEventArgs e)
        {
            database d = new database();
           
                try
                {
                    
                    string ID = IDTextBox.Text;
                                       
                    string insertQuery = "DELETE FROM Assets  WHERE ID = @ID";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, d.GetConnection()))
                    {

                        //
                        cmd.Parameters.AddWithValue("@ID", ID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data deleted to the database.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
             
            
        }
        //clear textboxes 
        private void GetClear_Click(object sender, RoutedEventArgs e)
        {
            IDTextBox.Text = string.Empty;
            nameTextBox.Text = string.Empty;
            ModelTextBox.Text = string.Empty;
            ManTextBox.Text = string.Empty;
            SysTextBox.Text = string.Empty;
            IpTextBox.Text = string.Empty;
            DateTextBox.Text = string.Empty;
            descriptionTextBox.Text = string.Empty; 
        }

        //will take back to home page
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
    }
}
