using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.TransactionSearch
{
    public class PMSearchFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string DocNumber { get; set; }
        public decimal AmountFrom { get; set; }
        public decimal AmountTo { get; set; }

        public string SelectedEntities { get; set; }

    }
}
