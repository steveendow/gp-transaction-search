using Microsoft.Dexterity;
using Microsoft.Dexterity.Applications;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GP.TransactionSearch
{
    class DataAccess
    {
        static public string ConnectionStringWindowsGP
        {
            get
            {
                //Return connection string for ".NET Framework Data Provider for SQL Server"  (System.Data.SqlClient.SqlConnection)
                return @"Data Source=" + Controller.Instance.Model.GPServer + ";Initial Catalog=" + Controller.Instance.Model.GPCompanyDB + ";Integrated Security=SSPI;";
            }
        }


        static public string ConnectionStringGP
        {

            get
            {
                string gpLoginType = "SQL";
                string gpServer = Controller.Instance.Model.GPServer;
                string gpDatabase = Controller.Instance.Model.GPCompanyDB;
                string gpUser = Controller.Instance.Model.GPUserID;
                string gpPassword = Controller.Instance.Model.GPPassword;

                if (gpLoginType == "SQL" && gpServer != string.Empty && gpDatabase != string.Empty && gpUser != string.Empty && gpPassword != string.Empty)
                {
                    //Return connection string for ".NET Framework Data Provider for SQL Server"  (System.Data.SqlClient.SqlConnection)
                    return @"Data Source=" + gpServer + ";Initial Catalog=" + gpDatabase + ";User ID=" + gpUser + ";Password=" + gpPassword + ";";
                }
                else if (gpLoginType.ToUpper() == "WINDOWS" && gpServer != string.Empty && gpDatabase != string.Empty)
                {
                    //Return connection string for ".NET Framework Data Provider for SQL Server"  (System.Data.SqlClient.SqlConnection)
                    return @"Data Source=" + gpServer + ";Initial Catalog=" + gpDatabase + ";Integrated Security=SSPI;";
                }
                else
                {
                    return "";
                }

            }

        }


        private static SqlConnection ConnectionGP()
        {
            SqlConnection sqlConn = new SqlConnection();

            if (!Controller.Instance.Model.IsExternal)
            {
                sqlConn = CreateGPConnection();
            }
            else
            {
                sqlConn.ConnectionString = ConnectionStringGP;
                sqlConn.Open();    
            }

            return sqlConn;
        }


        private static SqlConnection CreateGPConnection()
        {
            SqlConnection sqlConn = GPSQLConnection.GPConn.GetConnection(Dynamics.Globals.SqlDataSourceName.Value, Controller.Instance.Model.GPCompanyDB, Dynamics.Globals.UserId.Value, Dynamics.Globals.SqlPassword.Value);   
            return sqlConn;
        }


        internal static int ExecuteDataSet(ref DataTable dataTable, SqlConnection sqlConn, string database, CommandType commandType, string commandText, SqlParameter[] sqlParameters)
        {

            try
            {
                SqlCommand cmd = new SqlCommand(commandText);
                cmd.Connection = sqlConn;
                cmd.CommandType = commandType;  //System.Data.CommandType.StoredProcedure;

                if (sqlParameters != null)
                {
                    foreach (SqlParameter sqlParameter in sqlParameters)
                    {
                        cmd.Parameters.Add(sqlParameter);
                    }
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                return dataTable.Rows.Count;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred in ExecuteDataSet: " + ex.Message);
                return 0;
            }
            finally
            {
                sqlConn.Close();
            }
        }


        internal static int ExecuteNonQuery(SqlConnection sqlConn, string database, CommandType commandType, string commandText, SqlParameter[] sqlParameters)
        {

            try
            {
                int rowsAffected = 0;

                SqlCommand cmd = new SqlCommand(commandText);
                cmd.Connection = sqlConn;
                cmd.CommandType = commandType;

                if ((commandType == CommandType.StoredProcedure) || (commandType == CommandType.Text))
                {
                    if (sqlParameters != null)
                    {
                        foreach (SqlParameter sqlParameter in sqlParameters)
                        {
                            cmd.Parameters.Add(sqlParameter);
                        }
                    }
                }

                rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred in ExecuteNonQuery: " + ex.Message);
                return 0;
            }
            finally
            {
                sqlConn.Close();
            }
        }


        internal static string ExecuteScalar(SqlConnection sqlConn, string database, CommandType commandType, string commandText, SqlParameter[] sqlParameters)
        {
            string scalarResult = "";

            try
            {
                SqlCommand cmd = new SqlCommand(commandText);
                cmd.Connection = sqlConn;
                cmd.CommandType = commandType;

                if ((commandType == CommandType.StoredProcedure) || (commandType == CommandType.Text))
                {
                    if (sqlParameters != null)
                    {
                        foreach (SqlParameter sqlParameter in sqlParameters)
                        {
                            cmd.Parameters.Add(sqlParameter);
                        }
                    }
                }

                scalarResult = Convert.ToString(cmd.ExecuteScalar()).Trim();

                return scalarResult;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred in ExecuteScalar: " + ex.Message);
                return string.Empty;
            }
            finally
            {
                sqlConn.Close();
            }
        }


        internal static bool ValidSQLLogin(SqlConnection sqlConn, ref string error)
        {

            error = string.Empty;

            try
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                error = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }

        }


        internal static bool ValidGPLogin(ref string error)
        {
            string result = string.Empty;
            error = string.Empty;

            try
            {
                SqlConnection gpConn = ConnectionGP();

                result = ExecuteScalar(gpConn, "DYNAMICS", CommandType.Text, "SELECT COUNT(*) AS Records FROM DYNAMICS..ACTIVITY", null);
                if (result.Trim() != "")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }

        }


        internal static DataTable PMTransactionSearch(DateTime startDate, DateTime endDate, string docNumber, string vendorID, string vendorName, decimal amountFrom, decimal amountTo)
        {
            try
            {
                
                string commandText = "csspPMTransactionSearch";

                SqlParameter[] sqlParameters = new SqlParameter[7];
                sqlParameters[0] = new SqlParameter("@StartDate", System.Data.SqlDbType.DateTime);
                sqlParameters[0].Value = startDate;
                sqlParameters[1] = new SqlParameter("@EndDate", System.Data.SqlDbType.DateTime);
                sqlParameters[1].Value = endDate;
                sqlParameters[2] = new SqlParameter("@DocNumber", System.Data.SqlDbType.VarChar, 21);
                sqlParameters[2].Value = docNumber;
                sqlParameters[3] = new SqlParameter("@VendorID", System.Data.SqlDbType.VarChar, 15);
                sqlParameters[3].Value = vendorID;
                sqlParameters[4] = new SqlParameter("@VendorName", System.Data.SqlDbType.VarChar, 65);
                sqlParameters[4].Value = vendorName;
                sqlParameters[5] = new SqlParameter("@AmountFrom", System.Data.SqlDbType.Decimal);
                sqlParameters[5].Value = amountFrom;
                sqlParameters[6] = new SqlParameter("@AmountTo", System.Data.SqlDbType.Decimal);
                sqlParameters[6].Value = amountTo;

                DataTable dataTable = new DataTable();
            
                SqlConnection sqlConn = ConnectionGP();

                int records = ExecuteDataSet(ref dataTable, sqlConn, Controller.Instance.Model.GPCompanyDB, CommandType.StoredProcedure, commandText, sqlParameters);

                return dataTable;
            }

            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred in PMTransactionSearch: " + ex.Message);
                return null;
            }
        }


        internal static DataTable RMTransactionSearch(DateTime startDate, DateTime endDate, string docNumber, string customerID, string customerName, decimal amountFrom, decimal amountTo)
        {
            try
            {

                string commandText = "csspRMTransactionSearch";

                SqlParameter[] sqlParameters = new SqlParameter[7];
                sqlParameters[0] = new SqlParameter("@StartDate", System.Data.SqlDbType.DateTime);
                sqlParameters[0].Value = startDate;
                sqlParameters[1] = new SqlParameter("@EndDate", System.Data.SqlDbType.DateTime);
                sqlParameters[1].Value = endDate;
                sqlParameters[2] = new SqlParameter("@DocNumber", System.Data.SqlDbType.VarChar, 21);
                sqlParameters[2].Value = docNumber;
                sqlParameters[3] = new SqlParameter("@CustomerID", System.Data.SqlDbType.VarChar, 15);
                sqlParameters[3].Value = customerID;
                sqlParameters[4] = new SqlParameter("@CustomerName", System.Data.SqlDbType.VarChar, 65);
                sqlParameters[4].Value = customerName;
                sqlParameters[5] = new SqlParameter("@AmountFrom", System.Data.SqlDbType.Decimal);
                sqlParameters[5].Value = amountFrom;
                sqlParameters[6] = new SqlParameter("@AmountTo", System.Data.SqlDbType.Decimal);
                sqlParameters[6].Value = amountTo;

                DataTable dataTable = new DataTable();

                SqlConnection sqlConn = ConnectionGP();

                int records = ExecuteDataSet(ref dataTable, sqlConn, Controller.Instance.Model.GPCompanyDB, CommandType.StoredProcedure, commandText, sqlParameters);

                return dataTable;
            }

            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred in RMTransactionSearch: " + ex.Message);
                return null;
            }
        }


        internal static DataTable GetPMTransactionInfo(string trxNumber, string vendorID)
        {
            try
            {

                //Need to verify which parameters GP uses for queries against PM00400
                //The CNTRLNUM field alone is not unique for all PM transactions--the same number can be used for 
                //different doc types.  Instead of deciphering numeric doc type from the search window, use
                //Vendor ID instead as a compromise. Not guaranteed to be unique, but usable for initial testing.

                string commandText = "SELECT CNTRLNUM, CNTRLTYP, DCSTATUS, DOCTYPE, RTRIM(VENDORID) AS VENDORID, RTRIM(DOCNUMBR) AS DOCNUMBR, RTRIM(TRXSORCE) AS TRXSORCE, RTRIM(CHEKBKID) AS CHEKBKID, ";
                commandText += "DUEDATE, DISCDATE, RTRIM(BCHSOURC) AS BCHSOURC, DOCDATE, RTRIM(USERID) AS USERID, DEX_ROW_ID FROM dbo.PM00400 WHERE CNTRLNUM = @CNTRLNUM AND VENDORID = @VENDORID";

                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@CNTRLNUM", System.Data.SqlDbType.VarChar, 21);
                sqlParameters[0].Value = trxNumber;
                sqlParameters[1] = new SqlParameter("@VENDORID", System.Data.SqlDbType.VarChar, 15);
                sqlParameters[1].Value = vendorID;

                DataTable dataTable = new DataTable();

                SqlConnection sqlConn = ConnectionGP();

                int records = ExecuteDataSet(ref dataTable, sqlConn, Controller.Instance.Model.GPCompanyDB, CommandType.Text, commandText, sqlParameters);

                return dataTable;
            }

            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred in GetPMTransactionInfo: " + ex.Message);
                return null;
            }
        }


        internal static DataTable GetRMTransactionInfo(string docNumber, int docType, string customerID)
        {
            try
            {
                string commandText = "SELECT RTRIM(DOCNUMBR) AS DOCNUMBR, RMDTYPAL, DCSTATUS, RTRIM(BCHSOURC) AS BCHSOURC, RTRIM(TRXSORCE) AS TRXSORCE, RTRIM(CUSTNMBR) AS CUSTNMBR, ";
                commandText += "RTRIM(CHEKNMBR) AS CHEKNMBR, DOCDATE, NEGQTYSOPINV, DEX_ROW_ID ";
                commandText += "FROM dbo.RM00401 WHERE DOCNUMBR = @DOCNUMBR AND RMDTYPAL = @RMDTYPAL AND CUSTNMBR = @CUSTNMBR";

                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@DOCNUMBR", System.Data.SqlDbType.VarChar, 21);
                sqlParameters[0].Value = docNumber;
                sqlParameters[1] = new SqlParameter("@RMDTYPAL", System.Data.SqlDbType.VarChar, 21);
                sqlParameters[1].Value = docType;
                sqlParameters[2] = new SqlParameter("@CUSTNMBR", System.Data.SqlDbType.VarChar, 15);
                sqlParameters[2].Value = customerID;

                DataTable dataTable = new DataTable();

                SqlConnection sqlConn = ConnectionGP();

                int records = ExecuteDataSet(ref dataTable, sqlConn, Controller.Instance.Model.GPCompanyDB, CommandType.Text, commandText, sqlParameters);

                return dataTable;
            }

            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred in GetRMTransactionInfo: " + ex.Message);
                return null;
            }
        }
    }
}
