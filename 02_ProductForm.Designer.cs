using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    partial class ProductForm
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
            dgvProducts = new DataGridView();
            ProductId = new DataGridViewTextBoxColumn();
            ProductCd = new DataGridViewTextBoxColumn();
            ProductNames = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            ProductStock = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnClose = new Button();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToOrderColumns = true;
            dgvProducts.AllowUserToResizeColumns = false;
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { ProductId, ProductCd, ProductNames, Price, ProductStock });
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(0, 0);
            dgvProducts.Margin = new Padding(2, 3, 2, 3);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowHeadersWidth = 62;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(1211, 756);
            dgvProducts.TabIndex = 4;
            dgvProducts.TabStop = false;
            // 
            // ProductId
            // 
            ProductId.DataPropertyName = "ProductId";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ProductId.DefaultCellStyle = dataGridViewCellStyle1;
            ProductId.FillWeight = 53.9772758F;
            ProductId.HeaderText = "商品ID";
            ProductId.MaxInputLength = 8;
            ProductId.MinimumWidth = 6;
            ProductId.Name = "ProductId";
            ProductId.ReadOnly = true;
            ProductId.Resizable = DataGridViewTriState.False;
            ProductId.Width = 150;
            // 
            // ProductCd
            // 
            ProductCd.DataPropertyName = "ProductCode";
            ProductCd.FillWeight = 53.9772758F;
            ProductCd.HeaderText = "商品コード";
            ProductCd.MaxInputLength = 10;
            ProductCd.MinimumWidth = 8;
            ProductCd.Name = "ProductCd";
            ProductCd.ReadOnly = true;
            ProductCd.SortMode = DataGridViewColumnSortMode.NotSortable;
            ProductCd.Width = 200;
            // 
            // ProductNames
            // 
            ProductNames.DataPropertyName = "ProductName";
            ProductNames.FillWeight = 53.9772758F;
            ProductNames.HeaderText = "商品名";
            ProductNames.MaxInputLength = 30;
            ProductNames.MinimumWidth = 8;
            ProductNames.Name = "ProductNames";
            ProductNames.ReadOnly = true;
            ProductNames.SortMode = DataGridViewColumnSortMode.NotSortable;
            ProductNames.Width = 250;
            // 
            // Price
            // 
            Price.DataPropertyName = "Price";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            Price.DefaultCellStyle = dataGridViewCellStyle2;
            Price.FillWeight = 53.9772758F;
            Price.HeaderText = "単価";
            Price.MaxInputLength = 8;
            Price.MinimumWidth = 8;
            Price.Name = "Price";
            Price.ReadOnly = true;
            Price.Width = 150;
            // 
            // ProductStock
            // 
            ProductStock.DataPropertyName = "ProductStock";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            ProductStock.DefaultCellStyle = dataGridViewCellStyle3;
            ProductStock.FillWeight = 284.0909F;
            ProductStock.HeaderText = "在庫数";
            ProductStock.MinimumWidth = 8;
            ProductStock.Name = "ProductStock";
            ProductStock.ReadOnly = true;
            ProductStock.Width = 135;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnAdd.Location = new Point(54, 11);
            btnAdd.Margin = new Padding(2, 3, 2, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(190, 68);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "データ追加";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Font = new Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnEdit.Location = new Point(351, 11);
            btnEdit.Margin = new Padding(2, 3, 2, 3);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(190, 68);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "データ編集";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnDelete.Location = new Point(637, 11);
            btnDelete.Margin = new Padding(2, 3, 2, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(190, 68);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "データ削除";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClose
            // 
            btnClose.Font = new Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnClose.Location = new Point(918, 11);
            btnClose.Margin = new Padding(2, 3, 2, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(190, 68);
            btnClose.TabIndex = 3;
            btnClose.Text = "終了";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("MS UI Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(602, 17);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(0, 25);
            label1.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(btnAdd);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1211, 96);
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(dgvProducts);
            panel2.Location = new Point(0, 97);
            panel2.Name = "panel2";
            panel2.Size = new Size(1211, 756);
            panel2.TabIndex = 7;
            // 
            // ProductForm
            // 
            AutoScaleDimensions = new SizeF(130F, 130F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1212, 951);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(2, 3, 2, 3);
            MaximizeBox = false;
            MaximumSize = new Size(1230, 1000);
            MinimumSize = new Size(1230, 1000);
            Name = "ProductForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "商品管理表";
            Load += ProductForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private Button btnClose;
        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private DataGridViewTextBoxColumn ProductId;
        private DataGridViewTextBoxColumn ProductCd;
        private DataGridViewTextBoxColumn ProductNames;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn ProductStock;
    }
}