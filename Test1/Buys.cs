using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp;
using System.IO;
using System.Data.SqlClient;

namespace Test1
{
    public partial class Buys : Form
    {
        string path;
        List<item> items = new List<item>();
        Test1.Utilities util = new Utilities();
        public Buys()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            buysitem newit = new buysitem(this);
            newit.Show();
            this.Hide();
        }

        private void Buys_Load(object sender, EventArgs e)
        {
             items = util.GetAllitems();
            name1.Enabled = false;
            name2.Enabled = false;
            name3.Enabled = false;
            name4.Enabled = false;
            name5.Enabled = false;
            supp1.Enabled = false;
            supp2.Enabled = false;
            supp3.Enabled = false;
            supp4.Enabled = false;
            supp5.Enabled = false;
            qty1.Enabled = false;
            qty2.Enabled = false;
            qty3.Enabled = false;
            qty4.Enabled = false;
            qty5.Enabled = false;
            datetext.Text = monthCalendar1.TodayDate.ToString("dd/MM/yyyy");
        
        AutoCompleteStringCollection codecollection = new AutoCompleteStringCollection();
            foreach (item it in items)
            {

                codecollection.Add(it.code);
                code1.Items.Add(it.code);
                code2.Items.Add(it.code);
                code3.Items.Add(it.code);
                code4.Items.Add(it.code);
                code5.Items.Add(it.code);
            }


            code1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            code1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            code1.AutoCompleteCustomSource = codecollection;
            
            code2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            code2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            code2.AutoCompleteCustomSource = codecollection;
         
            code3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            code3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            code3.AutoCompleteCustomSource = codecollection;

            code4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            code4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            code4.AutoCompleteCustomSource = codecollection;
            

            code5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            code5.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            code5.AutoCompleteCustomSource = codecollection;
        }

        private void code1_SelectedIndexChanged(object sender, EventArgs e)
        {
            item it = new item();
            it = items.Find(r => r.code == code1.Text);
            name1.Text = it.name;
            supp1.Text = it.supplier;
            qty1.Text = it.qty;
            newqty1.Text = "0";
            price1.Text = it.price;


        }

        private void code2_SelectedIndexChanged(object sender, EventArgs e)
        {
            item it = new item();
            it = items.Find(r => r.code == code2.Text);
            name2.Text = it.name;
            supp2.Text = it.supplier;
            qty2.Text = it.qty;
            newqty2.Text = "0";
            price2.Text = it.price;

        }

        private void code3_SelectedIndexChanged(object sender, EventArgs e)
        {
            item it = new item();
            it = items.Find(r => r.code == code3.Text);
            name3.Text = it.name;
            supp3.Text = it.supplier;
            qty3.Text = it.qty;
            newqty3.Text = "0";
            price3.Text = it.price;
        }

        private void code4_SelectedIndexChanged(object sender, EventArgs e)
        {
            item it = new item();
            it = items.Find(r => r.code == code4.Text);
            name4.Text = it.name;
            supp4.Text = it.supplier;
            qty4.Text = it.qty;
            newqty4.Text ="0";
            price4.Text = it.price;
        }

        private void code5_SelectedIndexChanged(object sender, EventArgs e)
        {
            item it = new item();
            it = items.Find(r => r.code == code5.Text);
            name5.Text = it.name;
            supp5.Text = it.supplier;
            qty5.Text = it.qty;
            newqty5.Text = "0";
            price5.Text = it.price;
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
            PdfPCell c = new PdfPCell();

            var arialFontPath1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "trado.TTF");
            BaseFont authorf = BaseFont.CreateFont(arialFontPath1, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font f1 = new Font(authorf, 18);
            prgAuthor.Alignment = Element.ALIGN_RIGHT;
            
            prgAuthor.Font = f1;
            prgAuthor.Add(new Chunk(" بيان مشتروات", f1));
            prgAuthor.Add(new Chunk("\nDate : " + datetext.Text, f1));
            c.AddElement(prgAuthor);
            c.Border =Rectangle.NO_BORDER;
            
            X.AddCell(c);
            document.Add(X);

            //Add a line seperation
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F,BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Add line break
            document.Add(new Chunk("\n", fntHead));

            //Write the table
            PdfPTable table = new PdfPTable(dtblTable.Columns.Count);
            //Table header
            var arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "tradbdo.TTF");
            BaseFont bf = BaseFont.CreateFont(arialFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font f = new Font(bf, 12);

            BaseFont btnColumnHeader = BaseFont.CreateFont(arialFontPath,BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
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
            document.Close();
            writer.Close();
            fs.Close();
        }



        DataTable ConvertListToDataTable(List<item> list)
        {
            DataTable table = new DataTable();
            table.Columns.Add("كود");
            table.Columns.Add("اسم الصنف");
            table.Columns.Add("الشركة");
            table.Columns.Add("الكمية");

            foreach (item it in list)
            {
                table.Rows.Add(it.code, it.name, it.supplier, it.qty);
            }

            return table;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to update", "Check ? ", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                List<item> chosenits = new List<item>();
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                {

                    if (!string.IsNullOrEmpty(code1.Text)&& !string.IsNullOrEmpty(name1.Text))
                {
                    item temp = new item();
                    temp.code = code1.Text;
                    temp.name =name1.Text;
                    temp.supplier = supp1.Text;
                    temp.qty = newqty1.Text;

                    chosenits.Add(temp);

                    int big_list_index = items.FindIndex(it => it.code == temp.code);
                    item temp_item = items[big_list_index];
                    temp_item.qty =Convert.ToString(Convert.ToInt32(temp_item.qty) + Convert.ToInt32(newqty1.Text));
                    items[big_list_index] = temp_item;

                       
                }
                if (!string.IsNullOrEmpty(code2.Text) && !string.IsNullOrEmpty(name2.Text))
                {
                    item temp = new item();
                    temp.code = code2.Text;
                    temp.name = name2.Text;
                    temp.supplier = supp2.Text;
                    temp.qty = newqty2.Text;
                   

                    chosenits.Add(temp);
                    int big_list_index = items.FindIndex(it => it.code == temp.code);
                    item temp_item = items[big_list_index];
                    temp_item.qty = Convert.ToString(Convert.ToInt32(temp_item.qty) + Convert.ToInt32(newqty2.Text));
                    items[big_list_index] = temp_item;
                }
                if (!string.IsNullOrEmpty(code3.Text) && !string.IsNullOrEmpty(name3.Text))
                {
                    item temp = new item();
                    temp.code = code3.Text;
                    temp.name = name3.Text;
                    temp.supplier = supp3.Text;
                    temp.qty = newqty3.Text;

                    chosenits.Add(temp);
                    int big_list_index = items.FindIndex(it => it.code == temp.code);
                    item temp_item = items[big_list_index];
                    temp_item.qty = Convert.ToString(Convert.ToInt32(temp_item.qty) + Convert.ToInt32(newqty3.Text));
                    items[big_list_index] = temp_item;
                }
                if (!string.IsNullOrEmpty(code4.Text) && !string.IsNullOrEmpty(name4.Text))
                {
                    item temp = new item();
                    temp.code = code4.Text;
                    temp.name = name4.Text;
                    temp.supplier = supp4.Text;
                    temp.qty = newqty4.Text;

                    chosenits.Add(temp);
                    int big_list_index = items.FindIndex(it => it.code == temp.code);
                    item temp_item = items[big_list_index];
                    temp_item.qty = Convert.ToString(Convert.ToInt32(temp_item.qty) + Convert.ToInt32(newqty4.Text));
                    items[big_list_index] = temp_item;
                }
                if (!string.IsNullOrEmpty(code5.Text) && !string.IsNullOrEmpty(name4.Text))
                {
                    item temp = new item();
                    temp.code = code5.Text;
                    temp.name = name5.Text;
                    temp.supplier = supp5.Text;
                    temp.qty = newqty5.Text;

                    chosenits.Add(temp);
                    int big_list_index = items.FindIndex(it => it.code == temp.code);
                    item temp_item = items[big_list_index];
                    temp_item.qty = Convert.ToString(Convert.ToInt32(temp_item.qty) + Convert.ToInt32(newqty5.Text));
                    items[big_list_index] = temp_item;
                }
                bool m = false;
                    try

                {

                     m = util.InsertItems(items);
                    if (m == true)
                    {
                        MessageBox.Show("Done");
                    }
                }
                catch
                {
                    MessageBox.Show("Error!");
                  
                }
                if (m == true)
                {
                    DataTable table = ConvertListToDataTable(chosenits);
                   
                        path = path + textBox2.Text + ".pdf";
                        ExportDataTableToPdf(table, path, "شركة فادي كو");
                        SqlConnection conn = new SqlConnection(util.GetConnectionString());
                        string itx = null;
                        foreach (item ii in chosenits)
                        {
                            itx = itx + ii.code;
                            itx = itx + "++";
                            itx = itx + ii.qty;
                            itx = itx + "&&";
                            
                        }
                        try
                        {
                            string query = "INSERT INTO Buys(date , items) VALUES (@date ,  @items)";

                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@date", datetext.Text);
                            cmd.Parameters.AddWithValue("@items", itx);


                            conn.Open();
                        
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            this.Close();
                        }
                        catch
                        {
                            MessageBox.Show("failed");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("please insert a path and a name");
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {

                    textBox1.Text = folderBrowserDialog1.SelectedPath;
                    textBox1.Enabled = false;
                    path = textBox1.Text + "//" ;

                }
            
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            datetext.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
