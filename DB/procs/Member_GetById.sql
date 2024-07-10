

CREATE PROCEDURE [dbo].[Member_GetById]
	@MemberId uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.Members
	WHERE PK_Members = @MemberId
END
GO