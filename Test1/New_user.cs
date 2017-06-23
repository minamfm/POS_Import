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

struct userz
{
    string name;
    string pass;
    string privilege;
    int id;

    public userz(int idz,string n,string p,string pr)
    {
        this.name = n;
        this.pass = p;
        this.privilege = pr;
        this.id = idz;
    }
}
namespace Test1
{
    public partial class New_user : Form
    {
        List<userz> users = new List<userz>();
        public New_user()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test1.Utilities util = new Test1.Utilities();
            SqlConnection conn = new SqlConnection(util.GetConnectionString());

            string query = "Select * From Users";
            string command = "insert into Users (usern,passw,privileges) values ('testuser','testpass','1')";

            SqlCommand cmdq = new SqlCommand(query, conn);
            SqlCommand cmdw = new SqlCommand(command, conn);
            SqlDataReader rdr;

            try
            {
                conn.Open();
                cmdw.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                rdr = cmdq.ExecuteReader();
                
                while(rdr.Read())
                {
                    users.Add(new userz((Int32)rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3)));
                }

  
            }
            catch
            {
                MessageBox.Show("A7a");
            }
        }
    }
}
