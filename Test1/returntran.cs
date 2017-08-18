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




public struct itemcdeqty
{
    public string code;
    public string qty;

    public itemcdeqty(string cde, string qt)
    {
        this.code = cde;
        this.qty = qt;
    }
}
public struct itemdisp
{
    public string codefinal;
    public string namefinal;
    public string qtyfinal;
    public string qtyufinal;
    public string supplierfinal;
    public bool x;
    public string oldqty;

    public itemdisp(string x1, string x2, string x3, string x4, string x5, bool x6, string x7)
    {
        this.codefinal = x1;
        this.namefinal = x2;
        this.qtyfinal = x3;
        this.qtyufinal = x4;
        this.supplierfinal = x5;
        this.x = x6;
        this.oldqty = x7;
    }

}


namespace Test1
{
    public partial class returntran : Form
    {

        string x1;
        string date1;
        string clientid1;
        string total1;
        string stuff1;
        string buysell1;
        string clientname1;
        string path;
       

        //Object senderform;

        List<itemcdeqty> itemx = new List<itemcdeqty>();
        List<itemdisp> itemdisplay = new List<itemdisp>();

        public returntran(string x, string date, string clientid, string total, string stuff, string buysell, string clientname)
        {
            x1 = x;
            date1 = date;
            clientid1 = clientid;
            total1 = total;
            stuff1 = stuff;
            buysell1 = buysell;
            clientname1 = clientname;
            InitializeComponent();
            getitems();

        }


        private void returntran_Load(object sender, EventArgs e)
        {
            textBox1.Text = x1;
            textBox1.ReadOnly = true;
            textBox2.Text = clientid1;
            textBox2.ReadOnly = true;
            textBox3.Text = clientname1;
            textBox3.ReadOnly = true;
            textBox4.Text = total1;
            textBox4.ReadOnly = true;
            textBox5.Text = DateTime.Today.ToString("dd/MM/yyyy");
            textBox5.ReadOnly = true;

            queryitem();

        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void getitems() // parsing string of items bought (stuff)
        {



            string temp_code = null;
            string temp_qty = null;

            while (stuff1 != "")
            {


                if (stuff1.IndexOf("&&++") >= 0)
                {

                    temp_code = stuff1.Substring(0, stuff1.IndexOf("&&++"));
                    stuff1 = stuff1.Substring(stuff1.IndexOf("&&++") + 4);

                    if (stuff1.IndexOf("++&&") >= 0)
                    {
                        temp_qty = stuff1.Substring(0, stuff1.IndexOf("++&&"));
                        stuff1 = stuff1.Substring(stuff1.IndexOf("++&&") + 4);

                    }

                    itemx.Add(new itemcdeqty(temp_code, temp_qty));

                }

            }
        }

        void queryitem() // Get items from db and adding it to List <itemdisp> ..
        {
            Test1.Utilities util = new Utilities();
            SqlConnection conn = new SqlConnection(util.GetConnectionString());
            string query = "Select * from Item";

            SqlCommand cmdq = new SqlCommand(query, conn);

            SqlDataReader rdr;

            objectListView1.CheckBoxes = true;
            objectListView1.View = View.Details;
            objectListView1.GridLines = true;
            objectListView1.FullRowSelect = true;
            objectListView1.AlwaysGroupBySortOrder = System.Windows.Forms.SortOrder.None;


            try
            {
                conn.Open();
                rdr = cmdq.ExecuteReader();

                while (rdr.Read())
                {
                    for (int i = 0; i < itemx.Count; i++)
                    {
                        if (itemx[i].code == rdr.GetString(0))
                        {
                            itemdisplay.Add(new itemdisp(rdr.GetString(0), rdr.GetString(1), itemx[i].qty,
                                rdr.GetValue(5).ToString(), rdr.GetString(3), false, "0"));

                            MessageBox.Show(itemx[i].qty);
                        }

                    }



                }




                objectListView1.SetObjects(itemdisplay);
                rdr.Close();
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
                itemdisp tempitem = itemdisplay[objectListView1.SelectedIndex];
                tempitem.oldqty = tempitem.qtyfinal;

                if (e.Column.Text == "Quantity")
                {
                    string test = Convert.ToString(e.NewValue);
                    if (Convert.ToInt32(test) > Convert.ToInt32(tempitem.oldqty))
                    {
                        MessageBox.Show("Quantity greater than initial sale");
                        objectListView1.SetObjects(itemdisplay);
                        return;
                    }
                    tempitem.qtyfinal = test;

                    itemdisplay[objectListView1.SelectedIndex] = tempitem;
                    itemdisplay[objectListView1.SelectedIndex] = tempitem;

                }

            }
            catch
            {
                MessageBox.Show("Editing error");
            }
        }


        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

            itemdisp tempitem = itemdisplay[e.Item.Index];
            tempitem.x = e.Item.Checked;
            itemdisplay[e.Item.Index] = tempitem;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool itemchecked = false;
            if (!string.IsNullOrEmpty(textBox6.Text)&& !string.IsNullOrEmpty(textBox7.Text))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to update", "Check ? ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                Test1.Utilities util = new Utilities();
                Transaction transaction = new Transaction();
                transaction.clientname = textBox3.Text;
                transaction.clientid = Convert.ToInt32(textBox2.Text);
                transaction.totalamount = Convert.ToSingle("-" + textBox6.Text);
                transaction.date = DateTime.Today.ToString("dd/MM/yyyy");
                transaction.sellreturn = 1;

                DataTable table = new DataTable();
                table.Columns.Add("كود");
                table.Columns.Add("اسم الصنف");
                table.Columns.Add("الشركة");
                table.Columns.Add("الكمية");

                foreach (itemdisp item in itemdisplay)
                {
                    if ((item.x == true))
                    {
                        itemchecked = true;
                        transaction.items += item.codefinal + "&&++" + "-"+item.qtyfinal + "++&&";

                        table.Rows.Add(item.codefinal, item.namefinal, item.supplierfinal, item.qtyfinal);
                        try
                        {

                            MessageBox.Show("Information updated");

                        }
                        catch
                        {
                            MessageBox.Show("Failed");
                        }
                    }
                }

                if (itemchecked)
                {
                    util.InsertTranscation(transaction);

                    string codetrans = null;
                    try
                    {
                        SqlConnection conn = new SqlConnection(util.GetConnectionString());

                        string query = "Select * from Transactions WHERE date = @date AND sellreturn =@snr AND clientname =@clientname AND totalsales =@ts";

                        SqlCommand cmdq = new SqlCommand(query, conn);

                        cmdq.Parameters.AddWithValue("@date", textBox1.Text);
                        cmdq.Parameters.AddWithValue("@snr", 1);
                        cmdq.Parameters.AddWithValue("@clientname", textBox3.Text);
                        cmdq.Parameters.AddWithValue("@ts", Convert.ToSingle("-" + textBox6.Text));


                        SqlDataReader rdr;
                        conn.Open();

                        rdr = cmdq.ExecuteReader();
                        while (rdr.Read())
                        {
                            codetrans = Convert.ToString(rdr.GetInt32(0));

                        }

                        rdr.Close();
                        conn.Close();
                        path = path + codetrans + DateTime.Today.ToString("dd-MM-yyyy") + ".pdf";

                        ExportDataTableToPdf(table, path, "شركة فادي كو", codetrans);
                        MessageBox.Show("Saved successfully");
                        this.Close();

                    }
                    catch
                    {

                    }
                }
                else
                {
                    MessageBox.Show("No items checked!");
                }
                }
            else
            {
                MessageBox.Show("Please Insert a value ");
            }
        }


        void ExportDataTableToPdf(DataTable dtblTable, String strPdfPath, string strHeader, string cde)
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
            Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1);
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
            prgAuthor.Add(new Chunk("" + cde + "بيان مرتجع", f1));
            prgAuthor.Add(new Chunk("\nDate : " + textBox5.Text, f1));
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
            Paragraph ammount = new Paragraph();
            PdfPTable N = new PdfPTable(1);
            N.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            N.HorizontalAlignment = Element.ALIGN_RIGHT;
            PdfPCell cx = new PdfPCell();

            var arialFontPath99 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "trado.TTF");
            BaseFont authorf12 = BaseFont.CreateFont(arialFontPath99, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fll = new Font(authorf12, 16);
            // prgAuthor.Alignment = Element.ALIGN_LEFT;

            ammount.Font = fll;
            ammount.Add(new Chunk("الاجمالي", fll));
            ammount.Add(new Chunk(textBox6.Text, fll));


            cx.AddElement(ammount);
            cx.Border = Rectangle.NO_BORDER;

            N.AddCell(cx);
            document.Add(N);

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
            End.Add(new Chunk("سياسة المرتجع من 15 الي 30 يوم علماً ان تكون السلع و الاصناف بالحالة الاساسية", fm));

            c3.AddElement(End);
            c3.Border = Rectangle.NO_BORDER;

            M.AddCell(c3);
            document.Add(M);

            document.Close();
            writer.Close();
            fs.Close();
        }

        DataTable ConvertListToDataTable(List<item> list)
        {
            DataTable table = new DataTable();
            


            foreach (item it in list)
            {
                table.Rows.Add(it.code, it.name, it.supplier, it.type, it.qty, textBox6.Text);
            }

            return table;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {

                textBox7.Text = folderBrowserDialog1.SelectedPath;

                path = textBox7.Text + "//";

            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox5.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
        }
    }


}




