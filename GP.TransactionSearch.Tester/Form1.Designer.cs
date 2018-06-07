namespace GP.TransactionSearch.Tester
{
    partial class Form1
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
            this.btnOpenSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenSearch
            // 
            this.btnOpenSearch.FlatAppearance.BorderSize = 0;
            this.btnOpenSearch.Location = new System.Drawing.Point(24, 36);
            this.btnOpenSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenSearch.Name = "btnOpenSearch";
            this.btnOpenSearch.Size = new System.Drawing.Size(186, 23);
            this.btnOpenSearch.TabIndex = 0;
            this.btnOpenSearch.Text = "Open PM Transaction Search";
            this.btnOpenSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenSearch.UseVisualStyleBackColor = true;
            this.btnOpenSearch.Click += new System.EventHandler(this.btnOpenSearch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnOpenSearch);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenSearch;
    }
}

