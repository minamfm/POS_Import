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
    public partial class Transactions : Form
    {
        Test1.Utilities util = new Test1.Utilities();

        SqlConnection conn;
        DataTable dt = new DataTable();
        SqlDataAdapter adap;

        public Transactions()
        {
            InitializeComponent();
            Getdatagrid();
          
        }

    


        
        private void Getdatagrid()
        {
            conn = new SqlConnection(util.GetConnectionString());

            string query = "Select * From Transactions";

            try
            {
                conn.Open();
                adap = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                dataGridView1.DataSource = dt;
                
                conn.Close();

            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            New_Transaction nt = new New_Transaction();
            nt.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //conn = new SqlConnection(util.GetConnectionString());

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string value1 = row.Cells[0].Value.ToString();
                string value2 = row.Cells[1].Value.ToString();
                string value3 = row.Cells[2].Value.ToString();
                string value4 = row.Cells[3].Value.ToString();
                string value5 = row.Cells[4].Value.ToString();
                string value6 = row.Cells[5].Value.ToString();
                string value7 = row.Cells[6].Value.ToString();
                string value8 = row.Cells[7].Value.ToString();

                MessageBox.Show(value1+"    "+ value2 + "    " + value3 + "    " + value4 + "    " + value5 + "    " + value6 + "    " + value7 + "    " + value8 + "    "  );
            }

        }

      

        private void datasell(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string value1 = row.Cells[1].Value.ToString();
              
               // MessageBox.Show(value1);
            }
        }
    }
}
