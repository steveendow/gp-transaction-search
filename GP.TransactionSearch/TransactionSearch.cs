using Microsoft.Dexterity.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GP.TransactionSearch
{
    public partial class TransactionSearch : Form
    {

        SearchFilter searchFilter;

        public TransactionSearch()
        {
            InitializeComponent();
        }

        private void TransactionSearch_Load(object sender, EventArgs e)
        {
            searchFilter = new SearchFilter();

            if (!Controller.Instance.Model.IsExternal)
            {
                Controller.Instance.SetConnectionInfo();
            }
            else
            {
                Controller.Instance.LoadConfiguration();
            }

            if (!string.IsNullOrEmpty(Controller.Instance.Model.AssemblyVersion))
            {
                this.Text = this.Text + " v" + Controller.Instance.Model.AssemblyVersion;
            }

            if (Controller.Instance.Model.DefaultDatesFromUserDate)
            {
                this.dateEnd.Value = Dynamics.Globals.UserDate;
                this.dateStart.Value = this.dateEnd.Value.AddYears(-1);
            }
            else
            {
                this.dateStart.Value = DateTime.Today.AddYears(-1);
            }

            PrepDataGrid();

            if (!string.IsNullOrEmpty(Controller.Instance.Model.VendorIDDefault))
            {
                this.txtVendorID.Text = Controller.Instance.Model.VendorIDDefault;
                GetTransactions();

                Controller.Instance.Model.VendorIDDefault = string.Empty;
            }

        }


        private void PrepDataGrid()
        {
            DataTable dataTable = DataAccess.PMTransactionSearch(Convert.ToDateTime("1800-01-01"), Convert.ToDateTime("1800-01-01"), "", "", "", 0, 0);
            this.dataGrid.DataSource = dataTable;

            foreach (DataGridViewColumn col in this.dataGrid.Columns)
            {
                switch (col.ValueType.Name)
                {
                    case "String":
                        break;
                    case "DateTime":
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "Decimal":
                        {
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            col.DefaultCellStyle.Format = "c";
                            break;
                        }
                }
            }

        }


        private void GetSearchValues()
        {
            searchFilter.StartDate = dateStart.Value;
            searchFilter.EndDate = dateEnd.Value;
            searchFilter.MasterID = txtVendorID.Text.Trim();
            searchFilter.MasterName = txtVendorName.Text.Trim();
            searchFilter.DocNumber = txtDocNumber.Text.Trim();

            if (!string.IsNullOrEmpty(txtAmountFrom.Text))
            {
                searchFilter.AmountFrom = Convert.ToDecimal(txtAmountFrom.Text);
            }
            else
            {
                searchFilter.AmountFrom = 0m;
            }

            if (!string.IsNullOrEmpty(txtAmountTo.Text))
            {
                searchFilter.AmountTo = Convert.ToDecimal(txtAmountTo.Text);
            }
            else
            {
                searchFilter.AmountTo = 999999999999.99m;
            }

        }


        private void GetTransactions()
        {
            GetSearchValues();

            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            sw1.Start();
            DataTable dataTable = DataAccess.PMTransactionSearch(searchFilter.StartDate, searchFilter.EndDate, searchFilter.DocNumber, searchFilter.MasterID, searchFilter.MasterName, searchFilter.AmountFrom, searchFilter.AmountTo);
            sw1.Stop();
            long dataTime = sw1.ElapsedMilliseconds;

            sw2.Start();
            this.dataGrid.DataSource = dataTable;

            foreach (DataGridViewColumn col in this.dataGrid.Columns)
            {
                // types.AppendLine(col.ValueType.Name);
                switch (col.ValueType.Name)
                {
                    case "String":
                        break;
                    case "DateTime":
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "Decimal":
                        {
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            col.DefaultCellStyle.Format = "c";
                            break;
                        }
                }
            }

            status1.Text = dataTable.Rows.Count.ToString() + " records";
            sw2.Stop();
            long displayTime = sw2.ElapsedMilliseconds;

            this.status4.Text = "Data: " + dataTime.ToString() + "ms  |  Display: " + displayTime.ToString() + "ms";
        }



    }
}
