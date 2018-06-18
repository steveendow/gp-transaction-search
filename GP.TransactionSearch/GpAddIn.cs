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

        public static PmVendorMaintenanceForm.PmVendorMaintenanceWindow pmVendorMaintWindow = pmVendorMaintForm.PmVendorMaintenance;

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
            pmVendorMaintForm.AddMenuHandler(OpenPmTransactionSearchVendor, "Open PM Transaction Search", "P");

            //PmTransactionInquiryForm.PmTransactionInquiryWindow pmTrxInquiryWindow = pmTrxInquiryForm.PmTransactionInquiry;
            
            pmTrxInquiryForm.OpenBeforeOriginal += new System.ComponentModel.CancelEventHandler(ReplacePMInquiryVendor);
            //6/7/2018: S. Endow: The Inquiry Document window does not behave well when cancelled or closed by the OpenBeforeOriginal window
            pmTrxInquiryDocForm.OpenBeforeOriginal += new System.ComponentModel.CancelEventHandler(ReplacePMInquiryDocument);
            pmTrxInquiryDocForm.OpenAfterOriginal += new  EventHandler(ClosePMTrxInquiryDoc);

            //Return focus to PM Search window after the transaction zoom window is closed
            pmTrxEntryZoom.CloseAfterOriginal += new EventHandler(SetPMSearchFocus);
            pmPaymentsZoom.CloseAfterOriginal += new EventHandler(SetPMSearchFocus);
            pmVendorInquiryForm.CloseAfterOriginal += new EventHandler(SetPMSearchFocus);
            
        }


        //6/7/2018: S. Endow: The Inquiry Document window behaves differently than the Inquiry Vendor window
        //Form.Close() in OpenBeforeOriginal causes a Dex error. Using e.Cancel avoids the error, but prevents the Transaction Search window
        //from being opened again.  For now, use OpenAfterOriginal to close the Inquiry Document window, then open PM Search.
        private void ReplacePMInquiryDocument(object sender, CancelEventArgs e)
        {
            if (Controller.Instance.Model.ReplacePMInquiryDocument)
            {
                //Use e.Cancel to cancel / suppress the GP form open
                //e.Cancel = true;
                //pmTrxInquiryDocForm.Close();

                OpenPMSearch();
                
                SetPMTransactionSearchFocus();
            }
        }

        private void ClosePMTrxInquiryDoc(object sender, EventArgs e)
        {
            if (Controller.Instance.Model.ReplacePMInquiryDocument)
            {
                pmTrxInquiryDocForm.Close();
                //OpenPMSearch();
                SetPMTransactionSearchFocus();
            }
        }

        //6/6/2018: S. Endow: Modify method to use a CancelEventArgs parameter instead of EventArgs, as it is a CancelEventHandler
        private void ReplacePMInquiryVendor(object sender, CancelEventArgs e)
        {
            //6/7/2018: S. Endow: It seems that the PM Trx Inquiry Vendor behaves differently than the Document inquiry window
            //Using e.Cancel with the Vendor window throws a Dex error. Use .Close() instead
            if (Controller.Instance.Model.ReplacePMInquiryVendor)
            {
                OpenPMSearch();
                //Use e.Cancel to cancel / suppress the GP form open
                //e.Cancel = true;
                pmTrxInquiryForm.Close();

            }
        }

        

        private void OpenPmTransactionSearch(object sender, EventArgs e)
        {
            OpenPMSearch();
        }

        private void OpenPmTransactionSearchVendor(object sender, EventArgs e)
        {
            Controller.Instance.Model.VendorIDDefault = pmVendorMaintWindow.VendorId.Value.Trim();
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
            Application.OpenForms[pmSearch.Name].Focus();
        }

        private void SetPMSearchFocus(object sender, EventArgs e)
        {
            if (Controller.Instance.Model.PMSearchFocus)
            {
                SetPMTransactionSearchFocus();
            }
        }


        private void SetPMTransactionSearchFocus()
        {
            Application.OpenForms[pmSearch.Name].Focus();
            Controller.Instance.Model.PMSearchFocus = false;
        }

    }
}
