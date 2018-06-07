using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.TransactionSearch
{
    public class PMTransaction
    {
        public string CNTRLNUM { get; set; }
        public short CNTRLTYP { get; set; }
        public short DCSTATUS { get; set; }
        public short DOCTYPE { get; set; }
        public string VENDORID { get; set; }
        public string DOCNUMBR { get; set; }
        public string TRXSORCE { get; set; }
        public string CHECKBKID { get; set; }
        public string BCHSOURC { get; set; }
        public string DOCDATE { get; set; }
    }
}
