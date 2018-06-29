IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'csspRMTransactionSearch')
DROP PROCEDURE csspRMTransactionSearch
GO


CREATE PROCEDURE csspRMTransactionSearch

@StartDate AS DATETIME, 
@EndDate AS DATETIME, 
@DocNumber AS VARCHAR(21),
@CustomerID AS VARCHAR(15),
@CustomerName AS VARCHAR(65),
@AmountFrom AS NUMERIC(19,5),
@AmountTo AS NUMERIC(19,5)

AS

BEGIN

/***************************************************************
csspRMTransactionSearch

Stored procedure that queries all Receivables transactions, with filtering
on Doc Date, Doc Number, Customer Number, and Customer Name

Based on view_PM_Transactions by Victoria Yudin - Flexible Solutions, Inc.
https://victoriayudin.com/2009/10/12/sql-view-for-all-posted-payables-transactions-in-dynamics-gp/

Created June 19, 2018 by Ian Grieve

EXEC dbo.csspRMTransactionSearch @StartDate = '2017-01-01',
                                  @EndDate = '2017-12-31',   
                                  @DocNumber = '200', 
                                  @CustomerID = 'AMERI',
                                  @CustomerName = '',
								  @AmountFrom = 0,
								  @AmountTo = 0

***************************************************************/

--If you have a large number of transaction history, adjust TOP count as needed for performance
SELECT TOP 200 	
	R.Origin,
	RTRIM(R.DOCNUMBR) DocNum,  --This field value must be named DocNum for drill down, DO NOT REMOVE
	S.DOCDESCR AS DocType,
	R.DOCDATE DocDate,
	R.POSTDATE PostDate,
	RTRIM(R.CUSTNMBR) CustomerID,  --This field value must be named CustomerID for drill down, DO NOT REMOVE
	RTRIM(C.CUSTNAME) [Name],
	CAST(R.ORTRXAMT AS NUMERIC(19,2)) Amount, --Cast to specify visible decimals
	RTRIM(R.CSPORNBR) PONumber,
	R.DUEDATE DueDate,
	CAST(R.CURTRXAM AS NUMERIC(19,2)) Unapplied, --Cast to specify visible decimals
	RTRIM(R.DOCDESCR) [Description],
	CASE R.VOIDSTTS
	     WHEN 0 THEN 'No'
	     WHEN 1 THEN 'Yes'
	     END Voided,
	R.RMDTYPAL	--RMDTYPAL is a REQUIRED field for RM Search to ensure unique lookups of RM transactions, DO NOT REMOVE. Column is hidden in RM Search grid.

FROM	(
	SELECT 'WORK' AS Origin, CUSTNMBR, DOCNUMBR, RMDTYPAL, DOCDATE, POSTEDDT AS POSTDATE,
	 DUEDATE, CSTPONBR AS CSPORNBR, DOCAMNT AS ORTRXAMT, DOCAMNT - APPLDAMT AS CURTRXAM, DOCDESCR, 0 AS VOIDSTTS
	 FROM dbo.RM10301 WITH (NOLOCK)
	UNION ALL
	 SELECT 'OPEN' AS Origin, CUSTNMBR, DOCNUMBR, RMDTYPAL, DOCDATE, POSTDATE,
	 DUEDATE, CSPORNBR, ORTRXAMT, CURTRXAM, TRXDSCRN, VOIDSTTS
	 FROM dbo.RM20101 WITH (NOLOCK)
	UNION ALL
	 SELECT 'HIST' AS Origin, CUSTNMBR, DOCNUMBR, RMDTYPAL, DOCDATE, POSTDATE,
	 DUEDATE, CSPORNBR, ORTRXAMT, CURTRXAM, TRXDSCRN, VOIDSTTS
	 FROM dbo.RM30101 WITH (NOLOCK)) R

     INNER JOIN dbo.RM00101 C WITH (NOLOCK) ON C.CUSTNMBR = R.CUSTNMBR
	 	 INNER JOIN dbo.RM40401 S WITH (NOLOCK) ON S.RMDTYPAL = R.RMDTYPAL

	 WHERE R.DOCDATE >= @StartDate AND R.DOCDATE <= @EndDate
	 AND R.DOCNUMBR LIKE '%'+@DocNumber+'%'
	 AND R.CUSTNMBR LIKE '%'+@CustomerID+'%'
	 AND C.CUSTNAME LIKE '%'+@CustomerName+'%'
	 AND R.ORTRXAMT >= @AmountFrom
	 AND R.ORTRXAMT <= @AmountTo
	
END

GO 

GRANT EXEC ON csspRMTransactionSearch TO DYNGRP
GO 