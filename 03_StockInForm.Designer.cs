using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace InventoryManagementSystem
{
    partial class StockInForm
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
            panel1 = new Panel();
            txtStaffName = new TextBox();
            label7 = new Label();
            btnClose = new Button();
            txtCurrentStock = new TextBox();
            label6 = new Label();
            StockInDate = new DateTimePicker();
            label5 = new Label();
            txtStockIn = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            txtProductName = new TextBox();
            txtProductCode = new TextBox();
            btnSearch = new Button();
            label1 = new Label();
            cmbProductCd = new ComboBox();
            btnStockIn = new Button();
            dgvStockIn = new DataGridView();
            ProductId = new DataGridViewTextBoxColumn();
            ProductCode = new DataGridViewTextBoxColumn();
            ProductNames = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            LogDate = new DataGridViewTextBoxColumn();
            StaffName = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStockIn).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(txtStaffName);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(txtCurrentStock);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(StockInDate);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(txtStockIn);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtProductName);
            panel1.Controls.Add(txtProductCode);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(cmbProductCd);
            panel1.Controls.Add(btnStockIn);
            panel1.Location = new Point(3, 1);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1603, 313);
            panel1.TabIndex = 0;
            // 
            // txtStaffName
            // 
            txtStaffName.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtStaffName.Location = new Point(726, 237);
            txtStaffName.Margin = new Padding(4, 5, 4, 5);
            txtStaffName.MaxLength = 30;
            txtStaffName.Name = "txtStaffName";
            txtStaffName.Size = new Size(240, 32);
            txtStaffName.TabIndex = 4;
            txtStaffName.TextAlign = HorizontalAlignment.Right;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label7.Location = new Point(619, 240);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(100, 25);
            label7.TabIndex = 17;
            label7.Text = "担当者：";
            // 
            // btnClose
            // 
            btnClose.Font = new System.Drawing.Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnClose.Location = new Point(1220, 32);
            btnClose.Margin = new Padding(2, 3, 2, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(153, 65);
            btnClose.TabIndex = 6;
            btnClose.Text = "閉じる";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // txtCurrentStock
            // 
            txtCurrentStock.Anchor = AnchorStyles.None;
            txtCurrentStock.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtCurrentStock.Location = new Point(727, 109);
            txtCurrentStock.Margin = new Padding(4, 5, 4, 5);
            txtCurrentStock.MaxLength = 10;
            txtCurrentStock.Name = "txtCurrentStock";
            txtCurrentStock.ReadOnly = true;
            txtCurrentStock.Size = new Size(240, 32);
            txtCurrentStock.TabIndex = 16;
            txtCurrentStock.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label6.Location = new Point(619, 112);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(100, 25);
            label6.TabIndex = 15;
            label6.Text = "在庫数：";
            // 
            // StockInDate
            // 
            StockInDate.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            StockInDate.Location = new Point(726, 194);
            StockInDate.Margin = new Padding(4, 5, 4, 5);
            StockInDate.Name = "StockInDate";
            StockInDate.Size = new Size(240, 32);
            StockInDate.TabIndex = 3;
            StockInDate.Value = new DateTime(2026, 6, 1, 0, 0, 0, 0);
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label5.Location = new Point(619, 199);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(100, 25);
            label5.TabIndex = 12;
            label5.Text = "入庫日：";
            // 
            // txtStockIn
            // 
            txtStockIn.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtStockIn.Location = new Point(726, 153);
            txtStockIn.Margin = new Padding(4, 5, 4, 5);
            txtStockIn.MaxLength = 5;
            txtStockIn.Name = "txtStockIn";
            txtStockIn.Size = new Size(240, 32);
            txtStockIn.TabIndex = 2;
            txtStockIn.TextAlign = HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label4.Location = new Point(619, 155);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(100, 25);
            label4.TabIndex = 10;
            label4.Text = "入庫数：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label3.Location = new Point(619, 71);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 25);
            label3.TabIndex = 9;
            label3.Text = "商品名：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label2.Location = new Point(589, 30);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(130, 25);
            label2.TabIndex = 8;
            label2.Text = "商品コード：";
            // 
            // txtProductName
            // 
            txtProductName.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtProductName.Location = new Point(726, 68);
            txtProductName.Margin = new Padding(4, 5, 4, 5);
            txtProductName.MaxLength = 30;
            txtProductName.Name = "txtProductName";
            txtProductName.ReadOnly = true;
            txtProductName.Size = new Size(240, 32);
            txtProductName.TabIndex = 7;
            txtProductName.TabStop = false;
            // 
            // txtProductCode
            // 
            txtProductCode.Anchor = AnchorStyles.None;
            txtProductCode.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtProductCode.Location = new Point(726, 27);
            txtProductCode.Margin = new Padding(4, 5, 4, 5);
            txtProductCode.MaxLength = 10;
            txtProductCode.Name = "txtProductCode";
            txtProductCode.ReadOnly = true;
            txtProductCode.Size = new Size(240, 32);
            txtProductCode.TabIndex = 0;
            txtProductCode.TabStop = false;
            // 
            // btnSearch
            // 
            btnSearch.Font = new System.Drawing.Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnSearch.Location = new Point(422, 30);
            btnSearch.Margin = new Padding(2, 3, 2, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(134, 65);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "検索";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label1.Location = new Point(12, 41);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(120, 22);
            label1.TabIndex = 3;
            label1.Text = "商品コード：";
            // 
            // cmbProductCd
            // 
            cmbProductCd.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductCd.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cmbProductCd.FormattingEnabled = true;
            cmbProductCd.Location = new Point(150, 36);
            cmbProductCd.Margin = new Padding(4, 5, 4, 5);
            cmbProductCd.Name = "cmbProductCd";
            cmbProductCd.Size = new Size(246, 33);
            cmbProductCd.TabIndex = 0;
            // 
            // btnStockIn
            // 
            btnStockIn.Font = new System.Drawing.Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnStockIn.Location = new Point(1030, 31);
            btnStockIn.Margin = new Padding(2, 3, 2, 3);
            btnStockIn.Name = "btnStockIn";
            btnStockIn.Size = new Size(153, 65);
            btnStockIn.TabIndex = 5;
            btnStockIn.Text = "入庫登録";
            btnStockIn.UseVisualStyleBackColor = true;
            btnStockIn.Click += btnStockIn_Click;
            // 
            // dgvStockIn
            // 
            dgvStockIn.AllowUserToAddRows = false;
            dgvStockIn.AllowUserToResizeColumns = false;
            dgvStockIn.AllowUserToResizeRows = false;
            dgvStockIn.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStockIn.Columns.AddRange(new DataGridViewColumn[] { ProductId, ProductCode, ProductNames, Price, Quantity, LogDate, StaffName });
            dgvStockIn.Location = new Point(3, 289);
            dgvStockIn.Margin = new Padding(2, 3, 2, 3);
            dgvStockIn.MultiSelect = false;
            dgvStockIn.Name = "dgvStockIn";
            dgvStockIn.ReadOnly = true;
            dgvStockIn.RowHeadersVisible = false;
            dgvStockIn.RowHeadersWidth = 62;
            dgvStockIn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStockIn.Size = new Size(1704, 491);
            dgvStockIn.TabIndex = 5;
            dgvStockIn.TabStop = false;
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
            Quantity.HeaderText = "入庫数量";
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
            LogDate.HeaderText = "入庫日";
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
            StaffName.Width = 260;
            // 
            // StockInForm
            // 
            AutoScaleDimensions = new SizeF(130F, 130F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1609, 951);
            Controls.Add(dgvStockIn);
            Controls.Add(panel1);
            Margin = new Padding(2, 3, 2, 3);
            MaximizeBox = false;
            MaximumSize = new Size(1627, 1000);
            MinimizeBox = false;
            MinimumSize = new Size(1627, 1000);
            Name = "StockInForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "商品入庫表";
            Load += StockInForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStockIn).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button btnClose;
        private Button btnStockIn;
        private Label label1;
        private ComboBox cmbProductCd;
        private Button btnSearch;
        private Label label2;
        private TextBox txtProductName;
        private TextBox txtProductCode;
        private Label label3;
        private TextBox txtStockIn;
        private Label label4;
        private Label label5;
        private DateTimePicker StockInDate;
        private TextBox txtCurrentStock;
        private Label label6;
        private Label label7;
        private TextBox txtStaffName;
        private DataGridView dgvStockIn;
        private DataGridViewTextBoxColumn ProductId;
        private DataGridViewTextBoxColumn ProductCode;
        private DataGridViewTextBoxColumn ProductNames;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn LogDate;
        private DataGridViewTextBoxColumn StaffName;
    }
}