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

        public bool PMSearchFocus { get; set; }
        public bool ReplacePMInquiryVendor { get; set; }
        public bool ReplacePMInquiryDocument { get; set; }
        public string VendorIDDefault { get; set; }
        public string PMVendorLabel { get; set; }

    }
}
