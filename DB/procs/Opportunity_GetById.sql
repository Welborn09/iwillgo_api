



CREATE PROCEDURE Opportunity_GetById 
	@EventId uniqueidentifier
AS
SELECT
	o.*, 
	(SELECT Count(FK_Members) from MemberEvents me
     WHERE me.FK_Opportunity = o.PK_Opportunity) AS MemberCount	   	   
FROM 
	dbo.Opportunity o
