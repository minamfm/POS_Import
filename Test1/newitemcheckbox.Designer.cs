﻿namespace Test1
{
    partial class newitemcheckbox
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listView1 = new BrightIdeasSoftware.DataListView();
            this.Code = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ItemName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Supplier = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Type = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Qty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Qtyunit = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Price = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.EditablePrice = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.listView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(549, 827);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 65);
            this.button1.TabIndex = 1;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(824, 827);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(162, 65);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.AllColumns.Add(this.Code);
            this.listView1.AllColumns.Add(this.ItemName);
            this.listView1.AllColumns.Add(this.Supplier);
            this.listView1.AllColumns.Add(this.Type);
            this.listView1.AllColumns.Add(this.Qty);
            this.listView1.AllColumns.Add(this.Qtyunit);
            this.listView1.AllColumns.Add(this.Price);
            this.listView1.AllColumns.Add(this.EditablePrice);
            this.listView1.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
            this.listView1.CellEditUseWholeCell = false;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Code,
            this.ItemName,
            this.Supplier,
            this.Type,
            this.Qty,
            this.Qtyunit,
            this.Price,
            this.EditablePrice});
            this.listView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView1.DataSource = null;
            this.listView1.Location = new System.Drawing.Point(12, 110);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1541, 695);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.UseFiltering = true;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Code
            // 
            this.Code.AspectName = "code";
            this.Code.Text = "Code";
            this.Code.Width = 187;
            // 
            // ItemName
            // 
            this.ItemName.AspectName = "name";
            this.ItemName.Text = "ItemName";
            // 
            // Supplier
            // 
            this.Supplier.AspectName = "supplier";
            this.Supplier.Text = "Supplier";
            // 
            // Type
            // 
            this.Type.AspectName = "type";
            this.Type.Text = "Type";
            // 
            // Qty
            // 
            this.Qty.AspectName = "qty";
            this.Qty.Text = "Quantity";
            // 
            // Qtyunit
            // 
            this.Qtyunit.AspectName = "qtyunit";
            this.Qtyunit.Text = "Quantity Unit";
            // 
            // Price
            // 
            this.Price.AspectName = "price";
            this.Price.Text = "Price";
            // 
            // EditablePrice
            // 
            this.EditablePrice.AspectName = "editable_price";
            this.EditablePrice.Text = "Editable Price";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(255, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(385, 31);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // newitemcheckbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1565, 904);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "newitemcheckbox";
            this.Text = "newitemcheckbox";
            this.Load += new System.EventHandler(this.newitemcheckbox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private BrightIdeasSoftware.DataListView listView1;
        private BrightIdeasSoftware.OLVColumn Code;
        private BrightIdeasSoftware.OLVColumn ItemName;
        private BrightIdeasSoftware.OLVColumn Supplier;
        private BrightIdeasSoftware.OLVColumn Type;
        private BrightIdeasSoftware.OLVColumn Qty;
        private BrightIdeasSoftware.OLVColumn Qtyunit;
        private BrightIdeasSoftware.OLVColumn Price;
        private BrightIdeasSoftware.OLVColumn EditablePrice;
        private System.Windows.Forms.TextBox textBox1;
    }
}