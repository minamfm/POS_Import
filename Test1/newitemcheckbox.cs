using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


public struct item
{
    public string code;
    public string name;
    public string supplier;
    public string type;
    public string qty;
    public string qtyunit;
    public string price;
    public string editable_price;
    public bool item_checked;
    public string Code 
        {
            get { return code; }
             set { }
        }
   public item (string cd, string nm, string supp, string type, string qty, string qtyunit, string price, string editable_prc)
    {
        this.code = cd;
        this.name = nm;
        this.supplier = supp;
        this.type = type;
        this.qty = qty;
        this.qtyunit = qtyunit;
        this.price = price;
        this.editable_price = editable_prc;
        this.item_checked = false;
    }
}
namespace Test1
{
    public partial class newitemcheckbox : Form
    {
        Utilities util = new Utilities();
        List<item> items = new List<item>();
        List<item> items_filter = new List<item>();
        object senderform;
        public newitemcheckbox(object sender_form)
        {
            senderform = sender_form;
            InitializeComponent();
        }

        private void newitemcheckbox_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(util.GetConnectionString());
            string query = "Select * from Item";

            SqlCommand cmdq = new SqlCommand(query, conn);

            SqlDataReader rdr;
            listView1.CheckBoxes = true;
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.AlwaysGroupBySortOrder = System.Windows.Forms.SortOrder.None;

            try
            {
                conn.Open();
                rdr = cmdq.ExecuteReader();

                while (rdr.Read())
                {
                    item itm = new item(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), Convert.ToString(0), Convert.ToString((int)rdr.GetValue(5)), Convert.ToString((float)(double)rdr.GetValue(6)), Convert.ToString((float)(double)rdr.GetValue(6)));
                    items.Add(itm);
                }
                items_filter = items;
                listView1.SetObjects(items_filter);
                rdr.Close();
                conn.Close();

            }
            catch
            {
                MessageBox.Show("Database access error");
            }
        }
        private static ListViewItem GenerateItem(item it)
        {
            string[] arr = { it.code, it.name, it.supplier, it.type, Convert.ToString(it.qty), Convert.ToString(it.qtyunit), Convert.ToString(it.price), Convert.ToString(it.editable_price) };
            ListViewItem ret = new ListViewItem(arr);
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New_Transaction ni = (New_Transaction)senderform;// items.FindAll(r => r.item_checked)
            ni.update_items(items.FindAll(r=> r.item_checked));
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            items_filter = items.FindAll(r => r.code.Contains(textBox1.Text));
            listView1.SetObjects(items_filter);
        }

        private void listView1_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            try
            {
                item tempitem = items_filter[listView1.SelectedIndex];
                if (e.Column.Text == "Editable Price")
                {
                    string test = Convert.ToString(e.NewValue);
                    tempitem.editable_price = test;
                    items_filter[listView1.SelectedIndex] = tempitem;
                    int index = items.FindIndex(r => r.code == tempitem.code);
                    items[index] = tempitem;
                }
                else if(e.Column.Text == "Quantity")
                {
                    string test = Convert.ToString(e.NewValue);
                    tempitem.qty = test;
                    items_filter[listView1.SelectedIndex] = tempitem;
                    int index = items.FindIndex(r => r.code == tempitem.code);
                    items[index] = tempitem;
                }
            }
            catch
            {
                MessageBox.Show("Editing error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            
            item tempitem = items_filter[e.Item.Index];
            tempitem.item_checked = e.Item.Checked;
            items_filter[e.Item.Index] = tempitem;
            items[items.FindIndex(r => r.code == tempitem.code)] = tempitem;
            listView1.SetObjects(items_filter);
        }

        private void newitemcheckbox_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
