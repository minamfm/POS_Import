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

struct user
{
    public string name;
    string pass;
    string privilege;
    int id;

    public user(int idz,string n,string p,string pr)
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
        List<user> users = new List<user>();
      
        public New_user()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            user input = new user(999, textBox1.Text, textBox2.Text, textBox4.Text);
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Passwords don't match");
                return;
            }
            Test1.Utilities util = new Test1.Utilities();
            SqlConnection conn = new SqlConnection(util.GetConnectionString());

            string query = "Select * From Users";
            string command = "insert into Users (usern,passw,privileges) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "')";

            SqlCommand cmdq = new SqlCommand(query, conn);
            SqlCommand cmdw = new SqlCommand(command, conn);
            SqlDataReader rdr;

            try
            {
                conn.Open();
                
                rdr = cmdq.ExecuteReader();
                
                while(rdr.Read())
                {
                    users.Add(new user((Int32)rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3)));
                }

                if(users.Exists(r => r.name.ToLower() == input.name.ToLower()))
                {
                    MessageBox.Show("Duplicate user");
                    return;
                }
                rdr.Close();
                conn.Close();
                conn.Open();
                cmdw.ExecuteNonQuery();

                MessageBox.Show("User added successfully");
                this.Close();

            }
            catch
            {
                MessageBox.Show("A7a");
            }
        }
    }
}
