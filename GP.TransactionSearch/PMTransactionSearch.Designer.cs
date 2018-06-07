namespace GP.TransactionSearch
{
    partial class PMTransactionSearch
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
            this.components = new System.ComponentModel.Container();
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVendorID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVendorName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDocNumber = new System.Windows.Forms.TextBox();
            this.statusPMTransaction = new System.Windows.Forms.StatusStrip();
            this.status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.bsPMTransaction = new System.Windows.Forms.BindingSource(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.txtAmountFrom = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmsPMTransaction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmViewMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmViewTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnCSV = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.txtAmountTo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.statusPMTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPMTransaction)).BeginInit();
            this.cmsPMTransaction.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateStart
            // 
            this.dateStart.CustomFormat = "";
            this.dateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateStart.Location = new System.Drawing.Point(71, 8);
            this.dateStart.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(104, 20);
            this.dateStart.TabIndex = 0;
            this.dateStart.ValueChanged += new System.EventHandler(this.dateStart_ValueChanged);
            this.dateStart.Leave += new System.EventHandler(this.dateStart_Leave);
            // 
            // dateEnd
            // 
            this.dateEnd.CustomFormat = "";
            this.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEnd.Location = new System.Drawing.Point(71, 34);
            this.dateEnd.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(104, 20);
            this.dateEnd.TabIndex = 1;
            this.dateEnd.ValueChanged += new System.EventHandler(this.dateEnd_ValueChanged);
            this.dateEnd.Leave += new System.EventHandler(this.dateEnd_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "From Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date:";
            // 
            // txtVendorID
            // 
            this.txtVendorID.Location = new System.Drawing.Point(268, 8);
            this.txtVendorID.Name = "txtVendorID";
            this.txtVendorID.Size = new System.Drawing.Size(89, 20);
            this.txtVendorID.TabIndex = 2;
            this.txtVendorID.TextChanged += new System.EventHandler(this.txtVendorID_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Vendor ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(190, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Vendor Name:";
            // 
            // txtVendorName
            // 
            this.txtVendorName.Location = new System.Drawing.Point(268, 34);
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.Size = new System.Drawing.Size(89, 20);
            this.txtVendorName.TabIndex = 3;
            this.txtVendorName.TextChanged += new System.EventHandler(this.txtVendorName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(368, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Doc Number:";
            // 
            // txtDocNumber
            // 
            this.txtDocNumber.Location = new System.Drawing.Point(445, 7);
            this.txtDocNumber.Name = "txtDocNumber";
            this.txtDocNumber.Size = new System.Drawing.Size(89, 20);
            this.txtDocNumber.TabIndex = 6;
            this.txtDocNumber.TextChanged += new System.EventHandler(this.txtDocNumber_TextChanged);
            // 
            // statusPMTransaction
            // 
            this.statusPMTransaction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status1,
            this.status2,
            this.status3,
            this.status4});
            this.statusPMTransaction.Location = new System.Drawing.Point(0, 413);
            this.statusPMTransaction.Name = "statusPMTransaction";
            this.statusPMTransaction.Size = new System.Drawing.Size(1042, 22);
            this.statusPMTransaction.TabIndex = 11;
            this.statusPMTransaction.Text = "statusStrip1";
            // 
            // status1
            // 
            this.status1.AutoSize = false;
            this.status1.Name = "status1";
            this.status1.Size = new System.Drawing.Size(200, 17);
            this.status1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status2
            // 
            this.status2.AutoSize = false;
            this.status2.Name = "status2";
            this.status2.Size = new System.Drawing.Size(250, 17);
            // 
            // status3
            // 
            this.status3.AutoSize = false;
            this.status3.Name = "status3";
            this.status3.Size = new System.Drawing.Size(250, 17);
            // 
            // status4
            // 
            this.status4.AutoSize = false;
            this.status4.Name = "status4";
            this.status4.Size = new System.Drawing.Size(320, 17);
            this.status4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToOrderColumns = true;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGrid.Location = new System.Drawing.Point(0, 60);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.ShowEditingIcon = false;
            this.dataGrid.Size = new System.Drawing.Size(1042, 350);
            this.dataGrid.TabIndex = 7;
            this.dataGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellContentDoubleClick);
            this.dataGrid.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_CellMouseDown);
            this.dataGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGrid_MouseClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(368, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Amount From:";
            // 
            // txtAmountFrom
            // 
            this.txtAmountFrom.Location = new System.Drawing.Point(445, 33);
            this.txtAmountFrom.Name = "txtAmountFrom";
            this.txtAmountFrom.ShortcutsEnabled = false;
            this.txtAmountFrom.Size = new System.Drawing.Size(89, 20);
            this.txtAmountFrom.TabIndex = 4;
            this.txtAmountFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmountFrom_KeyPress);
            this.txtAmountFrom.Leave += new System.EventHandler(this.txtAmountFrom_Leave);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(673, 30);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmsPMTransaction
            // 
            this.cmsPMTransaction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmViewMaster,
            this.tsmViewTransaction});
            this.cmsPMTransaction.Name = "cmsPMTransaction";
            this.cmsPMTransaction.Size = new System.Drawing.Size(165, 48);
            // 
            // tsmViewMaster
            // 
            this.tsmViewMaster.Name = "tsmViewMaster";
            this.tsmViewMaster.Size = new System.Drawing.Size(164, 22);
            this.tsmViewMaster.Text = "View Vendor";
            this.tsmViewMaster.Click += new System.EventHandler(this.tsmViewMaster_Click);
            // 
            // tsmViewTransaction
            // 
            this.tsmViewTransaction.Name = "tsmViewTransaction";
            this.tsmViewTransaction.Size = new System.Drawing.Size(164, 22);
            this.tsmViewTransaction.Text = "View Transaction";
            this.tsmViewTransaction.Click += new System.EventHandler(this.tsmViewTransaction_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(874, 31);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnCSV
            // 
            this.btnCSV.Location = new System.Drawing.Point(955, 31);
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.Size = new System.Drawing.Size(75, 23);
            this.btnCSV.TabIndex = 9;
            this.btnCSV.Text = "CSV";
            this.btnCSV.UseVisualStyleBackColor = true;
            this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
            // 
            // txtAmountTo
            // 
            this.txtAmountTo.Location = new System.Drawing.Point(569, 33);
            this.txtAmountTo.Name = "txtAmountTo";
            this.txtAmountTo.ShortcutsEnabled = false;
            this.txtAmountTo.Size = new System.Drawing.Size(89, 20);
            this.txtAmountTo.TabIndex = 5;
            this.txtAmountTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmountTo_KeyPress);
            this.txtAmountTo.Leave += new System.EventHandler(this.txtAmountTo_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(541, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "To:";
            // 
            // PMTransactionSearch
            // 
            this.AcceptButton = this.btnRefresh;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 435);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtAmountTo);
            this.Controls.Add(this.btnCSV);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAmountFrom);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.statusPMTransaction);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDocNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtVendorName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtVendorID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.dateStart);
            this.Name = "PMTransactionSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PM Transaction Search";
            this.Load += new System.EventHandler(this.PMTransactionSearch_Load);
            this.statusPMTransaction.ResumeLayout(false);
            this.statusPMTransaction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPMTransaction)).EndInit();
            this.cmsPMTransaction.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateStart;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVendorID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVendorName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDocNumber;
        private System.Windows.Forms.StatusStrip statusPMTransaction;
        private System.Windows.Forms.ToolStripStatusLabel status1;
        private System.Windows.Forms.BindingSource bsPMTransaction;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAmountFrom;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.ToolStripStatusLabel status2;
        private System.Windows.Forms.ToolStripStatusLabel status3;
        private System.Windows.Forms.ToolStripStatusLabel status4;
        private System.Windows.Forms.ContextMenuStrip cmsPMTransaction;
        private System.Windows.Forms.ToolStripMenuItem tsmViewMaster;
        private System.Windows.Forms.ToolStripMenuItem tsmViewTransaction;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCSV;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox txtAmountTo;
        private System.Windows.Forms.Label label7;
    }
}