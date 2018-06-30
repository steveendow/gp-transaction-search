using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GP.TransactionSearch;

namespace GP.TransactionSearch.Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnOpenSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Enter a password", "Enter a Password", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Controller.Instance.Model.IsExternal = true;
            Controller.Instance.SetConnectionInfo(txtServer.Text.Trim(), txtUser.Text.Trim(), txtPassword.Text.Trim(), txtSystemDB.Text.Trim(), txtCompanyDB.Text.Trim());

            PMTransactionSearch search = new PMTransactionSearch();
            search.Show();
        }

        private void btnOpenRMSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Enter a password", "Enter a Password", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Controller.Instance.Model.IsExternal = true;
            Controller.Instance.SetConnectionInfo(txtServer.Text.Trim(), txtUser.Text.Trim(), txtPassword.Text.Trim(), txtSystemDB.Text.Trim(), txtCompanyDB.Text.Trim());

            RMTransactionSearch search = new RMTransactionSearch();
            search.Show();
        }

        private void btnOpenSOPSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Enter a password", "Enter a Password", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Controller.Instance.Model.IsExternal = true;
            Controller.Instance.SetConnectionInfo(txtServer.Text.Trim(), txtUser.Text.Trim(), txtPassword.Text.Trim(), txtSystemDB.Text.Trim(), txtCompanyDB.Text.Trim());

            SOPTransactionSearch search = new SOPTransactionSearch();
            search.Show();
        }
    }

}
