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
        DataSet ds;
        public Items()
        {
            InitializeComponent();


        }

        
        private void Fill_Grid()
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

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(util.GetConnectionString());
            DialogResult dialogResult = MessageBox.Show("Are you sure want to update", "Check ? ", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                    try
                    {
                    conn.Open();
                    MessageBox.Show("TAMAM1");

                        commandbuilder = new SqlCommandBuilder(adap);
                    MessageBox.Show("TAMAM2");

                    ds = new DataSet();
                    ds.Tables.Add(dt);

                    adap.Update(ds, "Item");

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
