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
    public partial class buysitem : Form
    {
        Test1.Utilities util = new Utilities();
        SqlCommand cmd;
        SqlConnection conn;

        Buys buys;
        public buysitem(object sender)
        {
            buys = (Buys)sender;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text)
                    && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text))
            {
                conn = new SqlConnection(util.GetConnectionString());
                cmd = new SqlCommand("Insert into Item (Code,name,supplier,type,qty,qtyunit,price) VALUES (N'" + textBox1.Text + "' , N'" + textBox2.Text + "',N'" + textBox3.Text + "',N'" + textBox4.Text + "', 0, 0 ,0 )", conn);
                SqlDataReader rdr;
                try
                {
                    conn.Open();
                    rdr = cmd.ExecuteReader();
                    MessageBox.Show("Done");
                    buys.Show();
                    this.Close();

                }
                catch
                {
                    MessageBox.Show("Failed");
                }
            } else
            {
                MessageBox.Show("رجاء ادخال كل المساحات الخالية");
            }
        }
    }
}
