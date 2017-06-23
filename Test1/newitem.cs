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
        public newitem()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection conn = new SqlConnection(@" Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Ramez\Documents\Visual Studio 2015\Projects\Test1\Test1\DB1TEST.mdf ; Integrated Security = True; Connect Timeout = 30 ");

            string query = "Select * From Item WHERE Code = '" + textBox1.Text + "'and name='" + textBox2.Text + " ' and supllier='" + textBox5.Text + "'";
            SqlCommand cmdq = new SqlCommand(query, conn);
            SqlDataReader rdr;

            try
            {
                conn.Open();
             
                rdr = cmdq.ExecuteReader();

                Console.WriteLine("tamam");
                while (rdr.Read())
                {
                    MessageBox.Show("already in ");
                }

                string query2 = "Insert into Item"

                // Call Close when done reading.
                rdr.Close();
            }

            catch
            {
                MessageBox.Show("no");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
