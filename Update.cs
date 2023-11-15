using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;


namespace ScottishGeln
{
   class Update
    {
        //private string connectionString;

        //public  Update()
        //{
        //    database db = new  database();
        //    // connectionString = "server=lochnagar.abertay.ac.uk;username=sql2100258;password=reduces dump risk baths;database=sql2100258;";
            
        //}

        public void UpdateAsset(Asset asset)
        {
            database db = new database();
            //using (MySqlConnection connection = new MySqlConnection(connectionString))
            //{
            try
                {
                  //  connection.Open();

                    string updateQuery = "UPDATE Assets SET Name = @Name, Model = @Model, Manufacture = @Manufacture, Systeminfo = @Systeminfo, IpAddress = @IpAddress, PDate = @PDate, Department = @Department, Note = @Note WHERE ID = @ID";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, db.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@Name", asset.Name);
                        cmd.Parameters.AddWithValue("@Model", asset.Model);
                        cmd.Parameters.AddWithValue("@Manufacture", asset.Manufacture);
                        cmd.Parameters.AddWithValue("@Systeminfo", asset.SystemInfo);
                        cmd.Parameters.AddWithValue("@IpAddress", asset.IpAddress);
                        cmd.Parameters.AddWithValue("@PDate", asset.PDate);
                        cmd.Parameters.AddWithValue("@Department", asset.Department);
                        cmd.Parameters.AddWithValue("@Note", asset.Note);
                        cmd.Parameters.AddWithValue("@ID", asset.ID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data updated in the database.");
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
               // connection.Close();
            }
        }
    }

    public class Asset
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Manufacture { get; set; }
        public string SystemInfo { get; set; }
        public string IpAddress { get; set; }
        public string PDate { get; set; }
        public string Department { get; set; }
        public string Note { get; set; }

        public Asset(string id, string name, string model, string manufacture, string systemInfo, string ipAddress, string pDate, string department, string note)
        {
            ID = id;
            Name = name;
            Model = model;
            Manufacture = manufacture;
            SystemInfo = systemInfo;
            IpAddress = ipAddress;
            PDate = pDate;
            Department = department;
            Note = note;
        }
    }
    

