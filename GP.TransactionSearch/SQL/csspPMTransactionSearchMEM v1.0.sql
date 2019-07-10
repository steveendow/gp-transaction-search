--The string_split function is only availale on SQL Server 2016 and higher.
--On older versions of SQL Server, use the intlist_to_tbl User Defined Function instead
--Function from:  http://www.sommarskog.se/arrays-in-sql.html

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'TF' AND name = 'intlist_to_tbl')
DROP FUNCTION intlist_to_tbl
GO

CREATE FUNCTION intlist_to_tbl (@list nvarchar(MAX))
   RETURNS @tbl TABLE (itemvalue VARCHAR(50) NOT NULL) AS
BEGIN
   DECLARE @pos        int,
           @nextpos    int,
           @valuelen   int

   SELECT @pos = 0, @nextpos = 1

   WHILE @nextpos > 0
   BEGIN
      SELECT @nextpos = charindex(',', @list, @pos + 1)
      SELECT @valuelen = CASE WHEN @nextpos > 0
                              THEN @nextpos
                              ELSE len(@list) + 1
                         END - @pos - 1
      INSERT @tbl (itemvalue)
         VALUES (convert(VARCHAR(50), substring(@list, @pos + 1, @valuelen)))
      SELECT @pos = @nextpos
   END
   RETURN
END
GO

GRANT SELECT ON intlist_to_tbl TO DYNGRP
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'csspPMTransactionSearchMEM')
DROP PROCEDURE csspPMTransactionSearchMEM
GO

CREATE PROCEDURE csspPMTransactionSearchMEM

@StartDate AS DATETIME, 
@EndDate AS DATETIME, 
@DocNumber AS VARCHAR(21),
@VendorID AS VARCHAR(15),
@VendorName AS VARCHAR(65),
@AmountFrom AS NUMERIC(19,5),
@AmountTo AS NUMERIC(19,5),
@SelectedEntities AS VARCHAR(5000)

AS

BEGIN

/***************************************************************
csspPMTransactionSearchMEM

Stored procedure that queries all Payables transactions, with filtering
on Doc Date, Doc Number, Vendor ID, Vendor Name, and Binary Stream MEM Entities

Modified version of csspPMTransactionSearch

Based on view_PM_Transactions by Victoria Yudin - Flexible Solutions, Inc.
https://victoriayudin.com/2009/10/12/sql-view-for-all-posted-payables-transactions-in-dynamics-gp/

Created July 8, 2019 by Steve Endow

July 8, 2019: Version 1.0

EXEC dbo.csspPMTransactionSearchMEM @StartDate = '2020-01-01',
                                    @EndDate = '2030-12-31',   
                                    @DocNumber = '', 
                                    @VendorID = '',
                                    @VendorName = '',
								    @AmountFrom = 0,
								    @AmountTo = 10000000000,
								    @SelectedEntities = '100,200,300,400,999'

***************************************************************/

--If you have a large number of transaction history, adjust TOP count as needed for performance
SELECT TOP 200 	
	P.Origin, 
	P.Entity, 
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
	RTRIM(P.VENDORID) VendorID,  --This field value must be named VendorID for drill down, DO NOT REMOVE
	RTRIM(V.VENDNAME) Name,
	CAST(P.DOCAMNT AS NUMERIC(19,2)) Amount, --Cast to specify visible decimals
	RTRIM(P.VCHRNMBR) TrxNumber,  --This field value must be named TrxNumber for drill down, DO NOT REMOVE
	P.DUEDATE DueDate,
	CAST(P.CURTRXAM AS NUMERIC(19,2)) Unapplied, --Cast to specify visible decimals
	RTRIM(P.TRXDSCRN) [Description],
	CASE P.VOIDED
	     WHEN 0 THEN 'No'
	     WHEN 1 THEN 'Yes'
	     END Voided

FROM	(
	SELECT 'WORK' AS Origin, RTRIM(mem.BSSI_Facility_ID) AS Entity, pm.VENDORID, pm.VCHNUMWK AS VCHRNMBR, pm.DOCTYPE, pm.DOCDATE, pm.PSTGDATE,
	 pm.DUEDATE, pm.DOCNUMBR, pm.DOCAMNT, pm.CURTRXAM, pm.TRXDSCRN, 0 AS VOIDED
	 FROM dbo.PM10000 pm
	 JOIN dbo.B3920000 mem ON mem.VCHNUMWK = pm.VCHNUMWK AND mem.DOCTYPE = pm.DOCTYPE 

	UNION ALL

	 SELECT 'OPEN' AS Origin, RTRIM(mem.BSSI_Facility_ID) AS Entity, pm.VENDORID, pm.VCHRNMBR, pm.DOCTYPE, pm.DOCDATE, pm.PSTGDATE,
	 pm.DUEDATE, pm.DOCNUMBR, pm.DOCAMNT, pm.CURTRXAM, pm.TRXDSCRN, pm.VOIDED
	 FROM dbo.PM20000 pm
	 JOIN dbo.B3920001 mem ON mem.VCHRNMBR = pm.VCHRNMBR AND mem.DOCTYPE = pm.DOCTYPE AND mem.DOCNUMBR = pm.DOCNUMBR

	UNION ALL

	 SELECT 'HIST' AS Origin, RTRIM(mem.BSSI_Facility_ID) AS Entity, pm.VENDORID, pm.VCHRNMBR, pm.DOCTYPE, pm.DOCDATE, pm.PSTGDATE,
	 pm.DUEDATE, pm.DOCNUMBR, pm.DOCAMNT, pm.CURTRXAM, pm.TRXDSCRN, pm.VOIDED
	 FROM dbo.PM30200 pm
	 JOIN dbo.B3920200 mem ON mem.VCHRNMBR = pm.VCHRNMBR AND mem.DOCTYPE = pm.DOCTYPE AND mem.DOCNUMBR = pm.DOCNUMBR
	 	 
	 ) P

     INNER JOIN dbo.PM00200 V ON V.VENDORID = P.VENDORID

	 WHERE P.DOCDATE >= @StartDate AND P.DOCDATE <= @EndDate
	 AND P.DOCNUMBR LIKE '%'+ UPPER(@DocNumber) +'%'
	 AND P.VENDORID LIKE '%'+ UPPER(@VendorID) +'%'
	 AND UPPER(V.VENDNAME) LIKE '%' + UPPER(@VendorName) + '%'
	 AND P.DOCAMNT >= @AmountFrom
	 AND P.DOCAMNT <= @AmountTo
	 --On SQL Server 2016 and higher, the string_split function can be used
	 --AND P.Entity IN (SELECT LTRIM(RTRIM(convert(VARCHAR(20), value))) FROM string_split(@SelectedEntities, ','))
	 --On older versions of SQL Server, use the intlist_to_tbl User Defined Function instead
	 AND P.Entity IN (SELECT RTRIM(LTRIM(itemvalue)) FROM intlist_to_tbl(@SelectedEntities))
	 --AND EXISTS (SELECT * FROM intlist_to_tbl(@SelectedEntities) WHERE P.Entity = RTRIM(LTRIM(itemvalue)))

	
END
GO 

GRANT EXEC ON csspPMTransactionSearchMEM TO DYNGRP
GO 