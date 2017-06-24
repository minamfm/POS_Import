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
            AutoCompleteText();

        }
void AutoCompleteText()
        {
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
         
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

            SqlConnection conn = new SqlConnection(util.GetConnectionString());
            List<long> item_codes = new List<long>();
            List<String> item_name = new List<String>();
            List<String> item_suplier = new List<String>();
            List<String> item_type = new List<string>();
            List<long> item_qty = new List<long>();
            List<long> item_qtyunit = new List<long>();
            List<long> item_price = new List<long>();

            string query = "Select * From Item";
            SqlCommand cmdq = new SqlCommand(query, conn);
            SqlDataReader rdr;
            try
            {
                conn.Open();
                rdr = cmdq.ExecuteReader();
                while (rdr.Read())
                {
                    //item_codes.Add(rdr.GetInt32(0));
                    //item_name.Add(rdr.GetString(1));
                    //item_suplier.Add(rdr.GetString(2));
                    //item_type.Add(rdr.GetString(3));
                    //item_qty.Add(rdr.GetInt32(4));
                    //item_qtyunit.Add(rdr.GetInt32(5));
                    //item_price.Add(rdr.GetInt32(6));

                    string n = Convert.ToString(rdr.GetInt32(0));
                    collection.Add(n);  

                }
              

                rdr.Close();

            }
            catch
            {
                conn.Close();
            }

            textBox1.AutoCompleteCustomSource = collection;
        }

        // Auto complete Textbox suggest added     
        // TODO Autofill all blanks


        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(util.GetConnectionString());
            List<long> item_codes = new List<long>();
            List<String> item_name = new List<string>();
            string query = "Select * From Item";
            SqlCommand cmdq = new SqlCommand(query, conn);
            SqlDataReader rdr;
            try
            {
                conn.Open();
                rdr = cmdq.ExecuteReader();
                while (rdr.Read())
                {
                    item_codes.Add(rdr.GetInt32(0));
                    item_name.Add(rdr.GetString(1));

                }
                rdr.Close();

                if (item_codes.Contains(Convert.ToInt64(textBox1.Text)) ||
                    item_name.Contains(textBox2.Text))
                {
                    MessageBox.Show("item metkarara");
                    return;
                }

            }
            catch
            {
                conn.Close();
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text) &&
                   string.IsNullOrWhiteSpace(textBox2.Text) &&
                  string.IsNullOrWhiteSpace(textBox3.Text) &&
                   string.IsNullOrWhiteSpace(textBox4.Text) &&
                  string.IsNullOrWhiteSpace(textBox5.Text) &&
                      string.IsNullOrWhiteSpace(textBox6.Text) &&
                       string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("املا الفراغ يسطا! :]");

            }

            else
            {

                try
                {



                    //TODO: Sanity checks on these things  
                    // SOLVED BITCH :P
                    MessageBox.Show("still Adding");
                    int cde = Int32.Parse(textBox1.Text);
                    int qty = Int32.Parse(textBox3.Text);
                    int qtyunit = Int32.Parse(textBox4.Text);
                    float price = float.Parse(textBox6.Text);
                    //arabic solved :D !!
                    SqlCommand objcmd = new SqlCommand("Insert into Item (Code,name,supplier,type,qty,qtyunit,price) Values( '" +cde +" ',N'" + textBox2.Text + "',N'" + textBox5.Text + "',N'" + textBox7.Text + "','" + qty + "','" + qtyunit + "','" + price + "')", conn);
                    MessageBox.Show("Query insert tamam");

                    // conn.Open();
                    objcmd.ExecuteNonQuery();


                    MessageBox.Show("ADDED");



                }

                catch
                {
                    MessageBox.Show("fail");
                }

                finally
                {
                    conn.Close();
                }
            }
        }
    }
}