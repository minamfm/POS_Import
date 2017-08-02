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
    public partial class NewCustomerorRep : Form
    {
        Rep selected_rep;
        enum modes
        {
            client,
            rep
        }
        modes mode = modes.client;
        public NewCustomerorRep()
        {
            InitializeComponent();
            ViewRep view = new ViewRep(this);
            view.Show();
            view.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ViewRep view = new ViewRep(this);
            view.Show();
        }
        public void UpdateRep(Rep rep)
        {
            selected_rep = rep;
            label6.Text = selected_rep.repname;
            label6.Visible = true;
        }

        private void NewCustomerorRep_Load(object sender, EventArgs e)
        {
            radioButton2.Select();
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                mode = modes.rep;
            else mode = modes.client;
            ModeAdapter();
        }
        private void ModeAdapter()
        {
            if (mode == modes.client)
            {
                panel1.Visible = true;
                panel2.Visible = false;
            }
            else if(mode == modes.rep)
            {
                panel1.Visible = false;
                panel2.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(mode==modes.client)
            {
                Client client = new Client(Convert.ToInt32(idbox.Text), textBox1.Text, Convert.ToSingle(textBox2.Text), "", Convert.ToSingle(textBox3.Text), Convert.ToSingle(textBox4.Text),textBox5.Text, textBox6.Text, textBox7.Text);
                Utilities utils = new Utilities();
                utils.InsertClient(client, selected_rep);
            }
            else if(mode == modes.rep)
            {
                Rep rep = new Rep(0, textBox1.Text, 0, "", Convert.ToSingle(textBox11.Text));
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                mode = modes.rep;
            else mode = modes.client;
            ModeAdapter();
        }
    }
}
