using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Dexterity.Bridge;
using Microsoft.Dexterity.Applications;
using Microsoft.Dexterity.Applications.DynamicsDictionary;
using System.Windows.Forms;
using System.ComponentModel;

namespace GP.TransactionSearch
{
    public class GPAddIn : IDexterityAddIn
    {
        // IDexterityAddIn interface

        public static PmTransactionInquiryForm pmTrxInquiryForm = Dynamics.Forms.PmTransactionInquiry;
        public static PmTransactionInquiryDocumentForm pmTrxInquiryDocForm = Dynamics.Forms.PmTransactionInquiryDocument;
        public static PmVendorMaintenanceForm pmVendorMaintForm = Dynamics.Forms.PmVendorMaintenance;

        public static PmVendorInquiryForm pmVendorInquiryForm = Dynamics.Forms.PmVendorInquiry;
        public static PmTransactionEntryZoomForm pmTrxEntryZoom = Dynamics.Forms.PmTransactionEntryZoom;
        public static PmManualPaymentsZoomForm pmPaymentsZoom = Dynamics.Forms.PmManualPaymentsZoom;

        private static PMTransactionSearch pmSearch = null;

        public void Initialize()
        {
            bool success = Controller.Instance.LoadConfiguration();
            if (!success)
            {
                MessageBox.Show("Failed to load GP Transaction Search configuration", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            pmTrxInquiryForm.AddMenuHandler(OpenPmTransactionSearch, "Open PM Transaction Search", "P");
            pmTrxInquiryDocForm.AddMenuHandler(OpenPmTransactionSearch, "Open PM Transaction Search", "P");
            pmVendorMaintForm.AddMenuHandler(OpenPmTransactionSearch, "Open PM Transaction Search", "P");

            //PmTransactionInquiryForm.PmTransactionInquiryWindow pmTrxInquiryWindow = pmTrxInquiryForm.PmTransactionInquiry;
            
            pmTrxInquiryForm.OpenBeforeOriginal += new System.ComponentModel.CancelEventHandler(ReplacePMInquiryVendor);
            pmTrxInquiryDocForm.OpenBeforeOriginal += new System.ComponentModel.CancelEventHandler(ReplacePMInquiryDocument);

            //Return focus to PM Search window after the transaction zoom window is closed
            pmTrxEntryZoom.CloseAfterOriginal += new EventHandler(SetPMSearchFocus);
            pmPaymentsZoom.CloseAfterOriginal += new EventHandler(SetPMSearchFocus);
            pmVendorInquiryForm.CloseAfterOriginal += new EventHandler(SetPMSearchFocus);

        }

        //6/6/2018: S. Endow: Modify method to use a CancelEventArgs parameter instead of EventArgs, as it is a CancelEventHandler
        private void ReplacePMInquiryVendor(object sender, CancelEventArgs e)
        {
            if (Controller.Instance.Model.ReplacePMInquiryVendor)
            {
                OpenPMSearch();
                //Use e.Cancel to cancel / suppress the GP form open
                e.Cancel = true;
            }
        }

        //6/6/2018: S. Endow: Modify method to use a CancelEventArgs parameter instead of EventArgs, as it is a CancelEventHandler
        //This fixes the Dex error when the PM Inquiry Document window was replaced
        private void ReplacePMInquiryDocument(object sender, CancelEventArgs e)
        {
            if (Controller.Instance.Model.ReplacePMInquiryDocument)
            {
                OpenPMSearch();
                //Use e.Cancel to cancel / suppress the GP form open
                e.Cancel = true;
            }
        }

        private void OpenPmTransactionSearch(object sender, EventArgs e)
        {
            OpenPMSearch();
        }

        
        private void btnPMSearch_Click(object sender, EventArgs e)
        {
            OpenPMSearch();
        }

        private void OpenPMSearch()
        {
            if (pmSearch == null)
            {
                pmSearch = new PMTransactionSearch();
                pmSearch.FormClosed += delegate { pmSearch = null; };
                pmSearch.Show();
            }
            pmSearch.Focus();
        }

        private void SetPMSearchFocus(object sender, EventArgs e)
        {
            if (Controller.Instance.Model.PMSearchFocus)
            {
                Application.OpenForms[pmSearch.Name].Focus();
                Controller.Instance.Model.PMSearchFocus = false;
            }
        }

    }
}
