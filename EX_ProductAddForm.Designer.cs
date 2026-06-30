using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace InventoryManagementSystem
{
    partial class ProductAddForm
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
            panel1 = new Panel();
            btnClose = new Button();
            btnCancel = new Button();
            btnOk = new Button();
            txtPrice = new TextBox();
            txtProductName = new TextBox();
            txtProductCode = new TextBox();
            lblPrice = new Label();
            lblName = new Label();
            lblcheck = new Label();
            lblCode = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnOk);
            panel1.Controls.Add(txtPrice);
            panel1.Controls.Add(txtProductName);
            panel1.Controls.Add(txtProductCode);
            panel1.Controls.Add(lblPrice);
            panel1.Controls.Add(lblName);
            panel1.Controls.Add(lblcheck);
            panel1.Controls.Add(lblCode);
            panel1.Location = new Point(51, 18);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(808, 638);
            panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Font = new System.Drawing.Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnClose.Location = new Point(558, 543);
            btnClose.Margin = new Padding(2, 3, 2, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(165, 83);
            btnClose.TabIndex = 5;
            btnClose.Text = "閉じる";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new System.Drawing.Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnCancel.Location = new Point(558, 407);
            btnCancel.Margin = new Padding(2, 3, 2, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(165, 83);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "キャンセル";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.Font = new System.Drawing.Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnOk.Location = new Point(122, 407);
            btnOk.Margin = new Padding(2, 3, 2, 3);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(165, 83);
            btnOk.TabIndex = 3;
            btnOk.Text = "登録";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // txtPrice
            // 
            txtPrice.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            txtPrice.Location = new Point(255, 297);
            txtPrice.Margin = new Padding(2, 3, 2, 3);
            txtPrice.MaxLength = 8;
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(466, 45);
            txtPrice.TabIndex = 2;
            // 
            // txtProductName
            // 
            txtProductName.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            txtProductName.Location = new Point(255, 213);
            txtProductName.Margin = new Padding(2, 3, 2, 3);
            txtProductName.MaxLength = 30;
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(466, 45);
            txtProductName.TabIndex = 1;
            // 
            // txtProductCode
            // 
            txtProductCode.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            txtProductCode.Location = new Point(255, 133);
            txtProductCode.Margin = new Padding(2, 3, 2, 3);
            txtProductCode.MaxLength = 10;
            txtProductCode.Name = "txtProductCode";
            txtProductCode.Size = new Size(466, 45);
            txtProductCode.TabIndex = 0;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
            lblPrice.Location = new Point(101, 305);
            lblPrice.Margin = new Padding(2, 0, 2, 0);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(134, 32);
            lblPrice.TabIndex = 3;
            lblPrice.Text = "商品単価：";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
            lblName.Location = new Point(125, 222);
            lblName.Margin = new Padding(2, 0, 2, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(110, 32);
            lblName.TabIndex = 2;
            lblName.Text = "商品名：";
            // 
            // lblcheck
            // 
            lblcheck.AutoSize = true;
            lblcheck.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
            lblcheck.Location = new Point(35, 42);
            lblcheck.Margin = new Padding(2, 0, 2, 0);
            lblcheck.Name = "lblcheck";
            lblcheck.Size = new Size(279, 32);
            lblcheck.TabIndex = 1;
            lblcheck.Text = "項目を入力して商品を追加";
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
            lblCode.Location = new Point(99, 145);
            lblCode.Margin = new Padding(2, 0, 2, 0);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(136, 32);
            lblCode.TabIndex = 0;
            lblCode.Text = "商品コード：";
            // 
            // ProductAddForm
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(902, 693);
            Controls.Add(panel1);
            Margin = new Padding(2, 3, 2, 3);
            MinimumSize = new Size(736, 561);
            Name = "ProductAddForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "商品データ追加登録";
            Load += ProductAddForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label lblPrice;
        private Label lblName;
        private Label lblcheck;
        private Label lblCode;
        private TextBox txtPrice;
        private TextBox txtProductName;
        private TextBox txtProductCode;
        private Button btnCancel;
        private Button btnOk;
        private Button btnClose;
    }
}