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
    public partial class Items : Form
    {
        Utilities util = new Test1.Utilities();

        SqlConnection conn;

        SqlDataAdapter adap;
        DataTable dt;
        SqlCommandBuilder commandbuilder;

        public Items()
        {
            InitializeComponent();


        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Are you sure want to update", "Check ? ", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                    try
                    {
                    conn = new SqlConnection(util.GetConnectionString());
                    conn.Open();
                    MessageBox.Show("TAMAM1");
                    commandbuilder = new SqlCommandBuilder(adap);
                    MessageBox.Show("TAMAM2");
                    adap.Update(dt);
                    MessageBox.Show("Information updated");
                    conn.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Failed");
                    }
                }
                else
                {
                MessageBox.Show("Nothing done");
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(util.GetConnectionString());
                conn.Open();
                adap = new SqlDataAdapter("Select * from Item", conn);
                dt = new DataTable();
                adap.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();


            }
            catch
            {
                MessageBox.Show("Can't connect");
            }
        }
    }
}
