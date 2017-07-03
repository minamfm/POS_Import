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




public struct itemcdeqty
{
    public string code;
    public string qty;

    public itemcdeqty(string cde, string qt)
    {
        this.code = cde;
        this.qty = qt;
    }
}
public struct itemdisp
{
    public string codefinal;
    public string namefinal;
    public string qtyfinal;
    public string qtyufinal;
    public string supplierfinal;
    public bool x;
    public string oldqty;

    public itemdisp(string x1, string x2, string x3, string x4, string x5, bool x6, string x7)
    {
        this.codefinal = x1;
        this.namefinal = x2;
        this.qtyfinal = x3;
        this.qtyufinal = x4;
        this.supplierfinal = x5;
        this.x = x6;
        this.oldqty = x7;
    }

}


namespace Test1
{
    public partial class returntran : Form
    {

        string x1;
        string date1;
        string clientid1;
        string total1;
        string stuff1;
        string buysell1;
        string clientname1;

        //Object senderform;

        List<itemcdeqty> itemx = new List<itemcdeqty>();
        List<itemdisp> itemdisplay = new List<itemdisp>();

        public returntran(string x, string date, string clientid, string total, string stuff, string buysell, string clientname)
        {
            x1 = x;
            date1 = date;
            clientid1 = clientid;
            total1 = total;
            stuff1 = stuff;
            buysell1 = buysell;
            clientname1 = clientname;
            InitializeComponent();
            getitems();

        }


        private void returntran_Load(object sender, EventArgs e)
        {
            textBox1.Text = x1;
            textBox1.ReadOnly = true;
            textBox2.Text = clientid1;
            textBox2.ReadOnly = true;
            textBox3.Text = clientname1;
            textBox3.ReadOnly = true;
            textBox4.Text = total1;
            textBox4.ReadOnly = true;
            textBox5.Text = date1;
            textBox5.ReadOnly = true;

            queryitem();

        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void getitems() // parsing string of items bought (stuff)
        {



            string temp_code = null;
            string temp_qty = null;

            while (stuff1 != "")
            {


                if (stuff1.IndexOf("&&++") >= 0)
                {

                    temp_code = stuff1.Substring(0, stuff1.IndexOf("&&++"));
                    stuff1 = stuff1.Substring(stuff1.IndexOf("&&++") + 4);

                    if (stuff1.IndexOf("++&&") >= 0)
                    {
                        temp_qty = stuff1.Substring(0, stuff1.IndexOf("++&&"));
                        stuff1 = stuff1.Substring(stuff1.IndexOf("++&&") + 4);

                    }

                    itemx.Add(new itemcdeqty(temp_code, temp_qty));

                }

            }
        }

        void queryitem() // Get items from db and adding it to List <itemdisp> ..
        {
            Test1.Utilities util = new Utilities();
            SqlConnection conn = new SqlConnection(util.GetConnectionString());
            string query = "Select * from Item";

            SqlCommand cmdq = new SqlCommand(query, conn);

            SqlDataReader rdr;

            objectListView1.CheckBoxes = true;
            objectListView1.View = View.Details;
            objectListView1.GridLines = true;
            objectListView1.FullRowSelect = true;
            objectListView1.AlwaysGroupBySortOrder = System.Windows.Forms.SortOrder.None;


            try
            {
                conn.Open();
                rdr = cmdq.ExecuteReader();

                while (rdr.Read())
                {
                    for (int i = 0; i < itemx.Count; i++)
                    {
                        if (itemx[i].code == rdr.GetString(0))
                        {
                            itemdisplay.Add(new itemdisp(rdr.GetString(0), rdr.GetString(1), itemx[i].qty,
                                rdr.GetValue(5).ToString(), rdr.GetString(3), false, "0"));

                            MessageBox.Show(itemx[i].qty);
                        }

                    }



                }




                objectListView1.SetObjects(itemdisplay);
                rdr.Close();
                conn.Close();

            }
            catch
            {
                MessageBox.Show("Database access error");
            }
        }

        private void listView1_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            try
            {
                itemdisp tempitem = itemdisplay[objectListView1.SelectedIndex];
                tempitem.oldqty = tempitem.qtyfinal;

                if (e.Column.Text == "Quantity")
                {
                    string test = Convert.ToString(e.NewValue);
                    if (Convert.ToInt32(test) > Convert.ToInt32(tempitem.oldqty))
                    {
                        MessageBox.Show("Quantity greater than initial sale");
                        objectListView1.SetObjects(itemdisplay);
                        return;
                    }
                    tempitem.qtyfinal = test;

                    itemdisplay[objectListView1.SelectedIndex] = tempitem;
                    itemdisplay[objectListView1.SelectedIndex] = tempitem;

                }

            }
            catch
            {
                MessageBox.Show("Editing error");
            }
        }


        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

            itemdisp tempitem = itemdisplay[e.Item.Index];
            tempitem.x = e.Item.Checked;
            itemdisplay[e.Item.Index] = tempitem;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool itemchecked = false;
            if (!string.IsNullOrEmpty(textBox6.Text))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to update", "Check ? ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                Test1.Utilities util = new Utilities();
                Transaction transaction = new Transaction();
                transaction.clientname = textBox3.Text;
                transaction.clientid = Convert.ToInt32(textBox2.Text);
                transaction.totalamount = Convert.ToSingle("-" + textBox6.Text);
                transaction.date = "03072017";
                transaction.sellreturn = 1;

                foreach (itemdisp item in itemdisplay)
                {
                    if ((item.x == true))
                    {
                        itemchecked = true;
                        transaction.items += item.codefinal + "&&++" + "-"+item.qtyfinal + "++&&";
                        try
                        {

                            MessageBox.Show("Information updated");

                        }
                        catch
                        {
                            MessageBox.Show("Failed");
                        }
                    }
                }
                if (itemchecked)
                    util.InsertTranscation(transaction);
                else
                    MessageBox.Show("No items checked!");
            }
            else
            {
                MessageBox.Show("Please Insert a value ");
            }
        }


    }


}




