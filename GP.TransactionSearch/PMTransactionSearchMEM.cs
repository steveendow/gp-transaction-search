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
    public partial class PMTransactionSearchMEM : Form
    {
        public PMTransactionSearchMEM()
        {
            InitializeComponent();
        }

        private void PMTransactionSearchMEM_Load(object sender, EventArgs e)
        {
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

            if (!string.IsNullOrEmpty(Controller.Instance.Model.PMVendorLabel))
            {
                this.lblVendorID.Text = Controller.Instance.Model.PMVendorLabel + " ID:";
                this.lblVendorName.Text = Controller.Instance.Model.PMVendorLabel + " Name:";
                this.tsmViewMaster.Text = "View " + Controller.Instance.Model.PMVendorLabel;
            }

            this.dateStart.Value = DateTime.Today.AddYears(-1);

            PopulateMEMFacilities();

            PrepDataGrid();

            if (!string.IsNullOrEmpty(Controller.Instance.Model.VendorIDDefault))
            {
                this.txtVendorID.Text = Controller.Instance.Model.VendorIDDefault;
                GetPMTransactions();

                Controller.Instance.Model.VendorIDDefault = string.Empty;
            }
        }

        private void PopulateMEMFacilities () {

            DataTable dataTable = DataAccess.MEMGetUserFacilityIDs(Controller.Instance.Model.MEMFacilityIDSegment);

            if (dataTable.Rows.Count > 0) {

                foreach (DataRow row in dataTable.Rows) {
                    listMEMFacilities.Items.Add(row["FacilityID"].ToString().Trim(), true);
                }
                
            }

        }

        private void dateStart_ValueChanged(object sender, EventArgs e)
        {
            Controller.Instance.PMSearchFilter.StartDate = dateStart.Value;
        }

        private void dateEnd_ValueChanged(object sender, EventArgs e)
        {
            Controller.Instance.PMSearchFilter.EndDate = dateEnd.Value;
        }

        private void txtDocNumber_Leave(object sender, EventArgs e)
        {
            Controller.Instance.PMSearchFilter.DocNumber = this.txtDocNumber.Text.Trim();
        }

        private void txtVendorID_Leave(object sender, EventArgs e)
        {
            Controller.Instance.PMSearchFilter.VendorID = this.txtVendorID.Text.Trim();
        }

        private void txtVendorName_Leave(object sender, EventArgs e)
        {
            Controller.Instance.PMSearchFilter.VendorName = this.txtVendorName.Text.Trim();
        }

        private void dateStart_Leave(object sender, EventArgs e)
        {
            Controller.Instance.PMSearchFilter.StartDate = this.dateStart.Value;
        }

        private void dateEnd_Leave(object sender, EventArgs e)
        {
            Controller.Instance.PMSearchFilter.EndDate = this.dateEnd.Value;
        }
        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetPMTransactions();
        }


        private void GetPMSearchValues()
        {
            //Get MEM Selected Entities
            Controller.Instance.PMSearchFilter.SelectedEntities = GetSelectedEntitiesList();

            Controller.Instance.PMSearchFilter.StartDate = dateStart.Value;
            Controller.Instance.PMSearchFilter.EndDate = dateEnd.Value;
            Controller.Instance.PMSearchFilter.VendorID = txtVendorID.Text.Trim();
            Controller.Instance.PMSearchFilter.VendorName = txtVendorName.Text.Trim();
            Controller.Instance.PMSearchFilter.DocNumber = txtDocNumber.Text.Trim();
            if (!string.IsNullOrEmpty(txtAmountFrom.Text))
            {
                Controller.Instance.PMSearchFilter.AmountFrom = Convert.ToDecimal(txtAmountFrom.Text);
            }
            else
            {
                Controller.Instance.PMSearchFilter.AmountFrom = 0m;
            }

            if (!string.IsNullOrEmpty(txtAmountTo.Text))
            {
                Controller.Instance.PMSearchFilter.AmountTo = Convert.ToDecimal(txtAmountTo.Text);
            }
            else
            {
                Controller.Instance.PMSearchFilter.AmountTo = 999999999999.99m;
            }

        }


        private void PopulateMEMEntities () 
        {

        }

        private void PrepDataGrid()
        {
            DataTable dataTable = DataAccess.PMTransactionSearchMEM(Convert.ToDateTime("1800-01-01"), Convert.ToDateTime("1800-01-01"), "", "", "", 0, 0, "");
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


        private void GetPMTransactions()
        {
            
            GetPMSearchValues();

            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            sw1.Start();
            DataTable dataTable = DataAccess.PMTransactionSearchMEM(Controller.Instance.PMSearchFilter.StartDate, Controller.Instance.PMSearchFilter.EndDate, Controller.Instance.PMSearchFilter.DocNumber, Controller.Instance.PMSearchFilter.VendorID, Controller.Instance.PMSearchFilter.VendorName, Controller.Instance.PMSearchFilter.AmountFrom, Controller.Instance.PMSearchFilter.AmountTo, Controller.Instance.PMSearchFilter.SelectedEntities);
            sw1.Stop();
            long dataTime = sw1.ElapsedMilliseconds;

            sw2.Start();
            this.dataGrid.DataSource = dataTable;

            //StringBuilder types = new StringBuilder();

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


        private string GetSelectedEntitiesList() {

            string selectedEntities = string.Empty;
            
            CheckedListBox.CheckedItemCollection items = listMEMFacilities.CheckedItems;
            int checkedCount = listMEMFacilities.CheckedItems.Count;

            for (int loop = 0; loop < checkedCount; loop++) {
                //selectedEntities += "'" + listMEMFacilities.CheckedItems[loop].ToString() + "'";
                selectedEntities += listMEMFacilities.CheckedItems[loop].ToString();
                if (loop < checkedCount - 1) {
                    selectedEntities += ",";
                }
            }

            return selectedEntities;

        }

        private void txtDocNumber_TextChanged(object sender, EventArgs e)
        {
            if (Controller.Instance.Model.SearchAsYouType)
            {
                GetPMTransactions();
            }
        }

        private void txtVendorID_TextChanged(object sender, EventArgs e)
        {
            if (Controller.Instance.Model.SearchAsYouType)
            {
                GetPMTransactions();
            }
        }

        private void txtVendorName_TextChanged(object sender, EventArgs e)
        {
            if (Controller.Instance.Model.SearchAsYouType)
            {
                GetPMTransactions();
            }
        }

        private void dataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //Display right click context menu
                if (e.Button == MouseButtons.Right)
                {
                    cmsPMTransaction.Show(dataGrid, new Point(e.X, e.Y));
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
            //If View Vendor was clicked
            try
            {
                string fieldName = Controller.Instance.Model.PMVendorLabel + "ID";

                if (dataGrid.Rows.Count > 0)
                {
                    string masterID = dataGrid.Rows[dataGrid.SelectedRows[0].Index].Cells[fieldName].Value.ToString();

                    if (!string.IsNullOrEmpty(masterID))
                    {
                        //Set flag to return focus to Search window after GP inquiry window is closed
                        Controller.Instance.Model.PMSearchFocus = true;
                        OpenPMVendorInquiry(masterID);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred reading the master ID: " + ex.Message, "Error", MessageBoxButtons.OK);
            }

        }


        private void OpenPMVendorInquiry(string vendorID)
        {
            PmVendorInquiryForm pmVendorInquiryForm = Dynamics.Forms.PmVendorInquiry;
            PmVendorInquiryForm.PmVendorInquiryWindow pmVendorInquiryWindow;

            pmVendorInquiryWindow = pmVendorInquiryForm.PmVendorInquiry;
            pmVendorInquiryWindow.Open();
            pmVendorInquiryWindow.VendorId.Value = vendorID;
            pmVendorInquiryWindow.VendorId.RunValidate();
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
                    string fieldName = Controller.Instance.Model.PMVendorLabel + "ID";
                    string trxNumber = dataGrid.Rows[dataGrid.SelectedRows[0].Index].Cells["TrxNumber"].Value.ToString();
                    string vendorID = dataGrid.Rows[dataGrid.SelectedRows[0].Index].Cells[fieldName].Value.ToString();

                    if (!string.IsNullOrEmpty(trxNumber) && !string.IsNullOrEmpty(vendorID))
                    {
                        PMTransaction pmTrx = Controller.Instance.GetPMKeysInfo(trxNumber, vendorID);
                        //OpenPMTransactionInquiry(pmTrx);
                        OpenPMDocumentInquiry(pmTrx);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred reading the master ID: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }



        private void tsmViewApply_Click(object sender, EventArgs e)
        {
            ViewApply();
        }


        private void ViewApply()
        {
            //If View Apply was clicked 
            try
            {
                if (dataGrid.Rows.Count > 0)
                {
                    string fieldName = Controller.Instance.Model.PMVendorLabel + "ID";
                    string trxNumber = dataGrid.Rows[dataGrid.SelectedRows[0].Index].Cells["TrxNumber"].Value.ToString();
                    string vendorID = dataGrid.Rows[dataGrid.SelectedRows[0].Index].Cells[fieldName].Value.ToString();

                    if (!string.IsNullOrEmpty(trxNumber) && !string.IsNullOrEmpty(vendorID))
                    {
                        PMTransaction pmTrx = Controller.Instance.GetPMKeysInfo(trxNumber, vendorID);

                        PMVoucher pmVoucher = Controller.Instance.GetPMVoucher(pmTrx);

                        OpenPMApplyInquiry(pmVoucher);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred reading the master ID: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }



        private void OpenPMDocumentInquiry(PMTransaction pmTrx)
        {
            if (string.IsNullOrEmpty(pmTrx.VENDORID) || string.IsNullOrEmpty(pmTrx.DOCNUMBR))
            {
                return;
            }

            if (pmTrx.DOCTYPE == 6)
            {
                OpenPMPaymentInquiry(pmTrx);
            }
            else
            {
                
                if (pmTrx.BCHSOURC == "PM_Trxent")
                {
                    OpenPMTransactionInquiry(pmTrx);
                }
                else if (pmTrx.BCHSOURC == "Rcvg Trx Ivc")
                {
                    POPTransaction popTrx = Controller.Instance.GetPOPTransaction(pmTrx.VENDORID, pmTrx.CNTRLNUM, pmTrx.DOCNUMBR);
                    if (popTrx != null)
                    {
                        OpenPOPInvoiceInquiry(popTrx);
                    }
                    else
                    {
                        MessageBox.Show("Unable to retrieve POP transaction for vendor " + pmTrx.VENDORID + " voucher " + pmTrx.CNTRLNUM);
                    }
                }
            }
        }


        private void OpenPMTransactionInquiry(PMTransaction pmTrx)
        {
            if (string.IsNullOrEmpty(pmTrx.VENDORID) || string.IsNullOrEmpty(pmTrx.DOCNUMBR))
            {
                return;
            }

            if (pmTrx.DOCTYPE < 1 || pmTrx.DOCTYPE > 5)
            {
                return;
            }
            else
            {
                //Set flag to return focus to Search window after GP inquiry window is closed
                Controller.Instance.Model.PMSearchFocus = true;
                Dynamics.Forms.PmTransactionEntryZoom.Procedures.OpenWindow.Invoke(pmTrx.DOCTYPE, pmTrx.CNTRLNUM, pmTrx.DCSTATUS, 1, 7814);
            }

        }


        private void OpenPOPInvoiceInquiry(POPTransaction popTrx)
        {
            if (string.IsNullOrEmpty(popTrx.VENDORID) || string.IsNullOrEmpty(popTrx.POPRCTNM))
            {
                return;
            }
            else
            {

                //Set flag to return focus to Search window after GP inquiry window is closed
                Controller.Instance.Model.PMSearchFocus = true;
                //OpenWindow is available under FUNCTIONS, not Procedures, with this particular Purchasing window
                Dynamics.Forms.PopInquiryInvoiceEntry.Functions.OpenWindow.Invoke(popTrx.POPRCTNM, 2, 3, 1);
            }
        }


        private void OpenPMPaymentInquiry(PMTransaction pmTrx)
        {
            if (string.IsNullOrEmpty(pmTrx.VENDORID) || string.IsNullOrEmpty(pmTrx.DOCNUMBR))
            {
                return;
            }

            if (!(pmTrx.DOCTYPE == 6))
            {
                return;
            }
            else
            {
                //Set flag to return focus to Search window after GP inquiry window is closed
                Controller.Instance.Model.PMSearchFocus = true;
                Dynamics.Forms.PmManualPaymentsZoom.Procedures.OpenWindow.Invoke(pmTrx.CNTRLNUM, pmTrx.DCSTATUS, 1, 7814);

            }

        }


        private void OpenPMApplyInquiry(PMVoucher pmVoucher)
        {
            if (string.IsNullOrEmpty(pmVoucher.VENDORID) || string.IsNullOrEmpty(pmVoucher.DOCNUMBR))
            {
                return;
            }

            if (pmVoucher.DOCTYPE == 1)
            {
                //Set flag to return focus to Search window after GP inquiry window is closed
                Controller.Instance.Model.PMSearchFocus = true;
                Dynamics.Forms.PmApplyZoom.Procedures.OpenWindow.Invoke(pmVoucher.VCHRNMBR, pmVoucher.DOCNUMBR, pmVoucher.DOCTYPE, pmVoucher.DOCAMNT, pmVoucher.VENDORID, pmVoucher.CURTRXAM, pmVoucher.CURNCYID, "", "", 0.0m, 0, 0.0m, 0, "Transaction Entry", 7817);
            }
            else if (pmVoucher.DOCTYPE == 6)
            {
                //Set flag to return focus to Search window after GP inquiry window is closed
                Controller.Instance.Model.PMSearchFocus = true;
                Dynamics.Forms.PmApplyZoom.Procedures.OpenWindow.Invoke(pmVoucher.VCHRNMBR, pmVoucher.DOCNUMBR, pmVoucher.DOCTYPE, pmVoucher.DOCAMNT, pmVoucher.VENDORID, pmVoucher.CURTRXAM, pmVoucher.CURNCYID, "", "", 0.0m, 0, 0.0m, 0, "Payment Entry", 7818);
            }

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
                Controller.Instance.PMSearchFilter.AmountFrom = Convert.ToDecimal(txtAmountFrom.Text);
            }
            else
            {
                Controller.Instance.PMSearchFilter.AmountFrom = 0m;
            }

            GetPMTransactions();
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
                Controller.Instance.PMSearchFilter.AmountTo = Convert.ToDecimal(txtAmountTo.Text);
            }
            else
            {
                Controller.Instance.PMSearchFilter.AmountTo = 999999999999.99m;
            }

            GetPMTransactions();
        }
                
        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewTransaction();
        }

        private void cmsPMTransaction_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
