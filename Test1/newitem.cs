using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    public partial class newitem : Form
    {
        Test1.Utilities util = new Test1.Utilities();
        public newitem()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           SqlConnection conn = new SqlConnection(util.GetConnectionString());
            List<long> item_codes = new List<long>();
            string query = "Select * From Item";
            SqlCommand cmdq = new SqlCommand(query, conn);
            SqlDataReader rdr;
            try {
                conn.Open();
                rdr = cmdq.ExecuteReader();
                while (rdr.Read())
                {
                    item_codes.Add(rdr.GetInt32(0));
                }
                rdr.Close();

                if (item_codes.Contains(Convert.ToInt64(textBox1.Text)))
                {
                    MessageBox.Show("item metkarara");
                    return;
                }
            
            }
            catch {
                MessageBox.Show("adding!");
                conn.Close();
            }


            try
            {
                
                //TODO: Sanity checks on these things
                MessageBox.Show("still Adding");
                int cde = Int32.Parse(textBox1.Text);
                int qty = Int32.Parse(textBox3.Text);
                int qtyunit = Int32.Parse(textBox4.Text);
                float price = float.Parse(textBox6.Text);

                SqlCommand objcmd = new SqlCommand("Insert into Item (Code,name,supplier,type,qty,qtyunit,price) Values('" + cde + "','" + textBox2.Text + "','" + textBox5.Text + "','" + textBox7.Text + "','" + qty + "','" + qtyunit + "','" + price + "')", conn);
                MessageBox.Show("Query insert tamam");
              
              //  conn.Open();
                objcmd.ExecuteNonQuery();


                MessageBox.Show("ADDED");



            }

            catch
            {
                MessageBox.Show("fail");
                //using (SqlCommand cmd =
                //new SqlCommand("INSERT INTO Item VALUES(" + "@Code,@name , @supplier, @type,@qty,@qtyunit , @price)", conn))
                //{

                //    cmd.Parameters.AddWithValue("@Code", cde);
                //    cmd.Parameters.AddWithValue("@name", textBox2.Text);
                //    cmd.Parameters.AddWithValue("@supplier", textBox5.Text);
                //    cmd.Parameters.AddWithValue("@type", textBox7.Text);
                //    cmd.Parameters.AddWithValue("@qty", qty);
                //    cmd.Parameters.AddWithValue("@qtyunit", qtyunit);
                //    cmd.Parameters.AddWithValue("@price", price);

                //}
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
