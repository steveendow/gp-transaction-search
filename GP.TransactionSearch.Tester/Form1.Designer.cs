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
            this.btnOpenPMSearch = new System.Windows.Forms.Button();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtSystemDB = new System.Windows.Forms.TextBox();
            this.txtCompanyDB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOpenRMSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenPMSearch
            // 
            this.btnOpenPMSearch.FlatAppearance.BorderSize = 0;
            this.btnOpenPMSearch.Location = new System.Drawing.Point(32, 159);
            this.btnOpenPMSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenPMSearch.Name = "btnOpenPMSearch";
            this.btnOpenPMSearch.Size = new System.Drawing.Size(186, 23);
            this.btnOpenPMSearch.TabIndex = 0;
            this.btnOpenPMSearch.Text = "Open PM Transaction Search";
            this.btnOpenPMSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenPMSearch.UseVisualStyleBackColor = true;
            this.btnOpenPMSearch.Click += new System.EventHandler(this.btnOpenSearch_Click);
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(118, 11);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(100, 20);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "GP2016";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(118, 37);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 20);
            this.txtUser.TabIndex = 2;
            this.txtUser.Text = "sa";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(118, 63);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // txtSystemDB
            // 
            this.txtSystemDB.Location = new System.Drawing.Point(118, 89);
            this.txtSystemDB.Name = "txtSystemDB";
            this.txtSystemDB.Size = new System.Drawing.Size(100, 20);
            this.txtSystemDB.TabIndex = 4;
            this.txtSystemDB.Text = "DYNAMICS";
            // 
            // txtCompanyDB
            // 
            this.txtCompanyDB.Location = new System.Drawing.Point(118, 115);
            this.txtCompanyDB.Name = "txtCompanyDB";
            this.txtCompanyDB.Size = new System.Drawing.Size(100, 20);
            this.txtCompanyDB.TabIndex = 5;
            this.txtCompanyDB.Text = "TWO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "SQL Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Company DB:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "User:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "System DB:";
            // 
            // btnOpenRMSearch
            // 
            this.btnOpenRMSearch.FlatAppearance.BorderSize = 0;
            this.btnOpenRMSearch.Location = new System.Drawing.Point(32, 191);
            this.btnOpenRMSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenRMSearch.Name = "btnOpenRMSearch";
            this.btnOpenRMSearch.Size = new System.Drawing.Size(186, 23);
            this.btnOpenRMSearch.TabIndex = 12;
            this.btnOpenRMSearch.Text = "Open RM Transaction Search";
            this.btnOpenRMSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenRMSearch.UseVisualStyleBackColor = true;
            this.btnOpenRMSearch.Click += new System.EventHandler(this.btnOpenRMSearch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(258, 280);
            this.Controls.Add(this.btnOpenRMSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCompanyDB);
            this.Controls.Add(this.txtSystemDB);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.btnOpenPMSearch);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tester";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenPMSearch;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtSystemDB;
        private System.Windows.Forms.TextBox txtCompanyDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOpenRMSearch;
    }
}

