using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.TransactionSearch
{
    public class RMTransaction
    {
        public string DOCNUMBR { get; set; }
        public short RMDTYPAL { get; set; }
        public short DCSTATUS { get; set; }
        public string BCHSOURC { get; set; }
        public string TRXSORCE { get; set; }
        public string CUSTNMBR { get; set; }
        public string CHEKNMBR { get; set; }
        public string DOCDATE { get; set; }
        public short NEGQTYSOPINV { get; set; }
    }
}
