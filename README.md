Updated 6/30/2018 - v2.0.0.0


# Microsoft Dynamics GP Transaction Search
GP Transaction Search is a Visual Studio Tools AddIn for Microsoft Dynamics GP

Web site: [GP Transaction Search](https://precipioservices.com/free/gp-transaction-search/)

Overview:
[Dynamics GP Land Blog Post by Steve Endow](https://dynamicsgpland.blogspot.com/2018/06/dynamics-gp-transaction-search-v10-is.html)

Background:
[GPUG Open Forum Post](https://www.gpug.com/communities/community-home/digestviewer/viewthread?GroupId=247&MessageKey=662417f0-4c68-4ad8-b644-c4f628e45442&CommunityKey=4754a624-39c5-4458-8105-02b65a7e929e&tab=digestviewer&ReturnUrl=%2fcommunities%2fcommunity-home%2fdigestviewer%3fListKey%3dc8985617-e1ed-4b37-9427-d2bc0e80cbc1%26CommunityKey%3d4754a624-39c5-4458-8105-02b65a7e929e)

Developed in Visual Studio 2017 

You will need VS 2017 Version 15.5.7 or higher to login to Git - [See Bug Report Here](https://github.com/github/VisualStudio/issues/949)

Supported Dynamics GP Versions:  2013, 2015, 2016, 2018


## Project Dependencies
.NET 4.5.2

GPSQLConnection.dll (simple wrapper for GPConnNet developed by Steve Endow to avoid including GPConnNet license keys in open source code)

NEW in v2.0: Visual Studio Integration Toolkit by Wintrhop Dexterity Consultants: https://www.winthropdc.com/products_VSIT.htm
--Free, but requires basic registration after install

Dynamics GP 2013 - 2018 client application


## Project Questions
Contact Steve Endow via the [Precipio Services web site - Contact Us Page](https://precipioservices.com/contact-us/)


## Contributing
We'll need to figure that out. But here's the [Open Code of Conduct](http://todogroup.org/opencodeofconduct/#VisualStudio/opensource@github.com) for the project.

## Need
I need help from an experienced Dex developer to figure out how to open the POP Invoice Zoom window for drill down to Purchase Orders. Visual Studio Tools is unable to open the POP Zoom window directly, and that window does not have an OpenWindow.Invoke method exposed by the VST SDK.  The only remaining option I am aware of is Continuum, but I will need help creating the SanScript to try and open the window.
