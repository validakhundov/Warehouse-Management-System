using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
namespace Warehouse_Management_System
{
    public partial class Form3 : Form
    {
        string connstring = String.Format("Server={0};Port={1};" +
                  "User Id={2};Password={3};Database={4};",
                  "35.222.239.160", "5432", "wms_client",
                 "C#User13", "wms");
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string currentPass = null;
            if (!textBox1.Text.Trim().Equals(""))
            {
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "SELECT password FROM auth limit 1;";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, conn))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        currentPass = reader[0].ToString();

                    }
                    conn.Close();
                }
            }


            if (currentPass != null && textBox1.Text.Equals(currentPass))
            {
                string newPass=textBox2.Text.Trim();
                if (newPass.Equals(textBox3.Text)&&!newPass.Equals(""))
                {

           try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection())
                    {

                        NpgsqlConnection conn = new NpgsqlConnection(connstring);
                        conn.Open();
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "update auth set password=:newPass where password=:oldPass";
                        cmd.CommandType = CommandType.Text;
                         cmd.Parameters.Add(new NpgsqlParameter("oldPass",currentPass));
                         cmd.Parameters.Add(new NpgsqlParameter("newPass", newPass));
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        conn.Close();
                    }
                }
            catch (Exception ex) { }
                    this.Hide();
                    MessageBox.Show("Password updated successfully!");
                }
                else { 
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                MessageBox.Show("Passwords are not match!");
                }
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                MessageBox.Show("Currect password is incorrect!");
            }

        }
    }
}
