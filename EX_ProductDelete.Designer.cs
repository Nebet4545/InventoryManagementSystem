namespace InventoryManagementSystem
{
    partial class ProductDelete
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
            btnCdSearch = new Button();
            cmbProductList = new ComboBox();
            btnCancel = new Button();
            btnDelete = new Button();
            txtPrice = new TextBox();
            txtProductName = new TextBox();
            txtProductCode = new TextBox();
            lblPrice = new Label();
            lblName = new Label();
            lblcheck = new Label();
            lblCode = new Label();
            btnClose = new Button();
            SuspendLayout();
            // 
            // btnCdSearch
            // 
            btnCdSearch.Font = new Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnCdSearch.Location = new Point(454, 77);
            btnCdSearch.Margin = new Padding(2, 3, 2, 3);
            btnCdSearch.Name = "btnCdSearch";
            btnCdSearch.Size = new Size(132, 75);
            btnCdSearch.TabIndex = 1;
            btnCdSearch.Text = "検索";
            btnCdSearch.UseVisualStyleBackColor = true;
            btnCdSearch.Click += btnCdSearch_Click;
            // 
            // cmbProductList
            // 
            cmbProductList.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductList.Font = new Font("Yu Gothic UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cmbProductList.FormattingEnabled = true;
            cmbProductList.Location = new Point(46, 128);
            cmbProductList.Margin = new Padding(2, 3, 2, 3);
            cmbProductList.Name = "cmbProductList";
            cmbProductList.Size = new Size(246, 38);
            cmbProductList.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnCancel.Location = new Point(676, 465);
            btnCancel.Margin = new Padding(2, 3, 2, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(165, 80);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "キャンセル";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnDelete.Location = new Point(454, 465);
            btnDelete.Margin = new Padding(2, 3, 2, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(165, 80);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "削除";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // txtPrice
            // 
            txtPrice.Font = new Font("Yu Gothic UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            txtPrice.Location = new Point(454, 378);
            txtPrice.Margin = new Padding(2, 3, 2, 3);
            txtPrice.MaxLength = 8;
            txtPrice.Name = "txtPrice";
            txtPrice.ReadOnly = true;
            txtPrice.Size = new Size(386, 45);
            txtPrice.TabIndex = 19;
            txtPrice.TabStop = false;
            txtPrice.TextAlign = HorizontalAlignment.Right;
            // 
            // txtProductName
            // 
            txtProductName.Font = new Font("Yu Gothic UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            txtProductName.Location = new Point(454, 293);
            txtProductName.Margin = new Padding(2, 3, 2, 3);
            txtProductName.MaxLength = 30;
            txtProductName.Name = "txtProductName";
            txtProductName.ReadOnly = true;
            txtProductName.Size = new Size(386, 45);
            txtProductName.TabIndex = 18;
            txtProductName.TabStop = false;
            // 
            // txtProductCode
            // 
            txtProductCode.BackColor = SystemColors.Control;
            txtProductCode.Font = new Font("Yu Gothic UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            txtProductCode.Location = new Point(454, 213);
            txtProductCode.Margin = new Padding(2, 3, 2, 3);
            txtProductCode.MaxLength = 10;
            txtProductCode.Name = "txtProductCode";
            txtProductCode.ReadOnly = true;
            txtProductCode.Size = new Size(386, 45);
            txtProductCode.TabIndex = 5;
            txtProductCode.TabStop = false;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
            lblPrice.Location = new Point(322, 378);
            lblPrice.Margin = new Padding(2, 0, 2, 0);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(134, 32);
            lblPrice.TabIndex = 16;
            lblPrice.Text = "商品単価：";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
            lblName.Location = new Point(346, 293);
            lblName.Margin = new Padding(2, 0, 2, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(110, 32);
            lblName.TabIndex = 15;
            lblName.Text = "商品名：";
            // 
            // lblcheck
            // 
            lblcheck.AutoSize = true;
            lblcheck.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
            lblcheck.Location = new Point(40, 62);
            lblcheck.Margin = new Padding(2, 0, 2, 0);
            lblcheck.Name = "lblcheck";
            lblcheck.Size = new Size(185, 32);
            lblcheck.TabIndex = 14;
            lblcheck.Text = "商品コードを選択:";
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
            lblCode.Location = new Point(320, 225);
            lblCode.Margin = new Padding(2, 0, 2, 0);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(136, 32);
            lblCode.TabIndex = 13;
            lblCode.Text = "商品コード：";
            // 
            // btnClose
            // 
            btnClose.Font = new Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnClose.Location = new Point(676, 580);
            btnClose.Margin = new Padding(2, 3, 2, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(165, 80);
            btnClose.TabIndex = 4;
            btnClose.Text = "閉じる";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // ProductDelete
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(902, 693);
            Controls.Add(btnClose);
            Controls.Add(btnCdSearch);
            Controls.Add(cmbProductList);
            Controls.Add(btnCancel);
            Controls.Add(btnDelete);
            Controls.Add(txtPrice);
            Controls.Add(txtProductName);
            Controls.Add(txtProductCode);
            Controls.Add(lblPrice);
            Controls.Add(lblName);
            Controls.Add(lblcheck);
            Controls.Add(lblCode);
            Margin = new Padding(4, 5, 4, 5);
            Name = "ProductDelete";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "商品データ削除";
            Load += ProductDelete_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCdSearch;
        private System.Windows.Forms.ComboBox cmbProductList;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblcheck;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Button btnClose;
    }
}