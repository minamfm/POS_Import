﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test1.CNR;

namespace Test1
{
    public partial class Main : Form
    {
        Test1.Utilities util = new Test1.Utilities();

        public Main(int id ,String x)
        {
            String idd = id.ToString();
            InitializeComponent();
            this.label1.Text= idd + "  " + x;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Transactions transactions = new Transactions();
            transactions.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Items item = new Items();
            item.Show();
        }
        
        
        
        //Inventory
        private void button3_Click(object sender, EventArgs e)

        {


            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

            SqlConnection conn = new SqlConnection(util.GetConnectionString());
            List<long> item_codes = new List<long>();
            List<String> item_name = new List<String>();
            List<String> item_suplier = new List<String>();
            List<String> item_type = new List<string>();
            List<long> item_qty = new List<long>();
            List<long> item_qtyunit = new List<long>();
            List<long> item_price = new List<long>();

            string query = "Select * From Item";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                conn.Open();

                Inventorygrid invent = new Inventorygrid( dt);
                invent.Show();

            }
            catch
            {
                conn.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cusnrep css = new Cusnrep();
            css.Show();
         
        }


        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Permit per = new Permit();
            per.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Buys newbuys = new Buys();
            newbuys.Show();
        }
    }
}

