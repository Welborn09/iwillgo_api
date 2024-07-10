
CREATE PROCEDURE [dbo].[Member_Search_GetIds]
	@MemberId uniqueidentifier = null,
	@FirstName varchar(100) = null,
	@LastName varchar(20) = null,
	@Email nvarchar(500) = null,
	@Active bit = null,
	@Offset int = 0,
	@PageSize INT = null,
	@SortColumn VARCHAR(50) = NULL,
	@SortOrder BIT = null
AS
BEGIN
IF(@PageSize IS NULL)
BEGIN
	SELECT PK_Members, recordCount FROM Members
	CROSS JOIN (SELECT Count(*) AS recordCount FROM Members) AS searchRecordCount
END
ELSE
	WITH orderSearch_cte AS(
		SELECT [c].[PK_Members]
			  ,[c].[FirstName]
			  ,[c].[LastName]
			  ,[c].[Email]
			  ,[c].[City]
			  ,[c].[State]
			  ,[c].[Zip]
			  ,[c].[CreatedBy]
			  ,[c].[CreatedDate]
			  ,[c].[ModifiedBy]
			  ,[c].[ModifiedDate]
			  ,[c].[Active]		
		FROM  [Members] c	
		WHERE
			([c].[PK_Members] = @MemberId or @MemberId is null) AND
			([c].[FirstName] = @FirstName or @FirstName is null) AND
			([c].[LastName] = @LastName or @LastName is null) AND
			([c].[Email] = @Email or @Email is null)			
	)
	SELECT [PK_Members], recordCount FROM orderSearch_cte
	CROSS JOIN (SELECT Count(*) AS recordCount FROM orderSearch_cte) AS searchRecordCount
	ORDER BY 
		CASE WHEN @SortOrder IS null AND @SortColumn IS null then PK_Members END ASC,

		PK_Members
	offset @Offset rows
	Fetch next @pagesize rows only
END