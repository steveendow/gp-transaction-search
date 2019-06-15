using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.TransactionSearch
{
    public class PMVoucher
    {
        public string Origin { get; set; }
        public string VCHRNMBR { get; set; }
        public string VENDORID { get; set; }
        public short DOCTYPE { get; set; }
        public DateTime DOCDATE { get; set; }
        public string DOCNUMBR { get; set; }
        public decimal DOCAMNT { get; set; }
        public decimal CURTRXAM { get; set; }
        public string BACHNUMB { get; set; }
        public string TRXSORCE { get; set; }
        public string BCHSOURC { get; set; }
        public string PORDNMBR { get; set; }
        public string CHEKBKID { get; set; }
        public DateTime POSTEDDT { get; set; }
        public string CURNCYID { get; set; }
        public DateTime PSTGDATE { get; set; }
    }
}
