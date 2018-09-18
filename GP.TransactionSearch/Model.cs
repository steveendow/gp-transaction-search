using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.TransactionSearch
{
    public class Model
    {
        public string AssemblyVersion { get; set; }

        public string GPServer { get; set; }
        public string GPSystemDB { get; set; }
        public string GPCompanyDB { get; set; }
        public string GPUserID { get; set; }
        public string GPPassword { get; set; }

        //Used to support testing of windows using EXE outside of GP
        public bool IsExternal { get; set; }
        
        public bool SearchAsYouType { get; set; }

        public int NextSearchFilterKey { get; set; }

        public bool PMSearchFocus { get; set; }
        public string VendorIDDefault { get; set; }
        public string PMVendorLabel { get; set; }

        public bool RMSearchFocus { get; set; }
        public string CustomerIDDefault { get; internal set; }
        public string RMCustomerLabel { get; set; }

        public bool SOPSearchFocus { get; set; }

        public bool DefaultDatesFromUserDate { get; set; }
    }
}
