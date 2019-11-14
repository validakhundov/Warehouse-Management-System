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
    public partial class Form1 : Form
    {
        string connstring = String.Format("Server={0};Port={1};" +
            "User Id={2};Password={3};Database={4};",
            "35.222.239.160", "5432", "wms_client",
           "C#User13", "wms");
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string currentPass=null;
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
                    currentPass=reader[0].ToString();

                }
                conn.Close();
            }
           }


            if (currentPass!=null&&textBox1.Text.Equals(currentPass))
            {
                Form2 f2 = new Form2();
                f2.Show();
                this.Hide();
            }
            else {
                textBox1.Text = "";
                MessageBox.Show("Incorrect password!");
            }

          
        }
    }
}
