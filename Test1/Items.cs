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

    public itemdb(string x1 , string x2, string x3, string x4, int x5, int x6, float x7 )
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

        DataTable dt;
        //SqlCommandBuilder commandbuilder;
        string path;

        public Items()
        {
            InitializeComponent();


        }


        

       

        private void Items_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(util.GetConnectionString());
            string query = "Select * from Item";

            SqlCommand cmdq = new SqlCommand(query, conn);

            SqlDataReader rdr;

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<itemdb> itemfilter = new List<itemdb>();
            itemfilter = its;

            itemfilter = itemfilter.FindAll(r => r.code.Contains(textBox1.Text));
            dataListView1.SetObjects(itemfilter);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            List<itemdb> itemfilter = new List<itemdb>();
            itemfilter = its;
            itemfilter = itemfilter.FindAll(r => r.name.Contains(textBox4.Text));
            dataListView1.SetObjects(itemfilter);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            List<itemdb> itemfilter = new List<itemdb>();
            itemfilter = its;
            itemfilter = itemfilter.FindAll(r => r.supplier.Contains(textBox5.Text));
            dataListView1.SetObjects(itemfilter);
        }

        private void dataListView1_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            try
            {
                List<itemdb> itemf = new List<itemdb>();
                itemf = its;

                itemdb tempitem = itemf[dataListView1.SelectedIndex];

                if (e.Column.Text == "Code")
                {
                    string test = Convert.ToString(e.NewValue);
                    if (!string.IsNullOrEmpty(test))
                    {
                        tempitem.code = test;
                        itemf[dataListView1.SelectedIndex] = tempitem;
                        int index = its.FindIndex(r => r.code == tempitem.code);
                        its[index] = tempitem;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a value");
                    }
                }

                else if (e.Column.Text == "Item Name")
                {
                    string test = Convert.ToString(e.NewValue);
                    if (!string.IsNullOrEmpty(test))
                    {
                        tempitem.name = test;
                        itemf[dataListView1.SelectedIndex] = tempitem;
                        int index = its.FindIndex(r => r.name == tempitem.name);
                        its[index] = tempitem;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a value");
                    }
                }
                else if (e.Column.Text == "Supplier")
                {
                    string test = Convert.ToString(e.NewValue);
                    if (!string.IsNullOrEmpty(test))
                    {
                        tempitem.supplier = test;
                        itemf[dataListView1.SelectedIndex] = tempitem;
                        int index = its.FindIndex(r => r.supplier == tempitem.supplier);
                        its[index] = tempitem;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a value");
                    }
                } 
                else if (e.Column.Text == "type")
                {
                    string test = Convert.ToString(e.NewValue);
                    if (!string.IsNullOrEmpty(test))
                    {
                        tempitem.type = test;
                        itemf[dataListView1.SelectedIndex] = tempitem;
                        int index = its.FindIndex(r => r.type == tempitem.type);
                        its[index] = tempitem;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a value");
                    }
                }
                else if (e.Column.Text == "Quantity")
                {
                    string test = Convert.ToString(e.NewValue);
                    if (!string.IsNullOrEmpty(test))
                    {
                        try {
                            tempitem.qty = Convert.ToInt32(test);
                        } catch
                        {
                            MessageBox.Show("Please insert a good value");
                            return;
                        } 
                        itemf[dataListView1.SelectedIndex] = tempitem;
                        int index = its.FindIndex(r => r.qty == tempitem.qty);
                        its[index] = tempitem;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a value");
                    }
                }
                else if (e.Column.Text == "Quantity per unit")
                {
                    string test = Convert.ToString(e.NewValue);
                    if (!string.IsNullOrEmpty(test))
                    {
                        try
                        {
                            tempitem.qtyunit = Convert.ToInt32(test);
                        }
                        catch
                        {
                            MessageBox.Show("Please insert a good value");
                            return;
                        }
                        itemf[dataListView1.SelectedIndex] = tempitem;
                        int index = its.FindIndex(r => r.qtyunit == tempitem.qtyunit);
                        its[index] = tempitem;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a value");
                    }
                }
                else if (e.Column.Text == "Price")
                {
                    string test = Convert.ToString(e.NewValue);
                    if (!string.IsNullOrEmpty(test))
                    {
                        try
                        {
                            tempitem.price = Convert.ToSingle(test);
                        }
                        catch
                        {
                            MessageBox.Show("Please insert a good value");
                            return;
                        }
                        itemf[dataListView1.SelectedIndex] = tempitem;
                        int index = its.FindIndex(r => r.price == tempitem.price);
                        its[index] = tempitem;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a value");
                    }
                }

                dataListView1.SetObjects(its);




            }
            catch
            {
                MessageBox.Show("Editing error");
            }
        }



        //DataTable ConvertListToDataTable(List<itemdb> list)
        //{
        //    DataTable table = new DataTable();


        //    table.Columns.Add("Code", typeof(string));
        //    table.Columns.Add("name", typeof(string));
        //    table.Columns.Add("supplier", typeof(string));
        //    table.Columns.Add("type", typeof(string));
        //    table.Columns.Add("qty", typeof(int));
        //    table.Columns.Add("qtyunit", typeof(int));
        //    table.Columns.Add("price", typeof(float));

        //    //table.Columns.Add();
        //    //table.Columns.Add();
        //    //table.Columns.Add();
        //    //table.Columns.Add();
        //    //table.Columns.Add();
        //    //table.Columns.Add();
        //    //table.Columns.Add();

        //    foreach (itemdb it in list)
        //    {
        //        table.Rows.Add(it.code, it.name, it.supplier, it.type, it.qty, it.qtyunit, it.price);
        //    }

        //    return table;
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to update", "Check ? ", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                try
                {
                   // DataTable table = ConvertListToDataTable(its);
                    conn = new SqlConnection(util.GetConnectionString());

                    foreach (itemdb it in its)
                    {
                        conn.Open();
                        SqlDataAdapter adap = new SqlDataAdapter("Update Item Set Code=N'" +  it.code + "', name =N'" + it.name + "', supplier=N'" + it.supplier + "', type=N'" + it.type + "', qty='" + it.qty + "', qtyunit='" + it.qtyunit + "',price='" + it.price + "' WHERE Code ='" + it.code + "'", conn);

                        adap.SelectCommand.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Update 1 ");
                    }
             



                }
                catch
                {
                    MessageBox.Show("Error!");
                }

            }else
            {
                MessageBox.Show("Nothing Done");
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
