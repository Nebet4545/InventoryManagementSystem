using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace InventoryManagementSystem
{
    partial class StockOutForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            dgvStockOut = new DataGridView();
            btnClose = new Button();
            btnStockOut = new Button();
            cmbProductCd = new ComboBox();
            label1 = new Label();
            btnSearch = new Button();
            txtProductCode = new TextBox();
            txtProductName = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtStockOut = new TextBox();
            label5 = new Label();
            StockOutDate = new DateTimePicker();
            label6 = new Label();
            txtCurrentStock = new TextBox();
            label7 = new Label();
            txtStaffName = new TextBox();
            panel1 = new Panel();
            panel2 = new Panel();
            ProductId = new DataGridViewTextBoxColumn();
            ProductCode = new DataGridViewTextBoxColumn();
            ProductNames = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            LogDate = new DataGridViewTextBoxColumn();
            StaffName = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvStockOut).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvStockOut
            // 
            dgvStockOut.AllowUserToAddRows = false;
            dgvStockOut.AllowUserToResizeColumns = false;
            dgvStockOut.AllowUserToResizeRows = false;
            dgvStockOut.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStockOut.Columns.AddRange(new DataGridViewColumn[] { ProductId, ProductCode, ProductNames, Price, Quantity, LogDate, StaffName });
            dgvStockOut.Dock = DockStyle.Top;
            dgvStockOut.Location = new Point(0, 0);
            dgvStockOut.Margin = new Padding(2, 3, 2, 3);
            dgvStockOut.MultiSelect = false;
            dgvStockOut.Name = "dgvStockOut";
            dgvStockOut.ReadOnly = true;
            dgvStockOut.RowHeadersVisible = false;
            dgvStockOut.RowHeadersWidth = 62;
            dgvStockOut.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStockOut.Size = new Size(1606, 705);
            dgvStockOut.TabIndex = 4;
            dgvStockOut.TabStop = false;
            // 
            // btnClose
            // 
            btnClose.Font = new System.Drawing.Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnClose.Location = new Point(1222, 21);
            btnClose.Margin = new Padding(2, 3, 2, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(153, 65);
            btnClose.TabIndex = 0;
            btnClose.Text = "閉じる";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnStockOut
            // 
            btnStockOut.Font = new System.Drawing.Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnStockOut.Location = new Point(1017, 21);
            btnStockOut.Margin = new Padding(2, 3, 2, 3);
            btnStockOut.Name = "btnStockOut";
            btnStockOut.Size = new Size(153, 65);
            btnStockOut.TabIndex = 5;
            btnStockOut.Text = "出庫登録";
            btnStockOut.UseVisualStyleBackColor = true;
            btnStockOut.Click += btnStockOut_Click;
            // 
            // cmbProductCd
            // 
            cmbProductCd.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductCd.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cmbProductCd.FormattingEnabled = true;
            cmbProductCd.Location = new Point(135, 26);
            cmbProductCd.Margin = new Padding(4, 5, 4, 5);
            cmbProductCd.Name = "cmbProductCd";
            cmbProductCd.Size = new Size(246, 33);
            cmbProductCd.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label1.Location = new Point(11, 32);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(120, 22);
            label1.TabIndex = 3;
            label1.Text = "商品コード：";
            // 
            // btnSearch
            // 
            btnSearch.Font = new System.Drawing.Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnSearch.Location = new Point(404, 21);
            btnSearch.Margin = new Padding(2, 3, 2, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(134, 65);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "検索";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtProductCode
            // 
            txtProductCode.Anchor = AnchorStyles.None;
            txtProductCode.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtProductCode.Location = new Point(700, 23);
            txtProductCode.Margin = new Padding(4, 5, 4, 5);
            txtProductCode.MaxLength = 10;
            txtProductCode.Name = "txtProductCode";
            txtProductCode.ReadOnly = true;
            txtProductCode.Size = new Size(240, 32);
            txtProductCode.TabIndex = 6;
            txtProductCode.TabStop = false;
            // 
            // txtProductName
            // 
            txtProductName.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtProductName.Location = new Point(700, 66);
            txtProductName.Margin = new Padding(4, 5, 4, 5);
            txtProductName.MaxLength = 30;
            txtProductName.Name = "txtProductName";
            txtProductName.ReadOnly = true;
            txtProductName.Size = new Size(240, 32);
            txtProductName.TabIndex = 7;
            txtProductName.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label2.Location = new Point(562, 26);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(130, 25);
            label2.TabIndex = 8;
            label2.Text = "商品コード：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label3.Location = new Point(593, 66);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 25);
            label3.TabIndex = 9;
            label3.Text = "商品名：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label4.Location = new Point(593, 152);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(100, 25);
            label4.TabIndex = 10;
            label4.Text = "出庫数：";
            // 
            // txtStockOut
            // 
            txtStockOut.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtStockOut.Location = new Point(700, 152);
            txtStockOut.Margin = new Padding(4, 5, 4, 5);
            txtStockOut.MaxLength = 5;
            txtStockOut.Name = "txtStockOut";
            txtStockOut.Size = new Size(240, 32);
            txtStockOut.TabIndex = 2;
            txtStockOut.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label5.Location = new Point(593, 190);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(100, 25);
            label5.TabIndex = 12;
            label5.Text = "出庫日：";
            // 
            // StockOutDate
            // 
            StockOutDate.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            StockOutDate.Location = new Point(700, 190);
            StockOutDate.Margin = new Padding(4, 5, 4, 5);
            StockOutDate.Name = "StockOutDate";
            StockOutDate.Size = new Size(240, 32);
            StockOutDate.TabIndex = 3;
            StockOutDate.Value = new DateTime(2026, 6, 11, 0, 0, 0, 0);
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label6.Location = new Point(593, 109);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(100, 25);
            label6.TabIndex = 13;
            label6.Text = "在庫数：";
            // 
            // txtCurrentStock
            // 
            txtCurrentStock.Anchor = AnchorStyles.None;
            txtCurrentStock.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtCurrentStock.Location = new Point(700, 107);
            txtCurrentStock.Margin = new Padding(4, 5, 4, 5);
            txtCurrentStock.MaxLength = 10;
            txtCurrentStock.Name = "txtCurrentStock";
            txtCurrentStock.ReadOnly = true;
            txtCurrentStock.Size = new Size(240, 32);
            txtCurrentStock.TabIndex = 14;
            txtCurrentStock.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label7.Location = new Point(593, 230);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(100, 25);
            label7.TabIndex = 19;
            label7.Text = "担当者：";
            // 
            // txtStaffName
            // 
            txtStaffName.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtStaffName.Location = new Point(700, 228);
            txtStaffName.Margin = new Padding(4, 5, 4, 5);
            txtStaffName.MaxLength = 30;
            txtStaffName.Name = "txtStaffName";
            txtStaffName.Size = new Size(240, 32);
            txtStaffName.TabIndex = 4;
            txtStaffName.TextAlign = HorizontalAlignment.Right;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(txtStaffName);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(btnStockOut);
            panel1.Controls.Add(txtCurrentStock);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(txtProductName);
            panel1.Controls.Add(txtStockOut);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(StockOutDate);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtProductCode);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(cmbProductCd);
            panel1.ForeColor = Color.Black;
            panel1.Location = new Point(1, 0);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1606, 274);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(dgvStockOut);
            panel2.Location = new Point(1, 276);
            panel2.Name = "panel2";
            panel2.Size = new Size(1606, 682);
            panel2.TabIndex = 3;
            // 
            // ProductId
            // 
            ProductId.DataPropertyName = "ProductId";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ProductId.DefaultCellStyle = dataGridViewCellStyle1;
            ProductId.HeaderText = "商品ID";
            ProductId.MaxInputLength = 8;
            ProductId.MinimumWidth = 6;
            ProductId.Name = "ProductId";
            ProductId.ReadOnly = true;
            ProductId.Width = 65;
            // 
            // ProductCode
            // 
            ProductCode.DataPropertyName = "ProductCode";
            ProductCode.HeaderText = "商品コード";
            ProductCode.MaxInputLength = 10;
            ProductCode.MinimumWidth = 8;
            ProductCode.Name = "ProductCode";
            ProductCode.ReadOnly = true;
            ProductCode.Width = 200;
            // 
            // ProductNames
            // 
            ProductNames.DataPropertyName = "ProductName";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ProductNames.DefaultCellStyle = dataGridViewCellStyle2;
            ProductNames.HeaderText = "商品名";
            ProductNames.MaxInputLength = 30;
            ProductNames.MinimumWidth = 6;
            ProductNames.Name = "ProductNames";
            ProductNames.ReadOnly = true;
            ProductNames.Width = 300;
            // 
            // Price
            // 
            Price.DataPropertyName = "Price";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            Price.DefaultCellStyle = dataGridViewCellStyle3;
            Price.HeaderText = "単価";
            Price.MinimumWidth = 6;
            Price.Name = "Price";
            Price.ReadOnly = true;
            Price.Width = 75;
            // 
            // Quantity
            // 
            Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            Quantity.DefaultCellStyle = dataGridViewCellStyle4;
            Quantity.HeaderText = "出庫数量";
            Quantity.MaxInputLength = 5;
            Quantity.MinimumWidth = 6;
            Quantity.Name = "Quantity";
            Quantity.ReadOnly = true;
            Quantity.Width = 80;
            // 
            // LogDate
            // 
            LogDate.DataPropertyName = "LogDate";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.NullValue = null;
            LogDate.DefaultCellStyle = dataGridViewCellStyle5;
            LogDate.HeaderText = "出庫日";
            LogDate.MaxInputLength = 30;
            LogDate.MinimumWidth = 6;
            LogDate.Name = "LogDate";
            LogDate.ReadOnly = true;
            LogDate.Width = 120;
            // 
            // StaffName
            // 
            StaffName.DataPropertyName = "StaffName";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            StaffName.DefaultCellStyle = dataGridViewCellStyle6;
            StaffName.HeaderText = "担当者名";
            StaffName.MaxInputLength = 30;
            StaffName.MinimumWidth = 8;
            StaffName.Name = "StaffName";
            StaffName.ReadOnly = true;
            StaffName.Width = 250;
            // 
            // StockOutForm
            // 
            AutoScaleDimensions = new SizeF(130F, 130F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1609, 951);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(2, 3, 2, 3);
            MaximizeBox = false;
            MaximumSize = new Size(1627, 1000);
            MinimumSize = new Size(1627, 1000);
            Name = "StockOutForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "商品出庫表";
            Load += StockOutForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvStockOut).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private DataGridView dgvStockOut;
        private Button btnClose;
        private Button btnStockOut;
        private ComboBox cmbProductCd;
        private Label label1;
        private Button btnSearch;
        private TextBox txtProductCode;
        private TextBox txtProductName;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtStockOut;
        private Label label5;
        private DateTimePicker StockOutDate;
        private Label label6;
        private TextBox txtCurrentStock;
        private Label label7;
        private TextBox txtStaffName;
        private Panel panel1;
        private Panel panel2;
        private DataGridViewTextBoxColumn ProductId;
        private DataGridViewTextBoxColumn ProductCode;
        private DataGridViewTextBoxColumn ProductNames;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn LogDate;
        private DataGridViewTextBoxColumn StaffName;
    }
}