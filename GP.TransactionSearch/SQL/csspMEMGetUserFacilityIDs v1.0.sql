IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'csspMEMGetUserFacilityIDs')
DROP PROCEDURE csspMEMGetUserFacilityIDs
GO

CREATE PROCEDURE csspMEMGetUserFacilityIDs
	
	@SegmentNumber AS INT

AS
BEGIN

/***************************************************************
csspMEMGetUserFacilityIDs

Stored procedure that queries GL account segment values that the user
is permitted to access as Binary Stream MEM entities / facility IDs

Created June 26, 2019 by Steve Endow

EXEC dbo.csspMEMGetUserFacilityIDs @SegmentNumber = 1

***************************************************************/

	DECLARE @cur_user VARCHAR(20)

	--CURRENT_USER and related functions will return 'dbo' for logins with sysadmin/owner access
	SELECT @cur_user = CURRENT_USER;
	IF @cur_user = 'dbo'
		SET @cur_user = 'sa'

	SELECT RTRIM(gls.SGMNTID) AS FacilityID, RTRIM(gls.DSCRIPTN) AS DSCRIPTN 
	FROM GL40200 gls
	JOIN B3900200 mema ON (mema.BSSI_Facility_ID = gls.SGMNTID AND mema.USERID = @cur_user)
	WHERE gls.SGMTNUMB = @SegmentNumber ORDER BY gls.SGMNTID

END
GO



GRANT EXEC ON csspMEMGetUserFacilityIDs TO DYNGRP
GO 