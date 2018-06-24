using Microsoft.Dexterity.Applications;
using Microsoft.Dexterity.Applications.DynamicsDictionary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GP.TransactionSearch
{
    public partial class SOPTransactionSearch : Form
    {

        SearchFilter searchFilter;

        public SOPTransactionSearch()
        {
            InitializeComponent();
        }

        private void SOPTransactionSearch_Load(object sender, EventArgs e)
        {
            if (!Controller.Instance.Model.IsExternal)
            {
                Controller.Instance.SetConnectionInfo();
            }
            else
            {
                Controller.Instance.LoadConfiguration();
            }

            searchFilter = new SearchFilter();

            if (!string.IsNullOrEmpty(Controller.Instance.Model.AssemblyVersion))
            {
                this.Text = this.Text + " v" + Controller.Instance.Model.AssemblyVersion;
            }

            this.dateStart.Value = DateTime.Today.AddYears(-1);

            PrepDataGrid();

            if (!string.IsNullOrEmpty(Controller.Instance.Model.CustomerIDDefault))
            {
                this.txtMasterID.Text = Controller.Instance.Model.CustomerIDDefault;
                GetTransactions();

                Controller.Instance.Model.CustomerIDDefault = string.Empty;
            }
        }


        private void dateStart_ValueChanged(object sender, EventArgs e)
        {
            searchFilter.StartDate = dateStart.Value;
        }

        private void dateEnd_ValueChanged(object sender, EventArgs e)
        {
            searchFilter.EndDate = dateEnd.Value;
        }

        private void txtDocNumber_Leave(object sender, EventArgs e)
        {
            searchFilter.DocNumber = this.txtDocNumber.Text.Trim();
        }

        private void txtMasterID_Leave(object sender, EventArgs e)
        {
            searchFilter.MasterID = this.txtMasterID.Text.Trim();
        }

        private void txtMasterName_Leave(object sender, EventArgs e)
        {
            searchFilter.MasterName = this.txtMasterName.Text.Trim();
        }

        private void dateStart_Leave(object sender, EventArgs e)
        {
            searchFilter.StartDate = this.dateStart.Value;
        }

        private void dateEnd_Leave(object sender, EventArgs e)
        {
            searchFilter.EndDate = this.dateEnd.Value;
        }
        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetTransactions();
        }


        private void GetSearchValues()
        {
            searchFilter.StartDate = dateStart.Value;
            searchFilter.EndDate = dateEnd.Value;
            searchFilter.MasterID = txtMasterID.Text.Trim();
            searchFilter.MasterName = txtMasterName.Text.Trim();
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


        private void PrepDataGrid()
        {
            DataTable dataTable = DataAccess.SOPTransactionSearch(Convert.ToDateTime("1800-01-01"), Convert.ToDateTime("1800-01-01"), "", "", "", 0, 0);
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

            if (this.dataGrid.Columns["SOPTYPE"] != null)
            {
                this.dataGrid.Columns["SOPTYPE"].Visible = false;
            }
        }


        private void GetTransactions()
        {
            GetSearchValues();

            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            sw1.Start();
            DataTable dataTable = DataAccess.SOPTransactionSearch(searchFilter.StartDate, searchFilter.EndDate, searchFilter.DocNumber, searchFilter.MasterID, searchFilter.MasterName, searchFilter.AmountFrom, searchFilter.AmountTo);
            sw1.Stop();
            long dataTime = sw1.ElapsedMilliseconds;

            sw2.Start();
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

            if (this.dataGrid.Columns["SOPTYPE"] != null)
            {
                this.dataGrid.Columns["SOPTYPE"].Visible = false;
            }

            status1.Text = dataTable.Rows.Count.ToString() + " records";
            sw2.Stop();
            long displayTime = sw2.ElapsedMilliseconds;

            this.status4.Text = "Data: " + dataTime.ToString() + "ms  |  Display: " + displayTime.ToString() + "ms";
        }

        private void txtDocNumber_TextChanged(object sender, EventArgs e)
        {
            if (Controller.Instance.Model.SearchAsYouType)
            {
                GetTransactions();
            }
        }

        private void txtVendorID_TextChanged(object sender, EventArgs e)
        {
            if (Controller.Instance.Model.SearchAsYouType)
            {
                GetTransactions();
            }
        }

        private void txtVendorName_TextChanged(object sender, EventArgs e)
        {
            if (Controller.Instance.Model.SearchAsYouType)
            {
                GetTransactions();
            }
        }

        private void dataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //Display right click context menu
                if (e.Button == MouseButtons.Right)
                {
                    cmsRMTransaction.Show(dataGrid, new Point(e.X, e.Y));
                }
            }
            catch (Exception ex)
            {
                String innerMessage = (ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : "";
                MessageBox.Show("An unexpected error occurred in Data Grid Mouse Click: " + ex.Message + innerMessage + Environment.NewLine + ex.StackTrace);
            }
        }

        private void tsmViewMaster_Click(object sender, EventArgs e)
        {
            //If View Master was clicked
            try
            {
                if (dataGrid.Rows.Count > 0)
                {
                    string masterID = dataGrid.Rows[dataGrid.SelectedRows[0].Index].Cells["CustomerID"].Value.ToString();

                    if (!string.IsNullOrEmpty(masterID))
                    {
                        OpenRMCustomerInquiry(masterID);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred reading the master ID: " + ex.Message, "Error", MessageBoxButtons.OK);
            }

        }


        private void OpenRMCustomerInquiry(string customerID)
        {
            Controller.Instance.Model.RMSearchFocus = true;

            RmCustomerInquiryForm rmCustomerInquiryForm = Dynamics.Forms.RmCustomerInquiry;
            RmCustomerInquiryForm.RmCustomerInquiryWindow rmCustomerInquiryWindow;

            rmCustomerInquiryWindow = rmCustomerInquiryForm.RmCustomerInquiry;
            rmCustomerInquiryWindow.Open();
            rmCustomerInquiryWindow.CustomerNumber.Value = customerID;
            rmCustomerInquiryWindow.CustomerNumber.RunValidate();
        }
        

        private void tsmViewTransaction_Click(object sender, EventArgs e)
        {
            ViewTransaction();
        }


        private void ViewTransaction()
        {
            //If View Transaction was clicked or row was double clicked
            try
            {
                if (dataGrid.Rows.Count > 0)
                {
                    string docNumber = dataGrid.Rows[dataGrid.SelectedRows[0].Index].Cells["DocNum"].Value.ToString();
                    short docType = Convert.ToInt16(dataGrid.Rows[dataGrid.SelectedRows[0].Index].Cells["SOPTYPE"].Value);
                    string masterID = dataGrid.Rows[dataGrid.SelectedRows[0].Index].Cells["CustomerID"].Value.ToString();
                    string origin = dataGrid.Rows[dataGrid.SelectedRows[0].Index].Cells["Origin"].Value.ToString();

                    short status = 0;
                    if (origin.ToUpper() == "WORK")
                    {
                        status = 1;
                    }
                    else  //HIST
                    {
                        status = 2;
                    }

                    SOPTransaction sopTrx = new SOPTransaction(status, docType, docNumber, masterID);

                    if (!string.IsNullOrEmpty(docNumber) && docType > 0 && !string.IsNullOrEmpty(masterID))
                    {
                        OpenSOPDocumentInquiry(sopTrx);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred in ViewTransaction: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }


        private void OpenSOPDocumentInquiry(SOPTransaction sopTrx)
        {
            if (string.IsNullOrEmpty(sopTrx.CustomerID) || string.IsNullOrEmpty(sopTrx.DocNum) || sopTrx.Status == 0 || sopTrx.DocType == 0)
            {
                return;
            }

            OpenSOPTransactionInquiry(sopTrx);

        }


        private void OpenSOPTransactionInquiry(SOPTransaction sopTrx)
        {
            Controller.Instance.Model.SOPSearchFocus = true;
            Dynamics.Forms.SopInquiry.Procedures.Open.Invoke(sopTrx.DocType, sopTrx.DocNum, sopTrx.Status, 11, 1, 10806);    //11 = SOP 
        }
        

        private void dataGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                this.dataGrid.CurrentCell = this.dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void CopyDataGridToClipboard()
        {
            this.dataGrid.SelectAll();
            DataObject obj = this.dataGrid.GetClipboardContent();
            Clipboard.SetDataObject(obj, true);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyDataGridToClipboard();
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            CopyDataGridToClipboard();
            string clipboardText = Clipboard.GetText();
            clipboardText = clipboardText.Replace('\t', ',');
            SaveToFile(clipboardText);
        }

        private void SaveToFile(string textToSave)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Save to CSV";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            this.saveFileDialog1.DefaultExt = "csv";
            saveFileDialog1.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Controller.Instance.SaveTextFile(saveFileDialog1.FileName, textToSave);
            }

        }


        private void txtAmountFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAmountFrom.Text))
            {
                string amount = Regex.Replace(txtAmountFrom.Text, @"[^0-9\.]", string.Empty);
                txtAmountFrom.Text = string.Format("{0:#,##0.00}", double.Parse(amount));
                searchFilter.AmountFrom = Convert.ToDecimal(txtAmountFrom.Text);
            }
            else
            {
                searchFilter.AmountFrom = 0m;
            }

            GetTransactions();
        }


        private void txtAmountFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtAmountTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtAmountTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAmountTo.Text))
            {
                string amount = Regex.Replace(txtAmountTo.Text, @"[^0-9\.]", string.Empty);
                txtAmountTo.Text = string.Format("{0:#,##0.00}", double.Parse(amount));
                searchFilter.AmountTo = Convert.ToDecimal(txtAmountTo.Text);
            }
            else
            {
                searchFilter.AmountTo = 999999999999.99m;
            }

            GetTransactions();
        }

        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewTransaction();
        }
    }
}
