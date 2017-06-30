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
        public New_Transaction()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            newitemcheckbox nich = new newitemcheckbox(this);
            nich.Show();
        }
    }
}
