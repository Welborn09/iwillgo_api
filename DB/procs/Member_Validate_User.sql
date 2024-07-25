
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Member_Validate_User 
	@Email nvarchar(500),
	@Password nvarchar(500)
AS
BEGIN
	SELECT m.*
	FROM dbo.Members m
	WHERE
		m.Email = @Email AND
		m.Password = @Password
END
GO