using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;


namespace ScottishGeln
{
    class updateEmp
    {
        public void updateEmployee(Staff staff)
        {



            database d = new database();

            try
            {
                string updateQuery = "UPDATE staff SET id = @Id, firstname = @Firstname, lastname = @Lastname, email = @Email, department = @Department WHERE id = @ID";

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, d.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Id", staff.Id);
                    cmd.Parameters.AddWithValue("@Firstname", staff.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", staff.Lastname);
                    cmd.Parameters.AddWithValue("@Email", staff.Email);
                    cmd.Parameters.AddWithValue("@Department", staff.Department);


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

        }

    }


        public class Staff
        {

            public string Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }
            public string Department { get; set; }


            public Staff(string id, string fname, string lname, string email, string department)
            {


                Id = id;
                Firstname = fname;
                Lastname = lname;
                Email = email;
                Department = department;
            }
        }
    }

