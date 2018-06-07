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

        private void btnOpenSearch_Click(object sender, EventArgs e)
        {
            Controller.Instance.Model.IsExternal = true;
            Controller.Instance.SetConnectionInfo("GP2016", "sa", "spe", "DYNAMICS", "TWO");
            PMTransactionSearch search = new PMTransactionSearch();
            search.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}
