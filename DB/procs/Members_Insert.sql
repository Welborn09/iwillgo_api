
SET ANSI_NULLS ON
GO
DROP PROCEDURE IF EXISTS [dbo].[Members_Insert ]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE Members_Insert 
	@PK_Members uniqueidentifier,
	@FirstName varchar(50),
	@LastName varchar(100),
	@Email nvarchar(500),
	@Password nvarchar(MAX),
	@City varchar(500) = null,
	@State varchar(500) = null,
	@Zip nvarchar(10) = null,
	@CreatedBy VARCHAR(100) = null,
	@CreatedDate datetime = null
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO dbo.Members(PK_Members, FirstName, LastName, Email, Password, Confirm, City, State, Zip, CreatedDate, CreatedBy, Active)
	VALUES(@PK_Members, @FirstName, @LastName, @Email, @Password, 1, @City, @State, @Zip, @CreatedDate, @CreatedBy, 1)
END
GO
