using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
//using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    public struct itemper
    {
        public string code;
        public string name;
        public string supplier;
        public string qty;
        public bool item_checked;
        public string Code
        {
            get { return code; }
            set { }
        }
        public itemper(string cd, string nm, string supp,  string qty)
        {
            this.code = cd;
            this.name = nm;
            this.supplier = supp;
            this.qty = qty;
            this.item_checked = false;
        }
    }
    public partial class Permit : Form
    {
        List<itemper> items = new List<itemper>();
        List<itemper> items_filter = new List<itemper>();
        List<cx> cli = new List<cx>();
        List<itemper> chosenits = new List<itemper>();

        Utilities util = new Utilities();
        public Permit()
        {
            InitializeComponent();
        }

        private void Permit_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
            SqlConnection conn = new SqlConnection(util.GetConnectionString());
            string query = "Select * from Item";
            string query2 = "Select * from Client";

            SqlCommand cmdq = new SqlCommand(query, conn);
            SqlCommand cmd2 = new SqlCommand(query2, conn);

            SqlDataReader rdr,rdr2;
            listView1.CheckBoxes = true;
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.AlwaysGroupBySortOrder = System.Windows.Forms.SortOrder.None;

            try
            {
                conn.Open();
                rdr = cmdq.ExecuteReader();

                while (rdr.Read())
                {
                    itemper itm = new itemper(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), Convert.ToString(0));
                    items.Add(itm);
                }
                rdr.Close();

                rdr2 = cmd2.ExecuteReader();
                while (rdr2.Read())
                {
                    cli.Add(new cx(rdr2.GetInt32(0), rdr2.GetString(1), (float)rdr2.GetDouble(2), rdr2.GetString(3), (float)rdr2.GetDouble(4), (float)rdr2.GetDouble(5), rdr2.GetString(6), rdr2.GetString(7), rdr2.GetString(8)));
                }
                rdr2.Close();
                items_filter = items;
                listView1.SetObjects(items_filter);

                AutoCompleteStringCollection codecollection = new AutoCompleteStringCollection();

                foreach (cx c in cli)
                {
                    codecollection.Add(c.cname);
                    comboBox1.Items.Add(c.cname);

                }


                comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox1.AutoCompleteMode = AutoCompleteMode.Append;
                comboBox1.AutoCompleteCustomSource = codecollection;
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

                conn.Close();

            }
            catch
            {
                MessageBox.Show("Database access error");
            }
        }
        private void listView1_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            try
            {
                itemper tempitem = items_filter[listView1.SelectedIndex];
               if (e.Column.Text == "Quantity")
                {
                    string test = Convert.ToString(e.NewValue);
                    tempitem.qty = test;
                    items_filter[listView1.SelectedIndex] = tempitem;
                    int index = items.FindIndex(r => r.code == tempitem.code);
                    items[index] = tempitem;
                }
            }
            catch
            {
                MessageBox.Show("Editing error");
            }
        }
     
        private static ListViewItem GenerateItem(itemper it)
        {
            string[] arr = { it.code, it.name, it.supplier, Convert.ToString(it.qty) };
            ListViewItem ret = new ListViewItem(arr);
            return ret;
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

            itemper tempitem = items_filter[e.Item.Index];
            tempitem.item_checked = e.Item.Checked;
            items_filter[e.Item.Index] = tempitem;
            items[items.FindIndex(r => r.code == tempitem.code)] = tempitem;
            listView1.SetObjects(items_filter);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox1.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (itemper i in items)
            {
                if (i.item_checked == true)
                {
                    itemper temp = i;
                    chosenits.Add(temp);
                    
                }
            }
            if(comboBox1.SelectedItem != null && chosenits != null)
            {
                string path;
                string totalitems=null ;
                DataTable table;
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        string okk = folderBrowserDialog1.SelectedPath;

                        path = okk + "//" + DateTime.Today.ToString("dd-MM-yyyy") + "اذن صرف " + comboBox1.Text + ".pdf";
                        foreach (itemper i in chosenits)
                        {
                            totalitems = i.code + "&&++" + Convert.ToString(i.qty) + "++&&";
                        }
                        table = ConvertListToDataTable(chosenits);

                        SqlConnection conn = new SqlConnection(util.GetConnectionString());
                        SqlCommand cmd = new SqlCommand("Insert into Permit (date, items,clientname) Values ('" + textBox1.Text + "' ,'" + totalitems + "', N'" + comboBox1.Text + "' )", conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        ExportDataTableToPdf(table, path, "Fady Co.,");
                        this.Dispose();


                    }
                    catch
                    {

                    }
                }
            }
        }
        void ExportDataTableToPdf(DataTable dtblTable, String strPdfPath, string strHeader)
        {
            System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            writer.RunDirection = iTextSharp.text.pdf.PdfWriter.RUN_DIRECTION_RTL;

            document.Open();

            //Report Header
            var arialFontPath2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "trado.TTF");

            BaseFont bfntHead = BaseFont.CreateFont(arialFontPath2, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fntHead = new Font(bfntHead, 16, 1);
            Paragraph prgHeading = new Paragraph();
            PdfPTable Y = new PdfPTable(1);
            Y.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            PdfPCell c1 = new PdfPCell();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
            c1.AddElement(prgHeading);
            c1.Border = Rectangle.NO_BORDER;

            Y.AddCell(c1);
            document.Add(Y);

            //Author

            Paragraph prgAuthor = new Paragraph();
            PdfPTable X = new PdfPTable(1);
            X.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            X.HorizontalAlignment = Element.ALIGN_RIGHT;
            PdfPCell c = new PdfPCell();

            var arialFontPath1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "trado.TTF");
            BaseFont authorf = BaseFont.CreateFont(arialFontPath1, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font f1 = new Font(authorf, 18);
            // prgAuthor.Alignment = Element.ALIGN_LEFT;

            prgAuthor.Font = f1;
            prgAuthor.Add(new Chunk("" + comboBox1.Text + "          أذن صرف", f1));
            prgAuthor.Add(new Chunk("\nDate : " + textBox1.Text, f1));
            c.AddElement(prgAuthor);
            c.Border = Rectangle.NO_BORDER;

            X.AddCell(c);
            document.Add(X);

            //Add a line seperation
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Add line break
            document.Add(new Chunk("\n", fntHead));

            //Write the table
            PdfPTable table = new PdfPTable(dtblTable.Columns.Count);
            //Table header
            var arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "tradbdo.TTF");
            BaseFont bf = BaseFont.CreateFont(arialFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font f = new Font(bf, 12);

            BaseFont btnColumnHeader = BaseFont.CreateFont(arialFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 10, 1, BaseColor.WHITE);
            for (int i = 0; i < dtblTable.Columns.Count; i++)
            {
                Phrase phrase = new Phrase(dtblTable.Columns[i].ColumnName.ToUpper(), f);
                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                PdfPCell cell = new PdfPCell(phrase);
                cell.HorizontalAlignment = 1;
                cell.BackgroundColor = BaseColor.GRAY;
                table.AddCell(cell);
            }
            //table Data
            for (int i = 0; i < dtblTable.Rows.Count; i++)
            {
                for (int j = 0; j < dtblTable.Columns.Count; j++)
                {
                    Phrase phrase = new Phrase(dtblTable.Rows[i][j].ToString(), f);
                    PdfPCell cell = new PdfPCell(phrase);
                    cell.HorizontalAlignment = 1;
                    table.AddCell(cell);
                }
            }

            document.Add(table);
           

            //
            Paragraph l2 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.RED, Element.ALIGN_LEFT, 1)));
            document.Add(l2);
            // Extraa

            Paragraph End = new Paragraph();
            PdfPTable M = new PdfPTable(1);
            M.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            M.HorizontalAlignment = Element.ALIGN_RIGHT;
            PdfPCell c3 = new PdfPCell();

            var arialFontPath3 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "trado.TTF");
            BaseFont authorf1 = BaseFont.CreateFont(arialFontPath3, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fm = new Font(authorf1, 16);
            // prgAuthor.Alignment = Element.ALIGN_LEFT;

            End.Font = fm;
            End.Add(new Chunk("توقيع المستلم", fm));

            c3.AddElement(End);
            c3.Border = Rectangle.NO_BORDER;

            M.AddCell(c3);
            document.Add(M);

            document.Close();
            writer.Close();
            fs.Close();
        }
        DataTable ConvertListToDataTable(List<itemper> list)
        {
            DataTable table = new DataTable();
            table.Columns.Add("كود");
            table.Columns.Add("اسم الصنف");
            table.Columns.Add("الشركة");
            
            table.Columns.Add("الكمية");
     


            foreach (itemper it in list)
            {
                table.Rows.Add(it.code, it.name, it.supplier,  it.qty);
            }

            return table;
        }
    }
}
