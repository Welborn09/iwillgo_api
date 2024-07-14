DROP PROCEDURE IF EXISTS [dbo].[Member_GetById]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Member_GetById]	
	@MemberId uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.Members
	WHERE PK_Members = @MemberId
END
GO