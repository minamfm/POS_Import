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
struct itemdb
{
    public string code;
    public string name;
    public string supplier;
    public string type;
    public int qty;
    public int qtyunit;
    public float price;

    public itemdb(string x1, string x2, string x3, string x4, int x5, int x6, float x7)
    {
        this.code = x1;
        this.name = x2;
        this.supplier = x3;
        this.type = x4;
        this.qty = x5;
        this.qtyunit = x6;
        this.price = x7;
    }


}

namespace Test1
{
    public partial class Items : Form
    {
        Utilities util = new Test1.Utilities();

        List<itemdb> its = new List<itemdb>();

        SqlConnection conn;

        SqlDataAdapter adap;
        // DataSet ds = new DataSet();
        //   SqlCommandBuilder cmdb;


        DataTable dt = new DataTable();
        SqlCommand cmdq;
        string path;
        string path2;

        public Items()
        {
            InitializeComponent();


        }






        private void Items_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            try
            {
                SqlConnection conn = new SqlConnection(util.GetConnectionString());
                string query = "Select * from Item";

                adap = new SqlDataAdapter(query, conn);
                conn.Open();
                dt = new DataTable();
                adap.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();

                cmdq = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdq.ExecuteReader();

                while (rdr.Read())
                {
                    its.Add(new itemdb(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), Convert.ToInt32(rdr.GetValue(4)), Convert.ToInt32(rdr.GetValue(5)), Convert.ToSingle(rdr.GetValue(6))));
                }

                rdr.Close();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Database access error");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            {
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {

                    textBox2.Text = folderBrowserDialog1.SelectedPath;
                    path = textBox2.Text + "\\";

                }
            }
        }




        DataTable ConvertListToDataTable(List<itemdb> list)
        {
            DataTable table = new DataTable();


            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("supplier", typeof(string));
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("qty", typeof(int));
            table.Columns.Add("qtyunit", typeof(int));
            table.Columns.Add("price", typeof(float));

            //table.Columns.Add();
            //table.Columns.Add();
            //table.Columns.Add();
            //table.Columns.Add();
            //table.Columns.Add();
            //table.Columns.Add();
            //table.Columns.Add();

            foreach (itemdb it in list)
            {
                table.Rows.Add(it.code, it.name, it.supplier, it.type, it.qty, it.qtyunit, it.price);
            }

            return table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to update", "Check ? ", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    //DataTable table = ConvertListToDataTable(its);
                    // SqlDataAdapter adap = new SqlDataAdapter("Update Item Set Code=N'" +  it.code + "', name =N'" + it.name + "', supplier=N'" + it.supplier + "', type=N'" + it.type + "', qty='" + it.qty + "', qtyunit='" + it.qtyunit + "',price='" + it.price + "' WHERE Code ='" + it.code + "'", conn);
                    //adap.SelectCommand.ExecuteNonQuery();

                    conn = new SqlConnection(util.GetConnectionString());
                    conn.Open();

                    string query = "Select * from Item";

                    adap = new SqlDataAdapter(query, conn);
                    SqlCommandBuilder cmdb = new SqlCommandBuilder(adap);
                    adap.Update(dt);

                    conn.Close();
                    foreach (itemdb it in its)
                    {

                        MessageBox.Show(it.code);
                    }


                    this.InitializeComponent();
                    this.Refresh();

                }
                catch
                {
                    MessageBox.Show("Error!");
                }

            }
            else
            {
                MessageBox.Show("Nothing Done");
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please Enter a value");

            }
            else
            {
                DataView dv = new DataView(dt);
                if(listBox1.Text == "Code")
                {
                  dv.RowFilter = "Code = '"+textBox1.Text+"'";
                    //dv.RowStateFilter = DataViewRowState.ModifiedCurrent;
                    dataGridView2.DataSource = dv;

                }
                if (listBox1.Text == "Name")
                {
                    dv.RowFilter = "name = '" + textBox1.Text + "'";
                    //dv.RowStateFilter = DataViewRowState.ModifiedCurrent;
                    dataGridView2.DataSource = dv;

                }
                if (listBox1.Text == "Supplier")
                {
                    dv.RowFilter = "supplier = '" + textBox1.Text + "'";
                    //dv.RowStateFilter = DataViewRowState.ModifiedCurrent;
                    dataGridView2.DataSource = dv;

                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text)&& !string.IsNullOrEmpty(textBox2.Text))
            {
                ExportEx(dataGridView1, textBox3.Text, path);
            } 
            else
            {
                MessageBox.Show("Make sure you filled all requirements");
            }
        }







        public void ExportEx(DataGridView gr, string xlname, string path)
        {

                Microsoft.Office.Interop.Excel.Application obj = new Microsoft.Office.Interop.Excel.Application();
                obj.Application.Workbooks.Add(Type.Missing);
                obj.Columns.ColumnWidth = 25;
                for (int i = 1; i < gr.Columns.Count + 1; i++)
                {
                    obj.Cells[1, i] = gr.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < gr.Rows.Count-1; i++)
                {
                    for (int j = 0; j < gr.Columns.Count; j++)
                    {
                        obj.Cells[i + 2, j + 1] = gr.Rows[i].Cells[j].Value.ToString();
                    }
                }

                string xlxname = xlname;
                obj.ActiveWorkbook.SaveCopyAs(path + xlxname + ".xlsx");
                MessageBox.Show("Done :) ! ");
            

        }

        public void ExportEx2(DataGridView gr, string xlname, string path)
        {

            Microsoft.Office.Interop.Excel.Application obj = new Microsoft.Office.Interop.Excel.Application();
            obj.Application.Workbooks.Add(Type.Missing);
            obj.Columns.ColumnWidth = 25;
            for (int i = 1; i < gr.Columns.Count + 1; i++)
            {
                obj.Cells[1, i] = gr.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < gr.Rows.Count ; i++)
            {
                for (int j = 0; j < gr.Columns.Count; j++)
                {
                    obj.Cells[i + 2, j + 1] = gr.Rows[i].Cells[j].Value.ToString();
                }
            }

            string xlxname = xlname;
            obj.ActiveWorkbook.SaveCopyAs(path + xlxname + ".xlsx");
            MessageBox.Show("Done :) ! ");


        }

        private void button6_Click(object sender, EventArgs e)
        {   if(dataGridView2.RowCount != 0) {
                if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text))
                {
                    ExportEx2(dataGridView2, textBox5.Text, path2);
                }
                else
                {
                    MessageBox.Show("Make sure you filled all requirements");
                }
            }
            else
            {
                MessageBox.Show("No data filtered!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            {
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {

                    textBox4.Text = folderBrowserDialog1.SelectedPath;
                    path2 = textBox4.Text + "\\";

                }
            }
        }
    }


}
