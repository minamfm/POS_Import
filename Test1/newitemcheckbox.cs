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


struct item
{
    public string code;
    public string name;
    public string supplier;
    public string type;
    public int qty;
    public int qtyunit;
    public float price;
    public float editable_price;

   public item (string cd, string nm, string supp, string type, int qty, int qtyunit, float price, float editable_prc)
    {
        this.code = cd;
        this.name = nm;
        this.supplier = supp;
        this.type = type;
        this.qty = qty;
        this.qtyunit = qtyunit;
        this.price = price;
        this.editable_price = editable_prc;
    }
}
namespace Test1
{
    public partial class newitemcheckbox : Form
    {
        Utilities util = new Utilities();
        List<item> items = new List<item>();
        public newitemcheckbox()
        {
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

            listView1.Columns.Add("code");
            listView1.Columns.Add("name");
            listView1.Columns.Add("supplier");
            listView1.Columns.Add("type");
            listView1.Columns.Add("qty");
            listView1.Columns.Add("qtyunit");
            listView1.Columns.Add("price");
            listView1.Columns.Add("editable_price");

            try
            {
                conn.Open();
                rdr = cmdq.ExecuteReader();

                while (rdr.Read())
                {
                    item itm = new item(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), 0, (int)rdr.GetValue(5), (float)(double)rdr.GetValue(6), (float)(double)rdr.GetValue(6));
                    items.Add(itm);
                    listView1.Items.Add(GenerateItem(itm));
                }

              
                rdr.Close();
            }
            catch
            {
                MessageBox.Show("A7a");
            }
        }
        private static ListViewItem GenerateItem(item it)
        {
            string[] arr = { it.code, it.name, it.supplier, it.type, Convert.ToString(it.qty), Convert.ToString(it.qtyunit), Convert.ToString(it.price), Convert.ToString(it.editable_price) };
            ListViewItem ret = new ListViewItem(arr);
            return ret;
        }
    }
}
