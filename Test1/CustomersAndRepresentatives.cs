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

struct Rep
{
    long id;
    public string repname;
    public int numclient;
    public string clientids;
    public float totalsell;
    
    public Rep(int id, string repname, int numclient, string clientids, float totalsell)
    {
        this.id = id;
        this.repname = repname;
        this.numclient = numclient;
        this.clientids = clientids;
        this.totalsell = totalsell;
    }

    public string Repname
    {
        set{ }
        get { return this.repname; }
    }
}

struct Client
{
    public long id;
    string name;
    public float totalsales;
    public string itemsid;
    public float cash;
    public float debit;

    public Client(long id, string name, float totalsales, string itemsid, float cash, float debit)
    {
        this.id = id;
        this.name = name;
        this.totalsales = totalsales;
        this.itemsid = itemsid;
        this.cash = cash;
        this.debit = debit;
    }
    public string Name
    {
        get { return this.name; }
    }
}

namespace Test1
{
    public partial class CustomersAndRepresentatives : Form
    {
        BindingList<Rep> Reps = new BindingList<Rep>();
        BindingList<Client> clients = new BindingList<Client>();
        BindingList<Client> temp_clients = new BindingList<Client>();
        Utilities util = new Utilities();
        bool All_Loaded = false;
        public CustomersAndRepresentatives()
        {
            InitializeComponent();
            CustomersAndRepresentatives_Initialize();
        }

        private void CustomersAndRepresentatives_Initialize()
        {
            SqlConnection conn = new SqlConnection(util.GetConnectionString());
            string query = "Select * from Represent";
            string query2 = "Select * from Client";
            SqlCommand cmdq = new SqlCommand(query, conn);
            SqlCommand cmdq2 = new SqlCommand(query2, conn);

            SqlDataReader rdr,rdr2;

            try
            {
                conn.Open();

                rdr = cmdq.ExecuteReader();

                while (rdr.Read())
                {
                    Reps.Add(new Rep(rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2), rdr.GetString(3), (float) (double)rdr.GetValue(4)));
                }
                listBox2.DisplayMember = "repname";
                listBox2.DataSource = Reps;
                rdr.Close();

                rdr2 = cmdq2.ExecuteReader();

                while (rdr2.Read())
                {
                    clients.Add(new Client(rdr2.GetInt32(0), rdr2.GetString(1),(float)(double)rdr2.GetValue(2), rdr2.GetString(3), (float)(double)rdr2.GetValue(4), (float)(double)rdr2.GetValue(5)));
                }

                All_Loaded = true;
                listBox2_SelectedIndexChanged(this, new EventArgs ());
            }
            catch
            {
                MessageBox.Show("A7a");
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClientName.Text = temp_clients[listBox3.SelectedIndex].Name;
                Purchases.Text = Convert.ToString(temp_clients[listBox3.SelectedIndex].totalsales);
                Cash.Text = Convert.ToString(temp_clients[listBox3.SelectedIndex].cash);
                Credit.Text = Convert.ToString(temp_clients[listBox3.SelectedIndex].debit);

                ClientName.Visible = true;
                Purchases.Visible = true;
                Cash.Visible = true;
                Credit.Visible = true;
            }
            catch
            {
                
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox2.SelectedIndex;
            string ids_string = Reps[index].clientids;
            RepName.Text = Reps[index].repname;
            numclients.Text = Convert.ToString(Reps[index].numclient);
            Sales.Text = Convert.ToString(Reps[index].totalsell);

            RepName.Visible = true;
            numclients.Visible = true;
            Sales.Visible = true;

            temp_clients.Clear();

            string temp_id;
            if (!All_Loaded)
                return;
            try
            {
                while (ids_string != "")
                {
                    if (ids_string.IndexOf(',') >= 0)
                    {
                        temp_id = ids_string.Substring(0, ids_string.IndexOf(','));
                        ids_string = ids_string.Substring(ids_string.IndexOf(',') + 1);
                    }
                    else //last element
                    {
                        temp_id = ids_string;
                        ids_string = "";
                    }

                    if (temp_id != "" && clients.Any(r => r.id == Convert.ToInt32(temp_id)))
                        temp_clients.Add(clients.First(r => r.id == Convert.ToInt32(temp_id)));
                }

                    listBox3.DisplayMember = "Name";
                    listBox3.DataSource = temp_clients;
                listBox3_SelectedIndexChanged(this, new EventArgs());
            }
            catch
            {
                MessageBox.Show("index changed error");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
