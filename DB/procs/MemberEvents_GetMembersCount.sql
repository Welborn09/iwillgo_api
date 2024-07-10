
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE MemberEvents_GetMembersCount 
	@EventId uniqueidentifier
AS
BEGIN
	SELECT Count(FK_Members) AS MemberCount 
	FROM dbo.MemberEvents
	WHERE FK_Opportunity = @EventId
END
GO
