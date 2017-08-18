using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.SqlClient;

struct Transaction
{
    public string code;
    public string date;
    public int clientid;
    public float totalamount;
    public string items;
    public int sellreturn;
    public string clientname;

    public Transaction(string date, int clientid, float totalamount, string items, int sellreturn, string clientname)
    {
        this.code = "";
        this.date = date;
        this.clientid = clientid;
        this.totalamount = totalamount;
        this.items = items;
        this.sellreturn = sellreturn;
        this.clientname = clientname;
    }
    public Transaction(string code, string date, int clientid, float totalamount, string items, int sellreturn, string clientname)
    {
        this.code = code;
        this.date = date;
        this.clientid = clientid;
        this.totalamount = totalamount;
        this.items = items;
        this.sellreturn = sellreturn;
        this.clientname = clientname;
    }
}
public struct Rep
{
    public long id;
    public string repname;
    public int numclient;
    public string clientids;
    public float totalsell;

    public Rep(int id, string repname, int numclient, string clientids, float totalsell)
    {
        this.id = id;
        this.repname = repname;
        this.numclient = numclient;
        this.clientids = clientids;
        this.totalsell = totalsell;
    }

    public string Repname
    {
        set { }
        get { return this.repname; }
    }
}

public struct Client
{
    public long id;
    string name;
    public float totalsales;
    public string itemsid;
    public float cash;
    public float debit;
    public string mobi;
    public string companyname;
    public string companyadd;

    public Client(long id, string name, float totalsales, string itemsid, float cash, float debit, string mobi, string companyname, string companyadd)
    {
        this.id = id;
        this.name = name;
        this.totalsales = totalsales;
        this.itemsid = itemsid;
        this.cash = cash;
        this.debit = debit;
        this.mobi = mobi;
        this.companyname = companyname;
        this.companyadd = companyadd;

    }
    public string Name
    {
        get { return this.name; }
    }
}
namespace Test1
{
    public partial class New_Transaction : Form
    {
        List<item> items = new List<item>();
        newitemcheckbox nich;
        List<Client> clients = new List<Client>();
        List<Rep> reps = new List<Rep>();
        Utilities utils = new Utilities();
        string path;
        float sum = 0;
        public New_Transaction()
        {
            nich = new newitemcheckbox(this);
            InitializeComponent();
        }
        public void update_items(List<item> items_update)
        {

            items = items_update;
            listView1.SetObjects(items);
            listView1.AlwaysGroupBySortOrder = System.Windows.Forms.SortOrder.None;
                sum = 0;
                for (int index =0; index < items.Count; index++)
                {
                    sum += Convert.ToSingle(items[index].editable_price) * Convert.ToSingle(items[index].qty);
                    index++;
                }
            Amount.Text = sum.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            nich.Show();
        }

        private void New_Transaction_Load(object sender, EventArgs e)
        {
           
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.AlwaysGroupBySortOrder = System.Windows.Forms.SortOrder.None;
            listView1.ShowGroups = false;
            clients = utils.GetAllClients();
            textBox1.Text = DateTime.Today.ToString("dd/MM/yyyy");

            comboBox1.DisplayMember = "Name";
            comboBox1.DataSource = clients;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to update", "Check ? ", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes )
            {
                if (path == null)
                {
                    MessageBox.Show("Please select a Path");
                
                }
                else
                {
                    string items_in_transaction = "";  
                   

                    for (int i = 0; i < items.Count; i++)
                    {
                        items_in_transaction = items_in_transaction + items[i].code + "&&++" + items[i].qty + "++&&";
                    }
                    Transaction trans = new Transaction(textBox1.Text, (int)clients[comboBox1.SelectedIndex].id, sum, items_in_transaction, 0, clients[comboBox1.SelectedIndex].Name);
                    utils.InsertTranscation(trans);

                    string codetrans = null;
                    try
                    {
                        SqlConnection conn = new SqlConnection(utils.GetConnectionString());
                        
                        string query = "Select * from Transactions WHERE date = @date AND items =@items AND clientname =@clientname ";
                        
                        SqlCommand cmdq = new SqlCommand(query, conn);

                        cmdq.Parameters.AddWithValue("@date", textBox1.Text);
                        cmdq.Parameters.AddWithValue("@items", items_in_transaction);
                        cmdq.Parameters.AddWithValue("@clientname", comboBox1.Text);

                        SqlDataReader rdr;
                        conn.Open();

                        rdr = cmdq.ExecuteReader();
                        while (rdr.Read())
                        {
                            codetrans = Convert.ToString(rdr.GetInt32(0));

                        }

                        rdr.Close();
                        conn.Close();

                    } catch
                    {

                    }

                    path = path + comboBox1.Text + codetrans + ".pdf";
                    DataTable table = ConvertListToDataTable(items);
                    ExportDataTableToPdf(table, path, "شركة فادي كو" , codetrans);
                    MessageBox.Show("Saved successfully");
                    this.Close();
                }
                }

            
        }
        DataTable ConvertListToDataTable(List<item> list)
        {
            DataTable table = new DataTable();
            table.Columns.Add("كود");
            table.Columns.Add("اسم الصنف");
            table.Columns.Add("الشركة");
            table.Columns.Add("الوصف");
            table.Columns.Add("الكمية");
            table.Columns.Add("السعر");
            table.Columns.Add("السعر بعد الخصم");


            foreach (item it in list)
            {
                table.Rows.Add(it.code, it.name, it.supplier, it.type,it.qty,it.price,it.editable_price);
            }

            return table;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nich.Close();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {

                textBox2.Text = folderBrowserDialog1.SelectedPath;
              
                path = textBox2.Text + "//";

            }
        }

        void ExportDataTableToPdf(DataTable dtblTable, String strPdfPath, string strHeader , string cde)
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
            prgAuthor.Add(new Chunk(""+cde+"          بيان مبيعات", f1));
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
            ammount.Add(new Chunk(Amount.Text, fll));


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
            End.Add(new Chunk("سياسة المرتجع  من 15 الي 30 يوم علماً ان تكون السلع و الاصناف بالحالة الاساسية", fm));
           
            c3.AddElement(End);
            c3.Border = Rectangle.NO_BORDER;

            M.AddCell(c3);
            document.Add(M);

            document.Close();
            writer.Close();
            fs.Close();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox1.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy"); 
        }
    }
}
