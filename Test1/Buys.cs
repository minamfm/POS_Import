using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp;
using System.IO;

namespace Test1
{
    public partial class Buys : Form
    {
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
            qtyunit1.Text = it.qtyunit;
            price1.Text = it.price;


        }

        private void code2_SelectedIndexChanged(object sender, EventArgs e)
        {
            item it = new item();
            it = items.Find(r => r.code == code2.Text);
            name2.Text = it.name;
            supp2.Text = it.supplier;
            qty2.Text = it.qty;
            qtyunit2.Text = it.qtyunit;
            price2.Text = it.price;

        }

        private void code3_SelectedIndexChanged(object sender, EventArgs e)
        {
            item it = new item();
            it = items.Find(r => r.code == code3.Text);
            name3.Text = it.name;
            supp3.Text = it.supplier;
            qty3.Text = it.qty;
            qtyunit3.Text = it.qtyunit;
            price3.Text = it.price;
        }

        private void code4_SelectedIndexChanged(object sender, EventArgs e)
        {
            item it = new item();
            it = items.Find(r => r.code == code4.Text);
            name4.Text = it.name;
            supp4.Text = it.supplier;
            qty4.Text = it.qty;
            qtyunit4.Text = it.qtyunit;
            price4.Text = it.price;
        }

        private void code5_SelectedIndexChanged(object sender, EventArgs e)
        {
            item it = new item();
            it = items.Find(r => r.code == code5.Text);
            name5.Text = it.name;
            supp5.Text = it.supplier;
            qty5.Text = it.qty;
            qtyunit5.Text = it.qtyunit;
            price5.Text = it.price;
        }

        void ExportDataTableToPdf(DataTable dtblTable, String strPdfPath, string strHeader)
        {
            
            System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);

            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            //Report Header
            
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntHead = new Font(bfntHead, 16, 1);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
            document.Add(prgHeading);

            //Author
            Paragraph prgAuthor = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntAuthor = new Font(btnAuthor, 8, 2);
            prgAuthor.Alignment = Element.ALIGN_RIGHT;
            prgAuthor.Add(new Chunk("XXXXX", fntAuthor));
            prgAuthor.Add(new Chunk("\nRun Date : " + DateTime.Now.ToShortDateString(), fntAuthor));
            document.Add(prgAuthor);

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
            // string fontLoc = @"c:\windows\fonts\arialuni.ttf"; 
            // BaseFont btnColumnHeader = BaseFont.CreateFont(fontLoc, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
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
            table.Columns.Add();
            table.Columns.Add();
            table.Columns.Add();
            table.Columns.Add();
            table.Columns.Add();

            foreach (item it in list)
            {
                table.Rows.Add(it.code, it.name, it.supplier, it.qty, it.qtyunit);
            }

            return table;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to update", "Check ? ", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                List<item> chosenits = new List<item>();

                if (!string.IsNullOrEmpty(code1.Text))
                {
                    item temp = new item();
                    temp.code = code1.Text;
                    temp.name =String.Format(name1.Text);
                    temp.supplier = supp1.Text;
                    temp.qty = qty1.Text;
                    temp.qtyunit = qtyunit1.Text;

                    chosenits.Add(temp);       
                }
                if (!string.IsNullOrEmpty(code2.Text))
                {
                    item temp = new item();
                    temp.code = code2.Text;
                    temp.name = name2.Text;
                    temp.supplier = supp2.Text;
                    temp.qty = qty2.Text;
                    temp.qtyunit = qtyunit2.Text;

                    chosenits.Add(temp);
                }
                if (!string.IsNullOrEmpty(code3.Text))
                {
                    item temp = new item();
                    temp.code = code3.Text;
                    temp.name = name3.Text;
                    temp.supplier = supp3.Text;
                    temp.qty = qty3.Text;
                    temp.qtyunit = qtyunit3.Text;

                    chosenits.Add(temp);
                }
                if (!string.IsNullOrEmpty(code4.Text))
                {
                    item temp = new item();
                    temp.code = code4.Text;
                    temp.name = name4.Text;
                    temp.supplier = supp4.Text;
                    temp.qty = qty4.Text;
                    temp.qtyunit = qtyunit4.Text;

                    chosenits.Add(temp);
                }
                if (!string.IsNullOrEmpty(code5.Text))
                {
                    item temp = new item();
                    temp.code = code5.Text;
                    temp.name = name5.Text;
                    temp.supplier = supp5.Text;
                    temp.qty = qty5.Text;
                    temp.qtyunit = qtyunit5.Text;

                    chosenits.Add(temp);
                }

                DataTable table = ConvertListToDataTable(chosenits);

                ExportDataTableToPdf(table, "C://Users//minam//Desktop//pdf//txt1.pdf", "Hiii");
            }

        }
    }
}
