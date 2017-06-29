using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    public partial class Inventorygrid : Form
    {
        string x;
        public Inventorygrid(DataTable dt)
        {
            InitializeComponent();
            dataGridView1.DataSource = dt;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please Insert Path");
            }
            else
            {
                if(string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Please Enter a Name");
                }else
                {
                    ExportEx(dataGridView1, textBox2.Text, x);
                }
               
            }
            
        }

        public void ExportEx(DataGridView gr, string xlname, string path)
        {
            Microsoft.Office.Interop.Excel.Application obj = new Microsoft.Office.Interop.Excel.Application();
            obj.Application.Workbooks.Add(Type.Missing);
            obj.Columns.ColumnWidth = 25;
            for (int i = 1; i < gr.Columns.Count + 1; i++)
            {
                obj.Cells[1, i] = gr.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < gr.Rows.Count; i++)
            {
                for (int j = 0; j < gr.Columns.Count; j++)
                {
                    obj.Cells[i + 2, j + 1] = gr.Rows[i].Cells[j].Value.ToString();
                }
            }

           string xlxname = "\\" + xlname ; 
            obj.ActiveWorkbook.SaveCopyAs( path + xlxname + ".xlsx");
            MessageBox.Show("Done :) ! ");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
               
                    textBox1.Text = folderBrowserDialog1.SelectedPath;
                    x = textBox1.Text  ;
               
            }
        }
    }
    
}
