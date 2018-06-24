using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.TransactionSearch
{
    public class SOPTransaction
    {
        public short Status { get; set; }
        public short DocType { get; set; }
        public string DocNum { get; set; }
        public string CustomerID { get; set; }

        public SOPTransaction()
        { }

        public SOPTransaction(short Status, short DocType, string DocNum, string CustomerID)
        {
            this.Status = Status;
            this.DocType = DocType;
            this.DocNum = DocNum;
            this.CustomerID = CustomerID;
        }
    }

    
}
