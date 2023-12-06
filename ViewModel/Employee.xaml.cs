using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
            dataListView.SelectionChanged += ListView_SelectionChanged;
            LoadAssets();
            LoadSoft();
        }
        public class DataItem
        {
            public string Column1 { get; set; }
            public string Column2 { get; set; }
            public string Column3 { get; set; }
            public string Column4 { get; set; }
            public string Column5 { get; set; }

        }

        //shows all data from database
        private void Show_Click(object sender, RoutedEventArgs e)
        {
            List<DataItem> data = new List<DataItem>();
            database d = new database();
                              
                string query = "SELECT id,firstname,lastname,email,department FROM staff";
                using (MySqlCommand cmd = new MySqlCommand(query, d.GetConnection()))
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
                depCombox.Text = selectedData.Column5;

            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            database d = new database();

            try
            {                
                string id = idTextbox.Text;
                string fname = fnTextbox.Text;
                string lname = LnTextbox.Text;
                string email = emTextbox.Text;
                string dep = (depCombox.SelectedItem as ComboBoxItem)?.Content.ToString(); 

                string insertq = "INSERT INTO staff (id,firstname,lastname,email,department) values(FLOOR(10000 + RAND() * 90000),@fname,@lname,@email,@dep)";
                using (MySqlCommand cmd = new MySqlCommand(insertq, d.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@lname", lname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@dep", dep);

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


        //make changes to database
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                updateEmp emp = new updateEmp();
                Staff st = new Staff(
                 idTextbox.Text,
                 fnTextbox.Text,
                 LnTextbox.Text,
                 emTextbox.Text,
                 depCombox.Text
                 );
                emp.updateEmployee(st);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        //delete from database
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            database d = new database();

            try
            {

                string ID = idTextbox.Text;

                string insertQuery = "DELETE FROM staff  WHERE ID = @ID";
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
        //clear all boxes
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            fnTextbox.Text = string.Empty;
            LnTextbox.Text = string.Empty;
            emTextbox.Text = string.Empty;
            idTextbox.Text = string.Empty;
            depCombox.Text = string.Empty;

        }

        private void LoadAssets()
        {
            // Assume you have a method to retrieve assets from the database
            List<string> assets = GetAssetsFromDatabase();

            
            // Populate the ComboBox with assets
            assetComboBox.ItemsSource = assets;
        }
        // this is for assets
        private List<string> GetAssetsFromDatabase()
        {
            List<string> assets = new List<string>();

            database d = new database();
            try
            {           
                    string query = "SELECT Name FROM Assets"; // Adjust the table and column names accordingly
                    using (MySqlCommand cmd = new MySqlCommand(query, d.GetConnection()))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string assetName = reader["Name"].ToString(); // Adjust the column name accordingly
                                assets.Add(assetName);
                            }
                        }
                    }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return assets;
        }

        //this is for softwere
        private List<string> GetAssetsFromDatabaseSoft()
        {
            List<string> soft = new List<string>();

            database d = new database();
            try
            {
                string query = "SELECT Name FROM SoftwareAssets"; // Adjust the table and column names accordingly
                using (MySqlCommand cmd = new MySqlCommand(query, d.GetConnection()))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string assetName = reader["Name"].ToString(); // Adjust the column name accordingly
                            soft.Add(assetName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return soft;
        }
        private void LoadSoft()
        {
            List <string> soft = GetAssetsFromDatabaseSoft();
            SoftComboBox.ItemsSource = soft;
        }

        private void AssignAsset_Click(object sender, RoutedEventArgs e)
        {
            
                string selectedAsset = assetComboBox.SelectedItem as string;
                string selectedSoft= SoftComboBox.SelectedItem as string;
                string selectedEmployee = $"{fnTextbox.Text} {LnTextbox.Text}";
                string department = depCombox.Text;

            
            if (selectedAsset != null && selectedSoft != null && selectedEmployee != null && department != null)
            {
                // Call a method to perform the assignment and update the database
                AssignAssetToEmployee(selectedAsset, selectedSoft, selectedEmployee, department);

                // Show a message or perform any other actions if needed
                MessageBox.Show($"Asset '{selectedAsset}' and Software '{selectedSoft}' assigned to employee '{selectedEmployee}' in department '{department}'");
            }
            else
            {
                string errorMessage = "Please correct the following issues:\n";

                if (selectedAsset == null)
                {
                    errorMessage += "- Select an asset\n";
                }

                if (selectedSoft == null)
                {
                    errorMessage += "- Select software\n";
                }

                if (selectedEmployee == null)
                {
                    errorMessage += "- Enter employee information\n";
                }

                if (department == null)
                {
                    errorMessage += "- Select a department\n";
                }

                MessageBox.Show(errorMessage);
              //  MessageBox.Show("Please select an asset, ensure employee information is complete, and select a department.");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();

        }


        private void AssignAssetToEmployee(string assetName,string softName, string employeeName, string department)
        {

            database d = new database();
            try
            {
                string insertQuery = "INSERT INTO Assaigne_asset (id, Asset_name, Softwere_name, Employees_name,department ) VALUES (FLOOR(10000 + RAND() * 90000),@AssetName, @SoftName, @EmployeeName, @department)";
                using (MySqlCommand cmd = new MySqlCommand(insertQuery, d.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@AssetName", assetName);
                    cmd.Parameters.AddWithValue("@SoftName", softName);
                    cmd.Parameters.AddWithValue("@EmployeeName", employeeName);
                    cmd.Parameters.AddWithValue("@department", department);



                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Asset assigned successfully
                    }
                    else
                    {
                        MessageBox.Show("Failed to assign the asset. Please check your input.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


    }
}

