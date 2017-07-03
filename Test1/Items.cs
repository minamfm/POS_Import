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
    public bool check;

    public itemdb(string x1 , string x2, string x3, string x4, int x5, int x6, float x7 )
    {
        this.code = x1;
        this.name = x2;
        this.supplier = x3;
        this.type = x4;
        this.qty = x5;
        this.qtyunit = x6;
        this.price = x7;
        this.check = false;
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
        DataTable dt;
        //SqlCommandBuilder commandbuilder;
        string path;

        public Items()
        {
            InitializeComponent();


        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Are you sure want to update", "Check ? ", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

            
                    }
                    
                
                else
                {
                MessageBox.Show("Nothing done");
                
            }
        }

       

        private void Items_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(util.GetConnectionString());
            string query = "Select * from Item";

            SqlCommand cmdq = new SqlCommand(query, conn);

            SqlDataReader rdr;

            dataListView1.CheckBoxes = true;
            dataListView1.View = View.Details;
            dataListView1.GridLines = true;
            dataListView1.FullRowSelect = true;
            dataListView1.ShowGroups = false;
         

            try
            {
                conn.Open();
                rdr = cmdq.ExecuteReader();

                while (rdr.Read())
                {
                    itemdb itm = new itemdb(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2),rdr.GetString(3),(Int32)rdr.GetValue(4), (Int32) rdr.GetValue(5) , Convert.ToSingle(rdr.GetValue(6)));
                    its.Add(itm);
                }
                dataListView1.SetObjects(its);
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
                path = textBox1.Text + "\\";

            }
        }
    }





        //public void ExportEx(ListView gr, string xlname, string path)
        //{
        //    Microsoft.Office.Interop.Excel.Application obj = new Microsoft.Office.Interop.Excel.Application();
        //    obj.Application.Workbooks.Add(Type.Missing);
        //    obj.Columns.ColumnWidth = 25;
        //    for (int i = 1; i < gr.Columns.Count + 1; i++)
        //    {
        //        obj.Cells[1, i] = gr.Columns[i - 1].HeaderText;
        //    }
        //    for (int i = 0; i < gr.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < gr.Columns.Count; j++)
        //        {
        //            obj.Cells[i + 2, j + 1] = gr.Rows[i].Cells[j].Value.ToString();
        //        }
        //    }

        //    string xlxname = "\\" + xlname;
        //    obj.ActiveWorkbook.SaveCopyAs(path + xlxname + ".xlsx");
        //    MessageBox.Show("Done :) ! ");

        //}


    }
}
