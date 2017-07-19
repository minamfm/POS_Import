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
    public partial class ViewRep : Form
    {
        NewCustomerorRep sender_form;
        public ViewRep(object sender)
        {
            sender_form = (NewCustomerorRep) sender;
            InitializeComponent();
        }
        List<Rep> reps = new List<Rep>();
        private void ViewRep_Load(object sender, EventArgs e)
        {
            dataListView1.ShowGroups = false;
            Utilities utils = new Utilities();
            reps = utils.GetAllReps();
            dataListView1.SetObjects(reps);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sender_form.UpdateRep(reps[dataListView1.SelectedIndex]);
            this.Close();
        }
    }
}
