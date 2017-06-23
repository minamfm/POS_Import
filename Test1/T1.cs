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

namespace Test1
{

    public partial class T1 : Form
    {


        String x ,y ;
      

        public T1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(@" Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Ramez\Documents\Visual Studio 2015\Projects\Test1\Test1\DB1TEST.mdf ; Integrated Security = True; Connect Timeout = 30 ");

            string query = "Select * From Users WHERE usern = '"+textBox1.Text+"'and passw='" + textBox2.Text +"'";
            SqlCommand cmdq = new SqlCommand(query, conn);
            SqlDataReader rdr;
            
            try
            {
                conn.Open();
                Console.WriteLine("connected");
                rdr = cmdq.ExecuteReader();
                Console.WriteLine("tamam");
                while (rdr.Read())
                {

                    int id = (Int32)rdr.GetValue(0);
                    x = rdr.GetString(1);
                    y = rdr["passw"].ToString();

                    this.Hide();
                       Main mainmenu = new Main(id, x);
                       mainmenu.Show();
                    
                    Console.WriteLine(id +x + y);
                }

                // Call Close when done reading.
                rdr.Close();
            }

            catch
            {
                MessageBox.Show("no");
            }
            finally
            {
                conn.Close();
            }

            //    SqlDataAdapter sd = new SqlDataAdapter("Select Count(*) From Users Where usern = '"+ textBox1.Text + " ' and passw= ' " + textBox2.Text + " ' ", conn);
            //    DataTable dt = new DataTable();
            //    sd.Fill(dt);
            //    DataRow drow = dt.Rows[1];
            //    string value = drow.Field<string>("Users");
            //    Console.WriteLine(value);


            //    if (dt.Rows[0][0].ToString() == "1")
            //        {
            //           this.Hide();
            //           Main mainmenu = new Main();
            //           mainmenu.Show();
            //        }
            //        else
            //        {
            //            MessageBox.Show("ERRRRRR!");
            //        }
            //    }



        }

        //static void ReadSingleRow(IDataRecord record)
        //{
        //    Console.WriteLine(String.Format("{0}, {1} ", record[1], record[2] ));
        //}

    }




}