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
  
    public partial class Form2 : Form
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        string selectedId;
        string connstring = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    "35.222.239.160", "5432", "wms_client",
                   "C#User13", "wms");
        public Form2()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 1;
            show();
        }

        private void show() {
            try
            {
                
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "SELECT * FROM product WHERE category";
                if (radioButton1.Checked)
                {
                    sql += " is not null";
                }
                else if (radioButton2.Checked)
                {
                    sql += "='TOOL'";
                }
                else 
                {
                    sql += "='MATERIAL'";
                }
                
                sql += " ORDER BY ";

                if (radioButton5.Checked)
                {
                    sql += " name";
                }
                else if (radioButton4.Checked)
                {
                    sql += " price";
                }
                else
                {
                    sql += " id";
                }

                if (radioButton9.Checked)
                {
                    sql += " asc";
                }
                else 
                {
                    sql += " desc";
                }

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
            
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            show();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;

            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            comboBox1.Enabled = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = 1;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;

            string category = comboBox1.SelectedIndex == 0 ? "TOOL" : "MATERIAL";
            if (selectedId == null)
            {
                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection())
                    {

                        NpgsqlConnection conn = new NpgsqlConnection(connstring);
                        conn.Open();
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "insert into product (name,category,count,price,comment) values(@name,@category,@count,@price,@comment)";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new NpgsqlParameter("@name", textBox1.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@category", category));
                        cmd.Parameters.Add(new NpgsqlParameter("@count", textBox2.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@price", textBox3.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("@comment", textBox4.Text));
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        conn.Close();
                    }
                    show();
                    MessageBox.Show("New product added successfully!");
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.ToString());
               
           
                }
            }
            else { 
            try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection())
                    {

                        NpgsqlConnection conn = new NpgsqlConnection(connstring);
                        conn.Open();
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "update product set name=:name,category=:category,count=:count,price=:price,comment=:comment where id=:id";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new NpgsqlParameter("id", selectedId));
                        cmd.Parameters.Add(new NpgsqlParameter("name", textBox1.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("category", category));
                        cmd.Parameters.Add(new NpgsqlParameter("count", textBox2.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("price", textBox3.Text));
                        cmd.Parameters.Add(new NpgsqlParameter("comment", textBox4.Text));
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        conn.Close();
                        
                    }
                    show();
                    MessageBox.Show("Product updated successfully!");
                    selectedId = null; 
                }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 4)
            {
                selectedId = dataGridView1.SelectedCells[0].Value.ToString();
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;


                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                comboBox1.Enabled = true;

                string category = dataGridView1.SelectedCells[2].Value.ToString();
                if (category.Equals("TOOL"))
                {
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    comboBox1.SelectedIndex = 0;
                }
                textBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedCells[3].Value.ToString();
                textBox3.Text = dataGridView1.SelectedCells[4].Value.ToString();
                textBox4.Text = dataGridView1.SelectedCells[5].Value.ToString();
            }
            else {
                selectedId = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(selectedId!=null){
                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection())
                    {
                        NpgsqlConnection conn = new NpgsqlConnection(connstring);
                        conn.Open();
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "delete from product where id="+selectedId;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        conn.Close();

                    }
                  
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }  

                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;

                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                comboBox1.Enabled = false;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                comboBox1.SelectedIndex = 1;

                show();
                MessageBox.Show("Product deleted successfully!");
            }
       
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            show();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            show();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            show();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            show();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            show();
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            show();
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
