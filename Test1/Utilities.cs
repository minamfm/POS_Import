using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Test1
{
    class Utilities
    {
        public string GetConnectionString()
        {
            string current_dir = System.Reflection.Assembly.GetExecutingAssembly().Location;//because the database is currenly
            for (int i = 0; i < 3; i++)
            {
                current_dir = current_dir.Substring(0, current_dir.LastIndexOf('\\'));
            }

            return  (@" Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + current_dir + @"\DB1TEST.mdf ; Integrated Security = True; Connect Timeout = 30 ");
        }
        public List<Client> GetAllClients()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            string query = "Select * from Client";
            SqlCommand cmdq = new SqlCommand(query, conn);

            List<Client> clients = new List<Client>();

            SqlDataReader rdr;
            conn.Open();

            rdr = cmdq.ExecuteReader();

            while (rdr.Read())
            {
                clients.Add(new Client(rdr.GetInt32(0), rdr.GetString(1), (float)(double)rdr.GetValue(2), rdr.GetString(3), (float)(double)rdr.GetValue(4), (float)(double)rdr.GetValue(5)));
            }
            rdr.Close();
            conn.Close();
            return clients;

        }
        public List<Rep> GetAllReps()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            string query = "Select * from Represent";
            SqlCommand cmdq = new SqlCommand(query, conn);

            List<Rep> Reps = new List<Rep>();

            SqlDataReader rdr;
            conn.Open();

            rdr = cmdq.ExecuteReader();
            while (rdr.Read())
            {
                Reps.Add(new Rep(rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2), rdr.GetString(3), (float)(double)rdr.GetValue(4)));
            }

            rdr.Close();
            conn.Close();
            return Reps;
        }
    }
}
