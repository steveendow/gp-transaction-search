ALTER PROCEDURE csspPMTransactionSearch

@StartDate AS DATETIME, 
@EndDate AS DATETIME, 
@DocNumber AS VARCHAR(21),
@VendorID AS VARCHAR(15),
@VendorName AS VARCHAR(65),
@Amount AS NUMERIC(19,5)

AS

BEGIN

/***************************************************************
csspPMTransactionSearch

Stored procedure that queries all Payables transactions, with filtering
on Doc Date, Doc Number, Vendor ID, and Vendor Name

Based on view_PM_Transactions by Victoria Yudin - Flexible Solutions, Inc.
https://victoriayudin.com/2009/10/12/sql-view-for-all-posted-payables-transactions-in-dynamics-gp/

Created May 23, 2018 by Steve Endow

EXEC dbo.csspPMTransactionSearch @StartDate = '2017-01-01',
                                  @EndDate = '2017-12-31',   
                                  @DocNumber = '200', 
                                  @VendorID = 'AMERI',
                                  @VendorName = '',
								  @Amount = 0

***************************************************************/

--If you have a large number of transaction history, adjust TOP count as needed for performance
SELECT TOP 200 	
	P.Origin, 
	RTRIM(P.DOCNUMBR) DocNum,
	CASE P.DOCTYPE
	    WHEN 1 THEN 'INV'
	    WHEN 2 THEN 'FIN CHG'
		WHEN 3 THEN 'MISC CHG'
		WHEN 4 THEN 'RTN'
		WHEN 5 THEN 'CREDIT'
		WHEN 6 THEN 'PMT'
     END DocType,
	P.DOCDATE DocDate,
	P.PSTGDATE PostDate,
	RTRIM(P.VENDORID) VendorID,
	RTRIM(V.VENDNAME) Name,
	CAST(P.DOCAMNT AS NUMERIC(19,2)) Amount, --Cast to specify visible decimals
	RTRIM(P.VCHRNMBR) Voucher,
	P.DUEDATE DueDate,
	CAST(P.CURTRXAM AS NUMERIC(19,2)) Unapplied, --Cast to specify visible decimals
	RTRIM(P.TRXDSCRN) [Description],
	CASE P.VOIDED
	     WHEN 0 THEN 'No'
	     WHEN 1 THEN 'Yes'
	     END Voided

FROM	(
	SELECT 'WORK' AS Origin, VENDORID, VCHRNMBR, DOCTYPE, DOCDATE, PSTGDATE,
	 DUEDATE, DOCNUMBR, DOCAMNT, CURTRXAM, TRXDSCRN, 0 AS VOIDED
	 FROM dbo.PM10000
	UNION ALL
	 SELECT 'OPEN' AS Origin, VENDORID, VCHRNMBR, DOCTYPE, DOCDATE, PSTGDATE,
	 DUEDATE, DOCNUMBR, DOCAMNT, CURTRXAM, TRXDSCRN, VOIDED
	 FROM dbo.PM20000
	UNION ALL
	 SELECT 'HIST' AS Origin, VENDORID, VCHRNMBR, DOCTYPE, DOCDATE, PSTGDATE,
	 DUEDATE, DOCNUMBR, DOCAMNT, CURTRXAM, TRXDSCRN, VOIDED
	 FROM dbo.PM30200) P

     INNER JOIN dbo.PM00200 V ON V.VENDORID = P.VENDORID

	 WHERE P.DOCDATE >= @StartDate AND P.DOCDATE <= @EndDate
	 AND P.DOCNUMBR LIKE '%'+@DocNumber+'%'
	 AND P.VENDORID LIKE '%'+@VendorID+'%'
	 AND V.VENDNAME LIKE '%'+@VendorName+'%'
	 AND (@Amount = 0 OR P.DOCAMNT = @Amount)

END

GO 

GRANT EXEC ON csspPMTransactionSearch TO DYNGRP
GO 