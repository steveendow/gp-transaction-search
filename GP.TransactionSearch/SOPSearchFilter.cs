using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.TransactionSearch
{
    class SOPSearchFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string MasterID { get; set; }
        public string MasterName { get; set; }
        public string DocNumber { get; set; }
        public decimal AmountFrom { get; set; }
        public decimal AmountTo { get; set; }
        public string ItemNumber { get; set; }
        public string ItemDescription { get; set; }
    }
}
