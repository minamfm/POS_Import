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
        List<Transaction> AllTransactions = new List<Transaction>();
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
            //conn = new SqlConnection(util.GetConnectionString());

            //string query = "Select * From Transactions";

            //try
            //{
            //    conn.Open();
            //    adap = new SqlDataAdapter(query, conn);
            //    DataTable dt = new DataTable();
            //    adap.Fill(dt);
            //    dataGridView1.DataSource = dt;

            //    conn.Close();

            //}
            //catch
            //{

            //}
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
            if (dataListView1.SelectedItem != null)
            {


                x1 = AllTransactions[dataListView1.SelectedIndex].code;
                x2 = AllTransactions[dataListView1.SelectedIndex].date;   // date
                x3 = AllTransactions[dataListView1.SelectedIndex].clientid.ToString();    // client id 
                x4 = AllTransactions[dataListView1.SelectedIndex].totalamount.ToString();    // total
                x5 = AllTransactions[dataListView1.SelectedIndex].items;   // items
                x6 = AllTransactions[dataListView1.SelectedIndex].sellreturn.ToString();    //sellbuy
                x7 = AllTransactions[dataListView1.SelectedIndex].clientname;    // client name

            }

            if (!string.IsNullOrEmpty(x1) && !string.IsNullOrEmpty(x2) && !string.IsNullOrEmpty(x3)
                && !string.IsNullOrEmpty(x4) && !string.IsNullOrEmpty(x5) && !string.IsNullOrEmpty(x6)
                && !string.IsNullOrEmpty(x7))
            {
                returntran rt = new returntran(x1, x2, x3, x4, x5, x6, x7);
                rt.Show();
            }
            else
            {
                MessageBox.Show("errrr");
            }

        }



        private void datasell(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            //{
            //    string value1 = row.Cells[1].Value.ToString();

            //   // MessageBox.Show(value1);
            //}
        }

        private void Transactions_Load(object sender, EventArgs e)
        {
            AllTransactions = util.GetAllTransactions();
            dataListView1.SetObjects(AllTransactions);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string x1, x2, x3, x4, x5, x6, x7;
            
            if (dataListView1.SelectedItem != null)
            {


                x1 = AllTransactions[dataListView1.SelectedIndex].code;
                x2 = AllTransactions[dataListView1.SelectedIndex].date;   // date
                x3 = AllTransactions[dataListView1.SelectedIndex].clientid.ToString();    // client id 
                x4 = AllTransactions[dataListView1.SelectedIndex].totalamount.ToString();    // total
                x5 = AllTransactions[dataListView1.SelectedIndex].items;   // items
                x6 = AllTransactions[dataListView1.SelectedIndex].sellreturn.ToString();    //sellbuy
                x7 = AllTransactions[dataListView1.SelectedIndex].clientname;    // client name
                if (x6 == "0")
                {
                    string stuff = x5;
                    string tempcde = null;
                    string tempqty = null;
                    while (stuff != "")
                    {


                        if (stuff.IndexOf("&&++") >= 0)
                        {

                            tempcde = stuff.Substring(0, stuff.IndexOf("&&++"));
                            stuff = stuff.Substring(stuff.IndexOf("&&++") + 4);

                            if (stuff.IndexOf("++&&") >= 0)
                            {
                                tempqty = stuff.Substring(0, stuff.IndexOf("++&&"));
                                stuff = stuff.Substring(stuff.IndexOf("++&&") + 4);

                            }
                            SqlConnection conn = new SqlConnection(util.GetConnectionString());
                            SqlCommand cmd = new SqlCommand("Update Item SET qty= @qty Where Code= @code", conn);
                            SqlCommand cmd2 = new SqlCommand("Select qty From Item Where Code = @code", conn);

                            int qtyy = 0;
                            try
                            {
                                cmd2.Parameters.AddWithValue("@code", tempcde);
                                conn.Open();
                                using (SqlDataReader rdr = cmd2.ExecuteReader())
                                {
                                    if (rdr.Read())
                                    {
                                         qtyy = rdr.GetInt32(0);
                                    }
                                    conn.Close();
                                    qtyy = qtyy + Convert.ToInt32(tempqty);
                                }

                            }
                            catch
                            {
                                MessageBox.Show("rdr failed");
                            }


                            try
                            {
                                conn.Open();
                                cmd.Parameters.AddWithValue("@code", tempcde);
                                cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(qtyy));
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                            catch
                            {
                                MessageBox.Show("Failed to Update");
                                return;
                            }

                        }

                    }
                    util.deltransaction(Convert.ToInt32(x1), x2);
                    this.Transactions_Load(sender, e);
                }

            }
            


        }
    }
}
