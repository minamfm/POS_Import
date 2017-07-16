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
            }
            else if(mode == modes.rep)
            {
                panel1.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(mode==modes.client)
            {

            }
            else if(mode == modes.rep)
            {

            }
        }
    }
}
