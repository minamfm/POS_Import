using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Test1
{
    public partial class New_Transaction : Form
    {
        List<item> items = new List<item>();
        newitemcheckbox nich;
        List<Client> clients = new List<Client>();
        List<Rep> reps = new List<Rep>();
        Utilities utils = new Utilities();
        public New_Transaction()
        {
            nich = new newitemcheckbox(this);
            InitializeComponent();
        }
        public void update_items(List<item> items_update)
        {
            int index = 0;
            float sum = 0;
            items = items_update;
            listView1.SetObjects(items);
            listView1.AlwaysGroupBySortOrder = SortOrder.None;
            try
            {
                while (true)
                {
                    sum += Convert.ToSingle(items[index].editable_price);
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

        }
    }
}
