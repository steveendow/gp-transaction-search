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
 * -v1.0.0.3 - June 7, 2018
 * -The PM Trx Inquiry Vendor window behaves differently than the PM Trx Inquiry Document window when we attempt to replace them.
 * The Vendor window will throw a Dex error when e.Cancel is used in a OpenBeforeOriginal CancelEventHandler, and the Vendor window
 * only seems to like the .Close() method in the OpenBeforeOriginal.
 * Conversely, the Document window will throw an error when .Close() is used in OpenBeforeOriginal. e.Cancel does work, but only
 * the first time the window is opened. After the first open and e.Cancel is used, the OpenBeforeOriginal event will not fire.
 * So, as a reluctant compromise, for the Document window, do not use OpenBeforeOriginal--instead use OpenAfterOriginal to open PM Trx Search, let the
 * Document window open, and then close the Document window.  The Document window will be visible as it opens and closes, but it's the only way
 * I found to close the window without causing either a Dex error or a problem opening the window again.
 * Having the window visibly open and then close isn't ideal, but I have run out of ideas at the moment.
 * 
 * -v1.0.0.4 - June 7, 2018
 * -Fix incorrect GPConnNet reference in GPSQLConnection2018.dll and update references for all versions of GP Transaction Search
 * -Set ReplacePMInquiryVendor setting to False as default
 * 
 * -v1.0.0.5 - June 7, 2018
 * -When opening the PM Transaction Search window from Vendor Maintenance, default the vendor ID on the search window
 *  and pre-populate grid with vendor data.
 * -Fix tab order of search filter fields on PMTransactionSearch window
 * 
 */


// TODO: Add optional config file settings to override PM Transaction Search window labels, allowing replacement of Vendor -> Creditor


// WISHLIST: Add support for POP Invoice Zoom window (Requires Dex assistance)