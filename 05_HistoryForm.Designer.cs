using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace InventoryManagementSystem
{
    partial class HistoryForm
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
            panelFilter = new Panel();
            panel1 = new Panel();
            RadioStockAll = new RadioButton();
            RadioStockIn = new RadioButton();
            RadioStockOut = new RadioButton();
            txtProductName = new TextBox();
            label1 = new Label();
            RadioBetween = new RadioButton();
            RadioAll = new RadioButton();
            lblStartData = new Label();
            RadioThisMonth = new RadioButton();
            label4 = new Label();
            label3 = new Label();
            txtStaffName = new TextBox();
            label2 = new Label();
            cmbProductCode = new ComboBox();
            lblSearch = new Label();
            btnSearch = new Button();
            lblProductName = new Label();
            dtpEndDate = new DateTimePicker();
            lblEndDate = new Label();
            dtpStartDate = new DateTimePicker();
            btnClose = new Button();
            dgvHistory = new DataGridView();
            LogDate = new DataGridViewTextBoxColumn();
            ProductCode = new DataGridViewTextBoxColumn();
            ProductNames = new DataGridViewTextBoxColumn();
            Category = new DataGridViewTextBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            StaffName = new DataGridViewTextBoxColumn();
            panelFilter.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            SuspendLayout();
            // 
            // panelFilter
            // 
            panelFilter.BackColor = Color.Transparent;
            panelFilter.Controls.Add(panel1);
            panelFilter.Controls.Add(txtProductName);
            panelFilter.Controls.Add(label1);
            panelFilter.Controls.Add(RadioBetween);
            panelFilter.Controls.Add(RadioAll);
            panelFilter.Controls.Add(lblStartData);
            panelFilter.Controls.Add(RadioThisMonth);
            panelFilter.Controls.Add(label4);
            panelFilter.Controls.Add(label3);
            panelFilter.Controls.Add(txtStaffName);
            panelFilter.Controls.Add(label2);
            panelFilter.Controls.Add(cmbProductCode);
            panelFilter.Controls.Add(lblSearch);
            panelFilter.Controls.Add(btnSearch);
            panelFilter.Controls.Add(lblProductName);
            panelFilter.Controls.Add(dtpEndDate);
            panelFilter.Controls.Add(lblEndDate);
            panelFilter.Controls.Add(dtpStartDate);
            panelFilter.Location = new Point(81, 12);
            panelFilter.Margin = new Padding(2, 3, 2, 3);
            panelFilter.Name = "panelFilter";
            panelFilter.Size = new Size(1517, 275);
            panelFilter.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Controls.Add(RadioStockAll);
            panel1.Controls.Add(RadioStockIn);
            panel1.Controls.Add(RadioStockOut);
            panel1.Location = new Point(15, 152);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(271, 111);
            panel1.TabIndex = 29;
            // 
            // RadioStockAll
            // 
            RadioStockAll.AutoSize = true;
            RadioStockAll.Font = new System.Drawing.Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            RadioStockAll.Location = new Point(14, 7);
            RadioStockAll.Margin = new Padding(2, 3, 2, 3);
            RadioStockAll.Name = "RadioStockAll";
            RadioStockAll.Size = new Size(185, 23);
            RadioStockAll.TabIndex = 0;
            RadioStockAll.TabStop = true;
            RadioStockAll.Text = "入庫＆出庫を表示";
            RadioStockAll.UseVisualStyleBackColor = true;
            // 
            // RadioStockIn
            // 
            RadioStockIn.AutoSize = true;
            RadioStockIn.Font = new System.Drawing.Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            RadioStockIn.Location = new Point(13, 44);
            RadioStockIn.Margin = new Padding(2, 3, 2, 3);
            RadioStockIn.Name = "RadioStockIn";
            RadioStockIn.Size = new Size(145, 23);
            RadioStockIn.TabIndex = 1;
            RadioStockIn.TabStop = true;
            RadioStockIn.Text = "入庫のみ表示";
            RadioStockIn.UseVisualStyleBackColor = true;
            // 
            // RadioStockOut
            // 
            RadioStockOut.AutoSize = true;
            RadioStockOut.Font = new System.Drawing.Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            RadioStockOut.Location = new Point(13, 79);
            RadioStockOut.Margin = new Padding(2, 3, 2, 3);
            RadioStockOut.Name = "RadioStockOut";
            RadioStockOut.Size = new Size(145, 23);
            RadioStockOut.TabIndex = 2;
            RadioStockOut.TabStop = true;
            RadioStockOut.Text = "出庫のみ表示";
            RadioStockOut.UseVisualStyleBackColor = true;
            // 
            // txtProductName
            // 
            txtProductName.Font = new System.Drawing.Font("MS UI Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtProductName.Location = new Point(828, 98);
            txtProductName.Margin = new Padding(2, 3, 2, 3);
            txtProductName.MaxLength = 30;
            txtProductName.Name = "txtProductName";
            txtProductName.ReadOnly = true;
            txtProductName.Size = new Size(250, 33);
            txtProductName.TabIndex = 28;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(710, 101);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(101, 30);
            label1.TabIndex = 27;
            label1.Text = "商品名：";
            // 
            // RadioBetween
            // 
            RadioBetween.AutoSize = true;
            RadioBetween.Font = new System.Drawing.Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            RadioBetween.Location = new Point(28, 23);
            RadioBetween.Margin = new Padding(2, 3, 2, 3);
            RadioBetween.Name = "RadioBetween";
            RadioBetween.Size = new Size(157, 23);
            RadioBetween.TabIndex = 2;
            RadioBetween.TabStop = true;
            RadioBetween.Text = "期間を指定する";
            RadioBetween.UseVisualStyleBackColor = true;
            // 
            // RadioAll
            // 
            RadioAll.AutoSize = true;
            RadioAll.Font = new System.Drawing.Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            RadioAll.Location = new Point(255, 123);
            RadioAll.Margin = new Padding(2, 3, 2, 3);
            RadioAll.Name = "RadioAll";
            RadioAll.Size = new Size(202, 23);
            RadioAll.TabIndex = 1;
            RadioAll.TabStop = true;
            RadioAll.Text = "全期間の履歴を表示";
            RadioAll.UseVisualStyleBackColor = true;
            RadioAll.CheckedChanged += radioAll_CheckedChanged;
            // 
            // lblStartData
            // 
            lblStartData.AutoSize = true;
            lblStartData.Font = new System.Drawing.Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblStartData.Location = new Point(187, 23);
            lblStartData.Margin = new Padding(2, 0, 2, 0);
            lblStartData.Name = "lblStartData";
            lblStartData.Size = new Size(101, 30);
            lblStartData.TabIndex = 1;
            lblStartData.Text = "開始日：";
            // 
            // RadioThisMonth
            // 
            RadioThisMonth.AutoSize = true;
            RadioThisMonth.Font = new System.Drawing.Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            RadioThisMonth.Location = new Point(28, 123);
            RadioThisMonth.Margin = new Padding(2, 3, 2, 3);
            RadioThisMonth.Name = "RadioThisMonth";
            RadioThisMonth.Size = new Size(202, 23);
            RadioThisMonth.TabIndex = 0;
            RadioThisMonth.TabStop = true;
            RadioThisMonth.Text = "今月分の履歴を表示";
            RadioThisMonth.UseVisualStyleBackColor = true;
            RadioThisMonth.CheckedChanged += radioThisMonth_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(687, 67);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(464, 30);
            label4.TabIndex = 25;
            label4.Text = "※未選択可【未選択の場合は全商品コードを表示】";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(687, 189);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(567, 30);
            label3.TabIndex = 24;
            label3.Text = "※未入力・あいまい検索可【未入力の場合は全担当者を表示】";
            // 
            // txtStaffName
            // 
            txtStaffName.Font = new System.Drawing.Font("MS UI Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtStaffName.Location = new Point(828, 142);
            txtStaffName.Margin = new Padding(2, 3, 2, 3);
            txtStaffName.MaxLength = 30;
            txtStaffName.Name = "txtStaffName";
            txtStaffName.Size = new Size(250, 33);
            txtStaffName.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label2.Location = new Point(710, 143);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(101, 30);
            label2.TabIndex = 22;
            label2.Text = "担当者：";
            // 
            // cmbProductCode
            // 
            cmbProductCode.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cmbProductCode.FormattingEnabled = true;
            cmbProductCode.Location = new Point(828, 23);
            cmbProductCode.Margin = new Padding(4, 5, 4, 5);
            cmbProductCode.Name = "cmbProductCode";
            cmbProductCode.Size = new Size(250, 33);
            cmbProductCode.TabIndex = 2;
            cmbProductCode.SelectedIndexChanged += cmbProductCode_SelectedIndexChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new System.Drawing.Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblSearch.Location = new Point(735, 232);
            lblSearch.Margin = new Padding(2, 0, 2, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(79, 30);
            lblSearch.TabIndex = 7;
            lblSearch.Text = "検索：";
            // 
            // btnSearch
            // 
            btnSearch.Font = new System.Drawing.Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnSearch.Location = new Point(828, 220);
            btnSearch.Margin = new Padding(2, 3, 2, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(108, 49);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "検索";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Font = new System.Drawing.Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblProductName.Location = new Point(687, 23);
            lblProductName.Margin = new Padding(2, 0, 2, 0);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(124, 30);
            lblProductName.TabIndex = 4;
            lblProductName.Text = "商品コード：";
            // 
            // dtpEndDate
            // 
            dtpEndDate.Font = new System.Drawing.Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            dtpEndDate.Location = new Point(304, 67);
            dtpEndDate.Margin = new Padding(2, 3, 2, 3);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(264, 36);
            dtpEndDate.TabIndex = 1;
            dtpEndDate.Value = new DateTime(2026, 6, 14, 15, 59, 2, 0);
            // 
            // lblEndDate
            // 
            lblEndDate.AutoSize = true;
            lblEndDate.Font = new System.Drawing.Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblEndDate.Location = new Point(187, 67);
            lblEndDate.Margin = new Padding(2, 0, 2, 0);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new Size(101, 30);
            lblEndDate.TabIndex = 2;
            lblEndDate.Text = "終了日：";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Font = new System.Drawing.Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            dtpStartDate.Location = new Point(304, 23);
            dtpStartDate.Margin = new Padding(2, 3, 2, 3);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(264, 36);
            dtpStartDate.TabIndex = 0;
            dtpStartDate.Value = new DateTime(2026, 6, 14, 15, 58, 55, 0);
            // 
            // btnClose
            // 
            btnClose.Font = new System.Drawing.Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnClose.Location = new Point(1250, 719);
            btnClose.Margin = new Padding(2, 3, 2, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(108, 49);
            btnClose.TabIndex = 0;
            btnClose.Text = "閉じる";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToResizeColumns = false;
            dgvHistory.AllowUserToResizeRows = false;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistory.Columns.AddRange(new DataGridViewColumn[] { LogDate, ProductCode, ProductNames, Category, Quantity, StaffName });
            dgvHistory.Location = new Point(-1, 293);
            dgvHistory.Margin = new Padding(2, 3, 2, 3);
            dgvHistory.MultiSelect = false;
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowHeadersWidth = 62;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(1469, 420);
            dgvHistory.TabIndex = 6;
            dgvHistory.TabStop = false;
            dgvHistory.CellFormatting += dgvHistory_CellFormatting;
            // 
            // LogDate
            // 
            LogDate.DataPropertyName = "LogDate";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            LogDate.DefaultCellStyle = dataGridViewCellStyle1;
            LogDate.Frozen = true;
            LogDate.HeaderText = "入出庫日";
            LogDate.MaxInputLength = 30;
            LogDate.MinimumWidth = 6;
            LogDate.Name = "LogDate";
            LogDate.ReadOnly = true;
            LogDate.Width = 120;
            // 
            // ProductCode
            // 
            ProductCode.DataPropertyName = "ProductCode";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ProductCode.DefaultCellStyle = dataGridViewCellStyle2;
            ProductCode.Frozen = true;
            ProductCode.HeaderText = "商品コード";
            ProductCode.MaxInputLength = 10;
            ProductCode.MinimumWidth = 6;
            ProductCode.Name = "ProductCode";
            ProductCode.ReadOnly = true;
            ProductCode.Width = 170;
            // 
            // ProductNames
            // 
            ProductNames.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ProductNames.DefaultCellStyle = dataGridViewCellStyle3;
            ProductNames.Frozen = true;
            ProductNames.HeaderText = "商品名";
            ProductNames.MaxInputLength = 30;
            ProductNames.MinimumWidth = 6;
            ProductNames.Name = "ProductNames";
            ProductNames.ReadOnly = true;
            ProductNames.Width = 285;
            // 
            // Category
            // 
            Category.DataPropertyName = "Category";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Category.DefaultCellStyle = dataGridViewCellStyle4;
            Category.HeaderText = "区分";
            Category.MaxInputLength = 5;
            Category.MinimumWidth = 6;
            Category.Name = "Category";
            Category.ReadOnly = true;
            Category.Resizable = DataGridViewTriState.True;
            Category.Width = 70;
            // 
            // Quantity
            // 
            Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            Quantity.DefaultCellStyle = dataGridViewCellStyle5;
            Quantity.HeaderText = "数量";
            Quantity.MinimumWidth = 6;
            Quantity.Name = "Quantity";
            Quantity.ReadOnly = true;
            Quantity.Width = 120;
            // 
            // StaffName
            // 
            StaffName.DataPropertyName = "StaffName";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            StaffName.DefaultCellStyle = dataGridViewCellStyle6;
            StaffName.HeaderText = "担当者";
            StaffName.MaxInputLength = 30;
            StaffName.MinimumWidth = 6;
            StaffName.Name = "StaffName";
            StaffName.ReadOnly = true;
            StaffName.Width = 300;
            // 
            // HistoryForm
            // 
            AutoScaleDimensions = new SizeF(130F, 130F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1609, 951);
            Controls.Add(btnClose);
            Controls.Add(dgvHistory);
            Controls.Add(panelFilter);
            Margin = new Padding(2, 3, 2, 3);
            MaximizeBox = false;
            MaximumSize = new Size(1627, 1000);
            MinimumSize = new Size(1627, 1000);
            Name = "HistoryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "入出庫履歴表";
            Load += HistoryForm_Load;
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private Panel panelFilter;
        private Label lblStartData;
        private DateTimePicker dtpStartDate;
        private Label lblEndDate;
        private Label lblSearch;
        private Button btnSearch;
        private Label lblProductName;
        private DateTimePicker dtpEndDate;
        private DataGridView dgvHistory;
        private ComboBox cmbProductCode;
        private RadioButton RadioAll;
        private RadioButton RadioThisMonth;
        private RadioButton RadioStockAll;
        private RadioButton RadioStockIn;
        private RadioButton RadioStockOut;
        private Label label2;
        private Label label3;
        private TextBox txtStaffName;
        private Label label4;
        private Button btnClose;
        private RadioButton RadioBetween;
        private TextBox txtProductName;
        private Label label1;
        private Panel panel1;
        private DataGridViewTextBoxColumn LogDate;
        private DataGridViewTextBoxColumn ProductCode;
        private DataGridViewTextBoxColumn ProductNames;
        private DataGridViewTextBoxColumn Category;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn StaffName;
    }
}