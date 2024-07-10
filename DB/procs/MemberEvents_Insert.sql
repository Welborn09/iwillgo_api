
SET ANSI_NULLS ON
GO
DROP PROCEDURE IF EXISTS [dbo].[MemberEvents_Insert ]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE MemberEvents_Insert 
	@PK_MemberEvents uniqueidentifier,
	@FK_Members uniqueidentifier,
	@FK_Opportunity uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO dbo.MemberEvents(PK_MemberEvents,FK_Members, FK_Opportunity)
	VALUES(@PK_MemberEvents, @FK_Members, @FK_Opportunity)
END
GO
