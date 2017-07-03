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
        public List<item> GetAllitems()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            string query = "Select * from item";
            SqlCommand cmdq = new SqlCommand(query, conn);

            List<item> items = new List<item>();
            SqlDataReader rdr;
            conn.Open();
            rdr = cmdq.ExecuteReader();

            while (rdr.Read())
            {
                item itm = new item(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), Convert.ToString(rdr.GetValue(4)), Convert.ToString((int)rdr.GetValue(5)), Convert.ToString((float)(double)rdr.GetValue(6)), Convert.ToString((float)(double)rdr.GetValue(6)));
                items.Add(itm);
            }


            return items;
        }
        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();

            SqlConnection conn = new SqlConnection(GetConnectionString());
            string query = "Select * from Transactions";
            SqlCommand cmdq = new SqlCommand(query, conn);

            SqlDataReader rdr;
            conn.Open();
            rdr = cmdq.ExecuteReader();

            while(rdr.Read())
            {
                transactions.Add(new Transaction(((int)rdr.GetValue(0)).ToString(),(string)rdr.GetValue(1), (int)rdr.GetValue(2), (float)(double)rdr.GetValue(3),(string) rdr.GetValue(4),(int) rdr.GetValue(5), (string)rdr.GetValue(6)));

            }

            conn.Close();
            return transactions;
        }
        public bool InsertTranscation(Transaction trans)
        {
            bool ret = false;
            SqlConnection conn = new SqlConnection(GetConnectionString());

            string commandtrans = "insert into Transactions (date,clientid,totalsales,items,sellreturn,clientname) values ('" 
                + trans.date + "','" + trans.clientid + "','" + trans.totalamount + "','" + trans.items + "','" + trans.sellreturn + "','" + trans.clientname +"')";

            List<Client> clients = GetAllClients();

            Client client = clients.Find(r => r.id == trans.clientid);
            client.totalsales += trans.totalamount;

            string commandClient = "UPDATE Client SET totalsales='" + client.totalsales + "'WHERE clientid='" + client.id +"'";

            List<Rep> reps = GetAllReps();

            Rep rep = reps.Find(r => r.clientids.Contains(Convert.ToString(client.id)));
            rep.totalsell += trans.totalamount;

            string CommandRep = "UPDATE Represent SET totalsell='" + rep.totalsell + "'WHERE repid='" + rep.id + "'";

            List<item> items = GetAllitems();
            List<item> current_items = new List<item>();



            SqlCommand commtrans = new SqlCommand(commandtrans, conn);
            SqlCommand commClient = new SqlCommand(commandClient, conn);
            SqlCommand commRep = new SqlCommand(CommandRep,conn);
            try
            {
                conn.Open();
                commtrans.ExecuteNonQuery();
                commClient.ExecuteNonQuery();
                commRep.ExecuteNonQuery();

                while (trans.items != "")
                {
                    if (trans.items.IndexOf("&&++") >= 0)
                    {
                        string tempitem;
                        tempitem = trans.items.Substring(0, trans.items.IndexOf("&&++"));
                        item item_1 = items.Find(r => r.code == tempitem);
                        trans.items = trans.items.Substring(trans.items.IndexOf("&&++") + 4);
                        if (trans.items.IndexOf("++&&") >= 0)
                        {
                            string aykalam = trans.items.Substring(0, trans.items.IndexOf("++&&"));
                            item_1.qty = Convert.ToString(Convert.ToInt32(item_1.qty) - Convert.ToInt32(trans.items.Substring(0, trans.items.IndexOf("++&&"))));
                            trans.items = trans.items.Substring(trans.items.IndexOf("++&&") + 4);
                        }
                        SqlCommand commitem = new SqlCommand("UPDATE item SET qty='" + item_1.qty + "' WHERE Code='" + item_1.code + "'",conn);
                        commitem.ExecuteNonQuery();
                    }
                }
                    conn.Close();
            }
            catch
            {
                
            }
            return ret;
        }
    }
}
