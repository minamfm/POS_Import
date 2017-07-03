using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

struct Transaction
{
    public string code;
    public string date;
    public int clientid;
    public float totalamount;
    public string items;
    public int sellreturn;
    public string clientname;

    public Transaction(string date, int clientid, float totalamount, string items, int sellreturn, string clientname)
    {
        this.code = "";
        this.date = date;
        this.clientid = clientid;
        this.totalamount = totalamount;
        this.items = items;
        this.sellreturn = sellreturn;
        this.clientname = clientname;
    }
    public Transaction(string code, string date, int clientid, float totalamount, string items, int sellreturn, string clientname)
    {
        this.code = code;
        this.date = date;
        this.clientid = clientid;
        this.totalamount = totalamount;
        this.items = items;
        this.sellreturn = sellreturn;
        this.clientname = clientname;
    }
}

namespace Test1
{
    public partial class New_Transaction : Form
    {
        List<item> items = new List<item>();
        newitemcheckbox nich;
        List<Client> clients = new List<Client>();
        List<Rep> reps = new List<Rep>();
        Utilities utils = new Utilities();
        float sum = 0;
        public New_Transaction()
        {
            nich = new newitemcheckbox(this);
            InitializeComponent();
        }
        public void update_items(List<item> items_update)
        {
            int index = 0;

            items = items_update;
            listView1.SetObjects(items);
            listView1.AlwaysGroupBySortOrder = SortOrder.None;
            try
            {
                sum = 0;
                while (true)
                {
                    sum += Convert.ToSingle(items[index].editable_price) * Convert.ToSingle(items[index].qty);
                    index++;
                }
            }
            catch
            {

            }
            Amount.Text = sum.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            nich.Show();
        }

        private void New_Transaction_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.AlwaysGroupBySortOrder = SortOrder.None;
            listView1.ShowGroups = false;
            clients = utils.GetAllClients();

            comboBox1.DisplayMember = "Name";
            comboBox1.DataSource = clients;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string items_in_transaction = "";
            for (int i = 0; i < items.Count; i++)
            {
                items_in_transaction = items_in_transaction + items[i].code + "&&++" + items[i].qty + "++&&";
            }
            Transaction trans = new Transaction(textBox1.Text, (int)clients[comboBox1.SelectedIndex].id, sum, items_in_transaction, 0, clients[comboBox1.SelectedIndex].Name);
            utils.InsertTranscation(trans);

            MessageBox.Show("Saved successfully");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nich.Close();
            this.Close();
        }
    }
}
