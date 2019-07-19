using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace GP.TransactionSearch
{
    public class Controller
    {
        private static Model model = new Model();
        private static readonly Controller instance = new Controller();
        private System.ComponentModel.IContainer components = null;

        private static PMSearchFilter pmSearchFilter = new PMSearchFilter();

        public Model Model
        {
            get
            {
                return Controller.model;
            }
        }
        
        public static Controller Instance
        {
            get
            {
                return Controller.instance;
            }
        }

        private Controller()
        {
            try
            {
                components = new System.ComponentModel.Container();
                pmSearchFilter = new PMSearchFilter();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Dispose(bool disposing)
        {
            try
            {
                if (disposing && this.components != null)
                {
                    this.components.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Controller()
        {
            this.Dispose();
        }



        public bool LoadConfiguration()
        {
            try
            {
                
                string assemblyPath = string.Empty;
                string configFile = string.Empty;
                string settings = string.Empty;

                //We're using a DLL config file, so we have to load and map it
                Assembly myAssembly = Assembly.GetExecutingAssembly();

                //Get assembly version
                System.Version currentVersion = myAssembly.GetName().Version;
                Controller.Instance.Model.AssemblyVersion = currentVersion.ToString();

                //Initialize Search Filter key number for Controller.AddSearchFilter
                Controller.instance.Model.NextSearchFilterKey = 1;


                //Requires reference to System.Configuration and "using System.Configuration" statement
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();

                //Get path of assembly
                assemblyPath = myAssembly.Location;
                //Build config file name
                configFile = assemblyPath + ".config";
                //Assign config file
                configFileMap.ExeConfigFilename = configFile;
                //Load configuration
                System.Configuration.Configuration myConfig = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
                //Get properties settings section
                ClientSettingsSection configSection = (ClientSettingsSection)myConfig.GetSection("userSettings/GP.TransactionSearch.Properties.Settings");

                bool success = false;
                bool boolSetting = false;

                //Get config values, populate Model

                //Try and handle missing / incorrect config settings or invalid values
                //Bulky looking, open to a more streamlined method

                if (configSection.Settings.Get("SearchAsYouType").Value != null && (!string.IsNullOrEmpty(configSection.Settings.Get("SearchAsYouType").Value.ValueXml.InnerText)))
                {
                    success = bool.TryParse(configSection.Settings.Get("SearchAsYouType").Value.ValueXml.InnerText, out boolSetting);

                    if (success)
                    {
                        Controller.instance.Model.SearchAsYouType = Convert.ToBoolean(configSection.Settings.Get("SearchAsYouType").Value.ValueXml.InnerText);
                    }
                    else
                    {
                        Controller.instance.Model.SearchAsYouType = false;
                    }
                }

                if (configSection.Settings.Get("PMVendorLabel") != null && configSection.Settings.Get("PMVendorLabel").Value != null && (!string.IsNullOrEmpty(configSection.Settings.Get("PMVendorLabel").Value.ValueXml.InnerText)))
                {
                    Controller.instance.Model.PMVendorLabel = configSection.Settings.Get("PMVendorLabel").Value.ValueXml.InnerText.Trim();
                }
                else
                {
                    Controller.instance.Model.PMVendorLabel = string.Empty;
                }

                if (configSection.Settings.Get("RMCustomerLabel") != null && configSection.Settings.Get("RMCustomerLabel").Value != null && (!string.IsNullOrEmpty(configSection.Settings.Get("RMCustomerLabel").Value.ValueXml.InnerText)))
                {
                    Controller.instance.Model.RMCustomerLabel = configSection.Settings.Get("RMCustomerLabel").Value.ValueXml.InnerText.Trim();
                }
                else
                {
                    Controller.instance.Model.PMVendorLabel = string.Empty;
                }

                if (configSection.Settings.Get("DefaultDatesFromUserDate").Value != null && (!string.IsNullOrEmpty(configSection.Settings.Get("DefaultDatesFromUserDate").Value.ValueXml.InnerText)))
                {
                    success = bool.TryParse(configSection.Settings.Get("DefaultDatesFromUserDate").Value.ValueXml.InnerText, out boolSetting);

                    if (success)
                    {
                        Controller.instance.Model.DefaultDatesFromUserDate = Convert.ToBoolean(configSection.Settings.Get("DefaultDatesFromUserDate").Value.ValueXml.InnerText);
                    }
                    else
                    {
                        Controller.instance.Model.DefaultDatesFromUserDate = false;
                    }
                }

                return true;

            }
            catch //(Exception ex)
            {

                return false;
            }

        }

        public void SetConnectionInfo()
        {
            Microsoft.Dexterity.Applications.DynamicsDictionary.SyBackupRestoreForm backup;
            backup = Microsoft.Dexterity.Applications.Dynamics.Forms.SyBackupRestore;
            model.GPServer = backup.Functions.GetServerNameWithInstance.Invoke();
            model.GPUserID = Microsoft.Dexterity.Applications.Dynamics.Globals.UserId.Value;
            model.GPPassword = Microsoft.Dexterity.Applications.Dynamics.Globals.SqlPassword.Value;
            model.GPSystemDB = Microsoft.Dexterity.Applications.Dynamics.Globals.SystemDatabaseName.Value;
            model.GPCompanyDB = Microsoft.Dexterity.Applications.Dynamics.Globals.IntercompanyId.Value;
        }
        
        public void SetConnectionInfo(string server, string userID, string password, string systemDB, string companyDB)
        {
            model.GPServer = server;
            model.GPUserID = userID;
            model.GPPassword = password;
            model.GPSystemDB = systemDB;
            model.GPCompanyDB = companyDB;
        }



        public PMSearchFilter PMSearchFilter {
            get {
                return Controller.pmSearchFilter;
            }
        }

        public PMTransaction GetPMKeysInfo(string trxNumber, string vendorID)
        {
            PMTransaction pmTrx = new PMTransaction();

            try
            {
                DataTable dataTable = DataAccess.GetPMKeysInfo(trxNumber, vendorID);
                if (dataTable.Rows.Count == 1)
                {
                    pmTrx = ObjectMapper.DataRowToObject<PMTransaction>(dataTable.Rows[0]);
                }

                return pmTrx;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred in Controller.GetPMTransaction: " + ex.Message);
                return pmTrx;
            }
        }


        public PMVoucher GetPMVoucher(PMTransaction pmTrx)
        {
            PMVoucher pmVoucher = new PMVoucher();

            try
            {
                DataTable dataTable = DataAccess.GetPMVoucherInfo(pmTrx.VENDORID, pmTrx.DOCTYPE, pmTrx.CNTRLNUM, pmTrx.DOCNUMBR);
                if (dataTable.Rows.Count == 1)
                {
                    pmVoucher = ObjectMapper.DataRowToObject<PMVoucher>(dataTable.Rows[0]);
                }

                return pmVoucher;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred in Controller.GetPMVoucher: " + ex.Message);
                return pmVoucher;
            }
        }



        public POPTransaction GetPOPTransaction(string vendorID, string voucherNumber, string docNumber)
        {
            POPTransaction pmTrx = new POPTransaction();

            try
            {
                DataTable dataTable = DataAccess.GetPOPTransactionInfo(vendorID, docNumber, voucherNumber);
                if (dataTable.Rows.Count == 1)
                {
                    pmTrx = ObjectMapper.DataRowToObject<POPTransaction>(dataTable.Rows[0]);
                }

                return pmTrx;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred in Controller.GetPOPTransaction: " + ex.Message);
                return pmTrx;
            }
        }


        public RMTransaction GetRMTransaction(string docNumber, int docType, string customerID)
        {
            RMTransaction rmTrx = new RMTransaction();

            try
            {
                DataTable dataTable = DataAccess.GetRMTransactionInfo(docNumber, docType, customerID);
                if (dataTable.Rows.Count == 1)
                {
                    rmTrx = ObjectMapper.DataRowToObject<RMTransaction>(dataTable.Rows[0]);
                }

                return rmTrx;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred in Controller.GetRMTransaction: " + ex.Message);
                return rmTrx;
            }
        }


        public bool SaveTextFile(string fullFilePath, string textToSave)
        {
            try
            {
                System.IO.StreamWriter streamWriter;
                streamWriter = new System.IO.StreamWriter(fullFilePath, false, Encoding.UTF8);
                streamWriter.Write(textToSave);
                streamWriter.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred saving the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
