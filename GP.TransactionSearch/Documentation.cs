/*
 * GP Transaction Search
 * 
 * Credits:
 * -Thanks to Victoria Yudin for the PM Transaction view (and some assistance) that was the basis for the csspPMTransactionSearch procedure
 * -Props to Chad Bruels at RSM for the genius idea of using multiple VS projects to build for multiple GP versions   https://twitter.com/cbruels/status/915964138962259973
 *      https://softwareengineering.stackexchange.com/questions/358530/handling-multi-version-software-release/358552
 * 
 * 
 * v1.0.0.0 - June 4, 2018 - Initial release
 * 
 * v1.0.0.1 - June 5, 2018
 *  -Remove VST Controls
 *  -Add PM Transaction Inquiry menu replacement and config file options to enable replacement
 *  -Add Amount From and To fields
 *  -Modified csspPMTransactionSearch to support Amount From and Amount To search
 *  -Add double click for transaction inquiry
 *  -Fix UTF-8 formatting with CSV file export that caused invalid currency symbol in Excel
 *  -Return focus to PM Transaction Search window after GP zoom window is closed
 *  -Only allow one copy of PM Transaction Search window at a time
 * 
 *  Known bugs:
 *      -FIXED: Replacing the PM Trx Inquiry Document window with PM Transaction Search causes a Dex error dialog
 *      
 * -v1.0.0.2 - June 6, 2018
 *  -Modify ReplacePMInquiryVendor and ReplacePMInquiryDocument in GpAddIn.cs to have a CancelEventHandler parameter
 *  and use e.Cancel to abort GP window open. This appears to eliminate the Dex error when replacing the PM Trx Inquiry 
 *  Document window
 *  -Modify the config settings to set GenerateDefaultValueInCode = False, to try and fix issue where setting ReplacePMInquiryDocument = False
 *  did not turn off the window replacement in GP.
 *  
 */