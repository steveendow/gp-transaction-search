IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'csspSOPTransactionSearch')
DROP PROCEDURE csspSOPTransactionSearch
GO

CREATE PROCEDURE dbo.csspSOPTransactionSearch
    @StartDate AS DATETIME, 
    @EndDate AS DATETIME, 
    @DocNumber AS VARCHAR(21), 
    @CustomerID AS VARCHAR(15), 
    @CustomerName AS VARCHAR(65), 
    @AmountFrom AS NUMERIC(19, 5), 
    @AmountTo AS NUMERIC(19, 5), 
	@ItemNumber AS VARCHAR(15), 
    @ItemDescr AS VARCHAR(65)
AS
BEGIN

/***************************************************************
csspSOPTransactionSearch

Stored procedure that queries all SOP transactions, with filtering
on Doc Date, Doc Number, Customer ID, and Vendor Name

Created June 23, 2018 by Ian Grieve


EXEC dbo.csspSOPTransactionSearch @StartDate = '2017-01-01',
                                  @EndDate = '2017-12-31',   
                                  @DocNumber = 'STDINV2265', 
                                  @CustomerID = 'AARON',
                                  @CustomerName = '',
								  @AmountFrom = 0,
								  @AmountTo = 0

***************************************************************/

    --If you have a large number of transaction history, adjust TOP count as needed for performance
    SELECT TOP 200
           Origin,
           RTRIM(sh.SOPNUMBE) AS DocNum,  --This field value must be named DocNum for drill down, DO NOT REMOVE
           CASE sh.SOPTYPE
               WHEN 1 THEN 'Quote'
               WHEN 2 THEN 'Order'
               WHEN 3 THEN 'Invoice'
               WHEN 4 THEN 'Return'
               WHEN 5 THEN 'Back Order'
               WHEN 6 THEN 'Fulfillment Order'
               ELSE ''
           END AS DocType,
           RTRIM(ORIGNUMB) AS OrigDocNum,
           CASE ORIGTYPE
               WHEN 1 THEN 'Quote'
               WHEN 2 THEN 'Order'
               WHEN 3 THEN 'Invoice'
               WHEN 4 THEN 'Return'
               WHEN 5 THEN 'Back Order'
               WHEN 6 THEN 'Fulfillment Order'
               ELSE ''	
           END AS OrigDocType,
           RTRIM(CUSTNMBR) AS CustomerID,  --This field value must be named CustomerID for drill down, DO NOT REMOVE
           RTRIM(CUSTNAME) AS CustomerName,
           DOCDATE AS DocDate,
           RTRIM(CSTPONBR) AS PONumber,
           RTRIM(CURNCYID) AS CurrencyID,
           SUBTOTAL AS Subtotal,
           TAXAMNT AS TaxAmount,
           DOCAMNT AS DocAmount,
           CASE VOIDSTTS
               WHEN 1 THEN 'Yes'
               ELSE 'No'
           END AS Voided, 
		   sh.SOPTYPE  --Numeric SOPTYPE value is a REQUIRED field for SOP Search to ensure unique lookups of SOP transactions, DO NOT REMOVE. Column is hidden in SOP Search grid.
    FROM
    (
        SELECT 'WORK' AS Origin, SOPNUMBE, SOPTYPE, ORIGNUMB, ORIGTYPE, CUSTNMBR, CUSTNAME, 
		DOCDATE, CSTPONBR, CURNCYID, SUBTOTAL, TAXAMNT, DOCAMNT, VOIDSTTS
        FROM dbo.SOP10100 
        
		UNION ALL

        SELECT 'HIST' AS Origin, SOPNUMBE, SOPTYPE, ORIGNUMB, ORIGTYPE, CUSTNMBR, CUSTNAME, 
		DOCDATE, CSTPONBR, CURNCYID, SUBTOTAL, TAXAMNT, DOCAMNT, VOIDSTTS
        FROM dbo.SOP30200 

    ) AS sh

	JOIN
	(
		SELECT SOPTYPE, SOPNUMBE, ITEMNMBR, ITEMDESC, QUANTITY, UNITCOST, UNITPRCE, EXTDCOST, 
		XTNDPRCE, QTYFULFI, QTYINSVC, ReqShipDate, ACTLSHIP, SLSINDX, LOCNCODE
        FROM SOP10200

        UNION ALL

        SELECT SOPTYPE, SOPNUMBE, ITEMNMBR, ITEMDESC, QUANTITY, UNITCOST, UNITPRCE, EXTDCOST,
        XTNDPRCE, QTYFULFI, QTYINSVC, ReqShipDate, ACTLSHIP, SLSINDX, LOCNCODE
        FROM SOP30300 
	) AS sl ON sl.SOPTYPE = sh.SOPTYPE AND sl.SOPNUMBE = sh.SOPNUMBE

    WHERE sh.DOCDATE >= @StartDate
          AND sh.DOCDATE <= @EndDate
          AND sh.SOPNUMBE LIKE '%' + @DocNumber + '%'
          AND sh.CUSTNMBR LIKE '%' + @CustomerID + '%'
          AND upper(sh.CUSTNAME) LIKE '%' + upper(@CustomerName) + '%'
          AND sh.DOCAMNT >= @AmountFrom
          AND sh.DOCAMNT <= @AmountTo
		  AND (sl.ITEMNMBR LIKE '%' + @ItemNumber + '%' OR sl.ITEMDESC LIKE '%' + @ItemDescr + '%')

END
GO

GRANT EXECUTE ON csspSOPTransactionSearch TO DYNGRP;
GO