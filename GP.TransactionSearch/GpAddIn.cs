using System;
using Microsoft.Dexterity.Bridge;
using Microsoft.Dexterity.Applications;
using Microsoft.Dexterity.Applications.DynamicsDictionary;
using System.Windows.Forms;
using Microsoft.Dexterity.Applications.MenusForVisualStudioToolsDictionary;


namespace GP.TransactionSearch
{
    public class GPAddIn : IDexterityAddIn
    {

        #region VSIT Setup

        // IDexterityAddIn interface

        // Application Name
        const string APPNAME = "GP Transaction Search";

        // Dictionary ID Constants
        const short DYNAMICS = 0;

        // Shortcut Key Modifier Constants
        const int COMMAND_SHORTCUT_CTRL = 65536;
        const int COMMAND_SHORTCUT_CTRLSHIFT = 327680;
        const int COMMAND_SHORTCUT_CTRLALT = 196608;
        const int COMMAND_SHORTCUT_ALT = 131072;
        const int COMMAND_SHORTCUT_ALTSHIFT = 393216;
        const int COMMAND_SHORTCUT_CTRLALTSHIFT = 458752;

        // Shortcut Key Function Key Constants, can be used instead of ASCII value
        const short COMMAND_SHORTCUT_KEY_F1 = 112;
        const short COMMAND_SHORTCUT_KEY_F2 = 113;
        const short COMMAND_SHORTCUT_KEY_F3 = 114;
        const short COMMAND_SHORTCUT_KEY_F4 = 115;
        const short COMMAND_SHORTCUT_KEY_F5 = 116;
        const short COMMAND_SHORTCUT_KEY_F6 = 117;
        const short COMMAND_SHORTCUT_KEY_F7 = 118;
        const short COMMAND_SHORTCUT_KEY_F8 = 119;
        const short COMMAND_SHORTCUT_KEY_F9 = 120;
        const short COMMAND_SHORTCUT_KEY_F10 = 121;
        const short COMMAND_SHORTCUT_KEY_F11 = 122;
        const short COMMAND_SHORTCUT_KEY_F12 = 123;

        #endregion

        short PMTrxSearchMenuTag;
        short RMTrxSearchMenuTag;
        short SOPTrxSearchMenuTag;
        short GLTrxSearchMenuTag;

        public static PmTransactionInquiryForm pmTrxInquiryForm = Dynamics.Forms.PmTransactionInquiry;
        public static PmTransactionInquiryDocumentForm pmTrxInquiryDocForm = Dynamics.Forms.PmTransactionInquiryDocument;
        public static PmVendorMaintenanceForm pmVendorMaintForm = Dynamics.Forms.PmVendorMaintenance;

        public static PmVendorInquiryForm pmVendorInquiryForm = Dynamics.Forms.PmVendorInquiry;
        public static PmTransactionEntryZoomForm pmTrxEntryZoom = Dynamics.Forms.PmTransactionEntryZoom;
        public static PmManualPaymentsZoomForm pmPaymentsZoom = Dynamics.Forms.PmManualPaymentsZoom;

        public static PopInquiryInvoiceEntryForm popInvoiceInquiryForm = Dynamics.Forms.PopInquiryInvoiceEntry;
        
        public static PmApplyZoomForm pmApplyZoomForm = Dynamics.Forms.PmApplyZoom;

        public static PmVendorMaintenanceForm.PmVendorMaintenanceWindow pmVendorMaintWindow = pmVendorMaintForm.PmVendorMaintenance;


        public static RmCustomerInquiryForm rmCustomerInquiryForm = Dynamics.Forms.RmCustomerInquiry;
        public static RmTransactionInquiryForm rmTrxInquiryForm = Dynamics.Forms.RmTransactionInquiry;
        public static RmTransactionInquiryDocumentForm rmTrxInquiryDocForm = Dynamics.Forms.RmTransactionInquiryDocument;
        public static RmCustomerMaintenanceForm rmCustomerMaintForm = Dynamics.Forms.RmCustomerMaintenance;
        public static RmSalesInquiryForm rmSalesInquiryForm = Dynamics.Forms.RmSalesInquiry;        
        public static RmCustomerMaintenanceForm.RmCustomerMaintenanceWindow rmCustomerMaintWindow = rmCustomerMaintForm.RmCustomerMaintenance;

        public static SopInquiryForm sopInquiryForm = Dynamics.Forms.SopInquiry;
        
        private static PMTransactionSearch pmSearch = null;
        private static RMTransactionSearch rmSearch = null;
        private static SOPTransactionSearch sopSearch = null;


        public void Initialize()
        {
            bool success = Controller.Instance.LoadConfiguration();
            if (!success)
            {
                MessageBox.Show("Failed to load GP Transaction Search configuration", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            pmTrxInquiryForm.AddMenuHandler(OpenPMTransactionSearch, "Open PM Transaction Search", "P");
            pmTrxInquiryDocForm.AddMenuHandler(OpenPMTransactionSearch, "Open PM Transaction Search", "P");
            pmVendorMaintForm.AddMenuHandler(OpenPMTransactionSearchVendor, "Open PM Transaction Search", "P");
            
            rmTrxInquiryForm.AddMenuHandler(OpenRMTransactionSearch, "Open RM Transaction Search", "R");
            rmSalesInquiryForm.AddMenuHandler(OpenRMTransactionSearch, "Open RM Transaction Search", "R");
            rmCustomerMaintForm.AddMenuHandler(OpenRMTransactionSearchCustomer, "Open RM Transaction Search", "R");

            sopInquiryForm.AddMenuHandler(OpenSOPTransactionSearch, "Open SOP Transaction Search", "S");


            // Register Event to add menu entries
            MenusForVisualStudioTools.Functions.EventRegister.InvokeAfterOriginal += new EventRegisterFunction.InvokeEventHandler(VSTMCommandFormRegister);
            // Register Event to handle callbacks from menu entries
            MenusForVisualStudioTools.Functions.EventHandler.InvokeAfterOriginal += new EventHandlerFunction.InvokeEventHandler(VSTMCommandFormCallback);

            //Return focus to PM Search window after the drill down / zoom window is closed
            pmTrxEntryZoom.CloseAfterOriginal += new EventHandler(SetPMSearchFocus);
            pmPaymentsZoom.CloseAfterOriginal += new EventHandler(SetPMSearchFocus);
            pmVendorInquiryForm.CloseAfterOriginal += new EventHandler(SetPMSearchFocus);

            popInvoiceInquiryForm.CloseAfterOriginal += new EventHandler(TestWindowClosed);  //(SetPMSearchFocus);
            popInvoiceInquiryForm.OpenAfterOriginal += new EventHandler(TestWindowOpened);

            pmApplyZoomForm.CloseAfterOriginal += new EventHandler(SetPMSearchFocus);

            //Return focus to RM Search window after the drill down / zoom window is closed
            rmCustomerInquiryForm.CloseAfterOriginal += new EventHandler(SetRMSearchFocus);
            rmTrxInquiryForm.CloseAfterOriginal += new EventHandler(SetRMSearchFocus);
            rmTrxInquiryDocForm.CloseAfterOriginal += new EventHandler(SetRMSearchFocus);
            rmSalesInquiryForm.CloseAfterOriginal += new EventHandler(SetRMSearchFocus);

            sopInquiryForm.CloseAfterOriginal += new EventHandler(SetRMSearchFocus);

        }

        private void TestWindowOpened(object sender, EventArgs e)
        {
            MessageBox.Show("popInvoiceInquiryForm opened");
        }

        private void TestWindowClosed(object sender, EventArgs e)
        {
            MessageBox.Show("popInvoiceInquiryForm closed");
        }

        // Script to Register menu entries
        // void VSTMCommandFormRegister(object sender, EventArgs e)
        private void VSTMCommandFormRegister(object sender, EventRegisterFunction.InvokeEventArgs e)
        {
            short ParentTag = 0;
            short BelowTag = 0;
            short ResID = 0;
            short Err = 0;

            try
            {
                #region Add PM Transaction Search Menu
                // Get Parent Tag for command list to add menu to
                ParentTag = MenusForVisualStudioTools.Functions.GetTagByName.Invoke(DYNAMICS, "Command_Purchasing", "CL_Purchasing_Inquiry");           // Dictionary ID, Form Name, Command Name
                if (ParentTag <= 0)
                {
                    throw new Exception("PM Parent GetTagByName, error code: " + Convert.ToString(ParentTag));
                }

                // Get Below Tag for command/command list to add menu below
                BelowTag = MenusForVisualStudioTools.Functions.GetTagByName.Invoke(DYNAMICS, "Command_Purchasing", "PM_Transaction_Inquiry_Document");  // Dictionary ID, Form Name, Command Name
                if (BelowTag <= 0)
                {
                    throw new Exception("PM Below GetTagByName, error code: " + Convert.ToString(BelowTag));
                }

                // Get Security Form Resource ID for menus to inherit security access from
                ResID = MenusForVisualStudioTools.Functions.GetFormResId.Invoke(DYNAMICS, "PM_Transaction_Inquiry");                   // Get Form Resource ID for Security 
                if (ResID <= 0)
                {
                    throw new Exception("PM GetFormResId, error code: " + Convert.ToString(ResID));
                }
                                
                // Add Menu entry using API Function call to create first sub menu entry with security
                PMTrxSearchMenuTag = MenusForVisualStudioTools.Functions.RegisterWithSecurity.Invoke(
                                    ParentTag,                                                      // Parent Command Tag
                                    "PM Transaction Search",                                // Menu Caption
                                    "Open PM Transaction Search",                           // Menu Tooltip
                                    (int)'P', COMMAND_SHORTCUT_CTRLSHIFT,                     // Menu Shortcut Key, Shortcut Modifier
                                    false, false, false,                                     // Checked, Disabled, Hidden
                                    BelowTag,                                               // Add Below Command Tag
                                    false, false,                                            // Add Separator, Add Command List
                                    DYNAMICS, ResID);                                       // Security Dictionary and Form Resource ID
                if (PMTrxSearchMenuTag <= 0)
                {
                    throw new Exception("PM Transaction Search  RegisterWithSecurity, error code: " + Convert.ToString(PMTrxSearchMenuTag));
                }
                #endregion

                
                #region Add SOP Transaction Search Menu
                ParentTag = MenusForVisualStudioTools.Functions.GetTagByName.Invoke(DYNAMICS, "Command_Sales", "CL_Sales_Inquiry");           // Dictionary ID, Form Name, Command Name
                if (ParentTag <= 0)
                {
                    throw new Exception("RM Parent GetTagByName, error code: " + Convert.ToString(ParentTag));
                }

                // Get Below Tag for command/command list to add menu below
                BelowTag = MenusForVisualStudioTools.Functions.GetTagByName.Invoke(DYNAMICS, "Command_Sales", "RM_Transaction_Inquiry_Document");  // Dictionary ID, Form Name, Command Name
                if (BelowTag <= 0)
                {
                    throw new Exception("RM Below GetTagByName, error code: " + Convert.ToString(BelowTag));
                }

                // Get Security Form Resource ID for menus to inherit security access from
                ResID = MenusForVisualStudioTools.Functions.GetFormResId.Invoke(DYNAMICS, "RM_Transaction_Inquiry");                   // Get Form Resource ID for Security 
                if (ResID <= 0)
                {
                    throw new Exception("RM GetFormResId, error code: " + Convert.ToString(ResID));
                }

                // Add Menu entry using API Function call to create first sub menu entry with security
                SOPTrxSearchMenuTag = MenusForVisualStudioTools.Functions.RegisterWithSecurity.Invoke(
                                    ParentTag,                                                      // Parent Command Tag
                                    "SOP Transaction Search",                                // Menu Caption
                                    "Open SOP Transaction Search",                           // Menu Tooltip
                                    (int)'S', COMMAND_SHORTCUT_CTRLSHIFT,                     // Menu Shortcut Key, Shortcut Modifier
                                    false, false, false,                                     // Checked, Disabled, Hidden
                                    BelowTag,                                               // Add Below Command Tag
                                    false, false,                                            // Add Separator, Add Command List
                                    DYNAMICS, ResID);                                       // Security Dictionary and Form Resource ID
                if (SOPTrxSearchMenuTag <= 0)
                {
                    throw new Exception("SOP Transaction Search RegisterWithSecurity, error code: " + Convert.ToString(RMTrxSearchMenuTag));
                }
                #endregion


                #region Add RM Transaction Search Menu
                ParentTag = MenusForVisualStudioTools.Functions.GetTagByName.Invoke(DYNAMICS, "Command_Sales", "CL_Sales_Inquiry");           // Dictionary ID, Form Name, Command Name
                if (ParentTag <= 0)
                {
                    throw new Exception("RM Parent GetTagByName, error code: " + Convert.ToString(ParentTag));
                }

                // Get Below Tag for command/command list to add menu below
                BelowTag = MenusForVisualStudioTools.Functions.GetTagByName.Invoke(DYNAMICS, "Command_Sales", "RM_Transaction_Inquiry_Document");  // Dictionary ID, Form Name, Command Name
                if (BelowTag <= 0)
                {
                    throw new Exception("RM Below GetTagByName, error code: " + Convert.ToString(BelowTag));
                }

                // Get Security Form Resource ID for menus to inherit security access from
                ResID = MenusForVisualStudioTools.Functions.GetFormResId.Invoke(DYNAMICS, "RM_Transaction_Inquiry");                   // Get Form Resource ID for Security 
                if (ResID <= 0)
                {
                    throw new Exception("RM GetFormResId, error code: " + Convert.ToString(ResID));
                }
                
                // Add Menu entry using API Function call to create first sub menu entry with security
                RMTrxSearchMenuTag = MenusForVisualStudioTools.Functions.RegisterWithSecurity.Invoke(
                                    ParentTag,                                                      // Parent Command Tag
                                    "RM Transaction Search",                                // Menu Caption
                                    "Open RM Transaction Search",                           // Menu Tooltip
                                    (int)'R', COMMAND_SHORTCUT_CTRLSHIFT,                     // Menu Shortcut Key, Shortcut Modifier
                                    false, false, false,                                     // Checked, Disabled, Hidden
                                    BelowTag,                                               // Add Below Command Tag
                                    false, false,                                            // Add Separator, Add Command List
                                    DYNAMICS, ResID);                                       // Security Dictionary and Form Resource ID
                if (RMTrxSearchMenuTag <= 0)
                {
                    throw new Exception("RM Transaction Search RegisterWithSecurity, error code: " + Convert.ToString(RMTrxSearchMenuTag));
                }
                #endregion






                #region Add GL Transaction Search Menu
                //ParentTag = MenusForVisualStudioTools.Functions.GetTagByName.Invoke(DYNAMICS, "Command_Financial", "CL_Financial_Inquiry");           // Dictionary ID, Form Name, Command Name
                //if (ParentTag <= 0)
                //{
                //    throw new Exception("GL GetTagByName, error code: " + Convert.ToString(ParentTag));
                //}

                //// Get Below Tag for command/command list to add menu below
                //BelowTag = MenusForVisualStudioTools.Functions.GetTagByName.Invoke(DYNAMICS, "Command_Financial", "GL_Inquiry_Current_Summary");  // Dictionary ID, Form Name, Command Name
                //if (BelowTag <= 0)
                //{
                //    throw new Exception("GL Below GetTagByName, error code: " + Convert.ToString(BelowTag));
                //}

                //// Get Security Form Resource ID for menus to inherit security access from
                //ResID = MenusForVisualStudioTools.Functions.GetFormResId.Invoke(DYNAMICS, "GL_Inquiry_Current_Detail");                   // Get Form Resource ID for Security 
                //if (ResID <= 0)
                //{
                //    throw new Exception("GL GetFormResId, error code: " + Convert.ToString(ResID));
                //}
                               
                //// Add Menu entry using API Function call to create first sub menu entry with security
                //GLTrxSearchMenuTag = MenusForVisualStudioTools.Functions.RegisterWithSecurity.Invoke(
                //                    ParentTag,                                                      // Parent Command Tag
                //                    "GL Transaction Search",                                // Menu Caption
                //                    "Open GL Transaction Search",                           // Menu Tooltip
                //                    (int)'G', COMMAND_SHORTCUT_CTRLSHIFT,                     // Menu Shortcut Key, Shortcut Modifier
                //                    false, false, false,                                     // Checked, Disabled, Hidden
                //                    BelowTag,                                               // Add Below Command Tag
                //                    false, false,                                            // Add Separator, Add Command List
                //                    DYNAMICS, ResID);                                       // Security Dictionary and Form Resource ID
                //if (GLTrxSearchMenuTag <= 0)
                //{
                //    throw new Exception("GL Transaction Search RegisterWithSecurity, error code: " + Convert.ToString(GLTrxSearchMenuTag));
                //}
                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, APPNAME);

                // Unregister menu entry
                if (PMTrxSearchMenuTag > 0)
                {
                    Err = MenusForVisualStudioTools.Functions.Unregister.Invoke(0, PMTrxSearchMenuTag);
                    if (Err < 0)
                    {
                        MessageBox.Show("PM Transaction Search Menu Unregister, error code: " + Convert.ToString(Err), APPNAME);
                    }
                }

                // Unregister menu entry
                if (SOPTrxSearchMenuTag > 0)
                {
                    Err = MenusForVisualStudioTools.Functions.Unregister.Invoke(0, SOPTrxSearchMenuTag);
                    if (Err < 0)
                    {
                        MessageBox.Show("SOP Transaction Search Menu Unregister, error code: " + Convert.ToString(Err), APPNAME);
                    }
                }

                // Unregister menu entry
                if (RMTrxSearchMenuTag > 0)
                {
                    Err = MenusForVisualStudioTools.Functions.Unregister.Invoke(0, RMTrxSearchMenuTag);
                    if (Err < 0)
                    {
                        MessageBox.Show("RM Transaction Search Menu Unregister, error code: " + Convert.ToString(Err), APPNAME);
                    }
                }

                // Unregister menu entry
                if (GLTrxSearchMenuTag > 0)
                {
                    Err = MenusForVisualStudioTools.Functions.Unregister.Invoke(0, GLTrxSearchMenuTag);
                    if (Err < 0)
                    {
                        MessageBox.Show("GL Transaction Search Menu Unregister, error code: " + Convert.ToString(Err), APPNAME);
                    }
                }

            }
        }


        // Script to handle menu entry callbacks
        // void VSTMCommandFormCallback(object sender, EventArgs e)
        void VSTMCommandFormCallback(object sender, EventHandlerFunction.InvokeEventArgs e)
        {
            short Tag = 0;
            string Caption = "";

            // Get Callback Tag Number for menu entry
            // Tag = MenusForVisualStudioTools.Functions.Callback.Invoke();
            Tag = e.inParam1;

            // Compare Tag Sequence Number with Menu Sequence obtained during registration
            if (Tag == PMTrxSearchMenuTag)
            {
                OpenPMSearch();
            }
            else if (Tag == RMTrxSearchMenuTag)
            {
                OpenRMSearch();
            }
            else if (Tag == SOPTrxSearchMenuTag)
            {
                OpenSOPSearch();
            }
            else
            {
                // Not one of this application's menu entries
            }
        }


        private void OpenPMTransactionSearch(object sender, EventArgs e)
        {
            OpenPMSearch();
        }

        private void OpenPMTransactionSearchVendor(object sender, EventArgs e)
        {
            Controller.Instance.Model.VendorIDDefault = pmVendorMaintWindow.VendorId.Value.Trim();
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
            //Return focus to PM search window after GP window is closed
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


        private void OpenRMTransactionSearch(object sender, EventArgs e)
        {
            OpenRMSearch();
        }

        private void OpenRMSearch()
        {
            if (rmSearch == null)
            {
                rmSearch = new RMTransactionSearch();
                rmSearch.FormClosed += delegate { rmSearch = null; };
                rmSearch.Show();
            }
            Application.OpenForms[rmSearch.Name].Focus();
        }

        private void SetRMSearchFocus(object sender, EventArgs e)
        {
            //Return focus to RM search window after GP window is closed
            if (Controller.Instance.Model.RMSearchFocus)
            {
                SetRMTransactionSearchFocus();
            }
            else if (Controller.Instance.Model.SOPSearchFocus)
            {
                SetSOPTransactionSearchFocus();
            }
        }

        private void SetRMTransactionSearchFocus()
        {
            //Return focus to Search window after GP inquiry window is closed
            Application.OpenForms[rmSearch.Name].Focus();
            Controller.Instance.Model.RMSearchFocus = false;
        }

        private void OpenRMTransactionSearchCustomer(object sender, EventArgs e)
        {
            Controller.Instance.Model.CustomerIDDefault = rmCustomerMaintWindow.CustomerNumber.Value.Trim();
            OpenRMSearch();
        }

        
        private void OpenSOPTransactionSearch(object sender, EventArgs e)
        {
            OpenSOPSearch();
        }

        private void OpenSOPSearch()
        {
            if (sopSearch == null)
            {
                sopSearch = new SOPTransactionSearch();
                sopSearch.FormClosed += delegate { sopSearch = null; };
                sopSearch.Show();
            }
            Application.OpenForms[sopSearch.Name].Focus();
        }

        private void SetSOPSearchFocus(object sender, EventArgs e)
        {
            if (Controller.Instance.Model.SOPSearchFocus)
            {
                SetSOPTransactionSearchFocus();
            }
        }

        private void SetSOPTransactionSearchFocus()
        {
            Application.OpenForms[sopSearch.Name].Focus();
            Controller.Instance.Model.SOPSearchFocus = false;
        }

    }
}
