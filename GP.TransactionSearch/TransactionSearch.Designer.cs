namespace GP.TransactionSearch
{
    partial class TransactionSearch
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.txtAmountTo = new System.Windows.Forms.TextBox();
            this.btnCSV = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAmountFrom = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDocNumber = new System.Windows.Forms.TextBox();
            this.lblMasterName = new System.Windows.Forms.Label();
            this.txtVendorName = new System.Windows.Forms.TextBox();
            this.lblMasterID = new System.Windows.Forms.Label();
            this.txtVendorID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status1,
            this.status2,
            this.status3,
            this.status4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 413);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1042, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
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
            this.status2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status3
            // 
            this.status3.AutoSize = false;
            this.status3.Name = "status3";
            this.status3.Size = new System.Drawing.Size(250, 17);
            this.status3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status4
            // 
            this.status4.AutoSize = false;
            this.status4.Name = "status4";
            this.status4.Size = new System.Drawing.Size(320, 17);
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(0, 60);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(1042, 350);
            this.dataGrid.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "To Date:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "From Date:";
            // 
            // dateEnd
            // 
            this.dateEnd.CustomFormat = "";
            this.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEnd.Location = new System.Drawing.Point(71, 34);
            this.dateEnd.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(104, 20);
            this.dateEnd.TabIndex = 5;
            // 
            // dateStart
            // 
            this.dateStart.CustomFormat = "";
            this.dateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateStart.Location = new System.Drawing.Point(71, 8);
            this.dateStart.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(104, 20);
            this.dateStart.TabIndex = 4;
            // 
            // txtAmountTo
            // 
            this.txtAmountTo.Location = new System.Drawing.Point(587, 33);
            this.txtAmountTo.Name = "txtAmountTo";
            this.txtAmountTo.ShortcutsEnabled = false;
            this.txtAmountTo.Size = new System.Drawing.Size(89, 20);
            this.txtAmountTo.TabIndex = 20;
            // 
            // btnCSV
            // 
            this.btnCSV.Location = new System.Drawing.Point(955, 31);
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.Size = new System.Drawing.Size(75, 23);
            this.btnCSV.TabIndex = 24;
            this.btnCSV.Text = "CSV";
            this.btnCSV.UseVisualStyleBackColor = true;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(874, 31);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 23;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(691, 30);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 21;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(379, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Amount From:";
            // 
            // txtAmountFrom
            // 
            this.txtAmountFrom.Location = new System.Drawing.Point(459, 33);
            this.txtAmountFrom.Name = "txtAmountFrom";
            this.txtAmountFrom.ShortcutsEnabled = false;
            this.txtAmountFrom.Size = new System.Drawing.Size(89, 20);
            this.txtAmountFrom.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(379, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Doc Number:";
            // 
            // txtDocNumber
            // 
            this.txtDocNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDocNumber.Location = new System.Drawing.Point(459, 7);
            this.txtDocNumber.Name = "txtDocNumber";
            this.txtDocNumber.Size = new System.Drawing.Size(89, 20);
            this.txtDocNumber.TabIndex = 17;
            // 
            // lblMasterName
            // 
            this.lblMasterName.AutoSize = true;
            this.lblMasterName.Location = new System.Drawing.Point(190, 38);
            this.lblMasterName.Name = "lblMasterName";
            this.lblMasterName.Size = new System.Drawing.Size(75, 13);
            this.lblMasterName.TabIndex = 22;
            this.lblMasterName.Text = "Vendor Name:";
            // 
            // txtVendorName
            // 
            this.txtVendorName.Location = new System.Drawing.Point(276, 34);
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.Size = new System.Drawing.Size(97, 20);
            this.txtVendorName.TabIndex = 16;
            // 
            // lblMasterID
            // 
            this.lblMasterID.AutoSize = true;
            this.lblMasterID.Location = new System.Drawing.Point(190, 12);
            this.lblMasterID.Name = "lblMasterID";
            this.lblMasterID.Size = new System.Drawing.Size(56, 13);
            this.lblMasterID.TabIndex = 19;
            this.lblMasterID.Text = "Master ID:";
            // 
            // txtVendorID
            // 
            this.txtVendorID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVendorID.Location = new System.Drawing.Point(276, 8);
            this.txtVendorID.Name = "txtVendorID";
            this.txtVendorID.Size = new System.Drawing.Size(97, 20);
            this.txtVendorID.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(557, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "To:";
            // 
            // TransactionSearch
            // 
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
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDocNumber);
            this.Controls.Add(this.lblMasterName);
            this.Controls.Add(this.txtVendorName);
            this.Controls.Add(this.lblMasterID);
            this.Controls.Add(this.txtVendorID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.dateStart);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.statusStrip1);
            this.Name = "TransactionSearch";
            this.Text = "Transaction Search";
            this.Load += new System.EventHandler(this.TransactionSearch_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status1;
        private System.Windows.Forms.ToolStripStatusLabel status2;
        private System.Windows.Forms.ToolStripStatusLabel status3;
        private System.Windows.Forms.ToolStripStatusLabel status4;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.DateTimePicker dateStart;
        private System.Windows.Forms.TextBox txtAmountTo;
        private System.Windows.Forms.Button btnCSV;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAmountFrom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDocNumber;
        private System.Windows.Forms.Label lblMasterName;
        private System.Windows.Forms.TextBox txtVendorName;
        private System.Windows.Forms.Label lblMasterID;
        private System.Windows.Forms.TextBox txtVendorID;
        private System.Windows.Forms.Label label7;
    }
}