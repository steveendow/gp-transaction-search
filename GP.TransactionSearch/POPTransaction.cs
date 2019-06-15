using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.TransactionSearch
{
    public class POPTransaction
    {
        public string Origin { get; set; }
        public string POPRCTNM { get; set; }
        public int POPTYPE { get; set; }
        public string VNDDOCNM { get; set; }
        public DateTime receiptdate { get; set; }
        public string BCHSOURC { get; set; }
        public string VENDORID { get; set; }
        public DateTime POSTEDDT { get; set; }
        public string TRXSORCE { get; set; }
        public string VCHRNMBR { get; set; }

    }
}
