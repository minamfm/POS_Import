namespace Test1
{
    partial class Permit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new BrightIdeasSoftware.DataListView();
            this.Apply = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Code = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ItemName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Supplier = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Qty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.listView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(779, 66);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(483, 33);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1285, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 42);
            this.label1.TabIndex = 2;
            this.label1.Text = "اسم العميل";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1332, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 42);
            this.label2.TabIndex = 3;
            this.label2.Text = "التاريخ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(779, 323);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(483, 31);
            this.textBox1.TabIndex = 4;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(12, 66);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 5;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(556, 1113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(350, 78);
            this.button1.TabIndex = 6;
            this.button1.Text = "طباعة و خروج";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.AllColumns.Add(this.Apply);
            this.listView1.AllColumns.Add(this.Code);
            this.listView1.AllColumns.Add(this.ItemName);
            this.listView1.AllColumns.Add(this.Supplier);
            this.listView1.AllColumns.Add(this.Qty);
            this.listView1.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
            this.listView1.CellEditUseWholeCell = false;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Apply,
            this.Code,
            this.ItemName,
            this.Supplier,
            this.Qty});
            this.listView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView1.DataSource = null;
            this.listView1.HasCollapsibleGroups = false;
            this.listView1.Location = new System.Drawing.Point(13, 449);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1442, 499);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.UseFiltering = true;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.listView1_CellEditFinished);
            this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
            // 
            // Apply
            // 
            this.Apply.AspectName = "item_checked";
            this.Apply.CheckBoxes = true;
            this.Apply.Text = "Apply";
            // 
            // Code
            // 
            this.Code.AspectName = "code";
            this.Code.Groupable = false;
            this.Code.IsEditable = false;
            this.Code.Text = "Code";
            this.Code.Width = 187;
            // 
            // ItemName
            // 
            this.ItemName.AspectName = "name";
            this.ItemName.IsEditable = false;
            this.ItemName.Text = "ItemName";
            // 
            // Supplier
            // 
            this.Supplier.AspectName = "supplier";
            this.Supplier.IsEditable = false;
            this.Supplier.Text = "Supplier";
            // 
            // Qty
            // 
            this.Qty.AspectName = "qty";
            this.Qty.Text = "Quantity";
            // 
            // Permit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1468, 1203);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "Permit";
            this.Text = "Permit";
            this.Load += new System.EventHandler(this.Permit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Button button1;
        private BrightIdeasSoftware.DataListView listView1;
        private BrightIdeasSoftware.OLVColumn Apply;
        private BrightIdeasSoftware.OLVColumn Code;
        private BrightIdeasSoftware.OLVColumn ItemName;
        private BrightIdeasSoftware.OLVColumn Supplier;
        private BrightIdeasSoftware.OLVColumn Qty;
    }
}