using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnProductList = new Button();
            label1 = new Label();
            btnProductIn = new Button();
            btnProductOut = new Button();
            btnHistory = new Button();
            btnClose = new Button();
            SuspendLayout();
            // 
            // btnProductList
            // 
            btnProductList.Font = new Font("Yu Gothic UI", 28F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnProductList.Location = new Point(163, 241);
            btnProductList.Margin = new Padding(2, 3, 2, 3);
            btnProductList.Name = "btnProductList";
            btnProductList.Size = new Size(425, 129);
            btnProductList.TabIndex = 0;
            btnProductList.Text = "商品管理表";
            btnProductList.UseVisualStyleBackColor = true;
            btnProductList.Click += btnProductList_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Yu Gothic UI", 48F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label1.Location = new Point(339, 29);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(670, 116);
            label1.TabIndex = 1;
            label1.Text = "在庫管理システム";
            // 
            // btnProductIn
            // 
            btnProductIn.Font = new Font("Yu Gothic UI", 28F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnProductIn.Location = new Point(163, 429);
            btnProductIn.Margin = new Padding(2, 3, 2, 3);
            btnProductIn.Name = "btnProductIn";
            btnProductIn.Size = new Size(425, 129);
            btnProductIn.TabIndex = 2;
            btnProductIn.Text = "商品入庫表";
            btnProductIn.UseVisualStyleBackColor = true;
            btnProductIn.Click += btnProductIn_Click;
            // 
            // btnProductOut
            // 
            btnProductOut.Font = new Font("Yu Gothic UI", 28F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnProductOut.Location = new Point(683, 429);
            btnProductOut.Margin = new Padding(2, 3, 2, 3);
            btnProductOut.Name = "btnProductOut";
            btnProductOut.Size = new Size(425, 129);
            btnProductOut.TabIndex = 3;
            btnProductOut.Text = "商品出庫表";
            btnProductOut.UseVisualStyleBackColor = true;
            btnProductOut.Click += btnProductOut_Click;
            // 
            // btnHistory
            // 
            btnHistory.Font = new Font("Yu Gothic UI", 28F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnHistory.Location = new Point(683, 241);
            btnHistory.Margin = new Padding(2, 3, 2, 3);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(425, 129);
            btnHistory.TabIndex = 1;
            btnHistory.Text = "入出庫履歴表";
            btnHistory.UseVisualStyleBackColor = true;
            btnHistory.Click += btnHistory_Click;
            // 
            // btnClose
            // 
            btnClose.Font = new Font("MS UI Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnClose.Location = new Point(870, 641);
            btnClose.Margin = new Padding(4, 5, 4, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(238, 81);
            btnClose.TabIndex = 4;
            btnClose.Text = "閉じる";
            btnClose.UseMnemonic = false;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(130F, 130F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1212, 951);
            Controls.Add(btnClose);
            Controls.Add(btnHistory);
            Controls.Add(btnProductIn);
            Controls.Add(btnProductOut);
            Controls.Add(label1);
            Controls.Add(btnProductList);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2, 3, 2, 3);
            MaximizeBox = false;
            MaximumSize = new Size(1230, 1000);
            MinimizeBox = false;
            MinimumSize = new Size(1230, 1000);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "在庫管理システム";
            Load += main_Load;
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProductList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnProductIn;
        private System.Windows.Forms.Button btnProductOut;
        private System.Windows.Forms.Button btnHistory;
        private Button btnClose;
    }
}