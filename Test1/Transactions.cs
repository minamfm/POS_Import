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
            string x1 = "";
            string x2 = "";
            string x3 = "";
            string x4 = "";
            string x5 = "";
            string x6 = "";
            string x7 = "";

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string value1 = row.Cells[0].Value.ToString();
                string value2 = row.Cells[1].Value.ToString();
                string value3 = row.Cells[2].Value.ToString();
                string value4 = row.Cells[3].Value.ToString();
                string value5 = row.Cells[4].Value.ToString();
                string value6 = row.Cells[5].Value.ToString();
                string value7 = row.Cells[6].Value.ToString();
                x1 = value1;   // x
                x2 = value2;   // date
                x3 = value3;    // client id 
                x4 = value4;    // total
                x5 = value5;   // items
                x6 = value6;    //sellbuy
                x7 = value7;    // client name
                MessageBox.Show(value1+"    "+ value2 + "    " + value3 + "    " + value4 + "    " + value5 + "    " + value6 + "    " + value7 + "    "  );
            }

            if (!string.IsNullOrEmpty(x1) && !string.IsNullOrEmpty(x2) && !string.IsNullOrEmpty(x3)
                && !string.IsNullOrEmpty(x4)&&!string.IsNullOrEmpty(x5) &&!string.IsNullOrEmpty(x6)
                && !string.IsNullOrEmpty(x7))
            {
                returntran rt = new returntran(x1,x2,x3,x4,x5,x6,x7);
                rt.Show();
            }
            else
            {
                MessageBox.Show("errrr");
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
