
DROP Procedure dbo.Opportunity_GetALL
GO

CREATE PROCEDURE [dbo].[Opportunity_GetAll]
	@Address nvarchar(500) = null,
	@City varchar(500) = null,
	@State varchar(500) = null,
	@Zip varchar(10) = null,
	@EventDate DateTime = null,
	@HostId uniqueidentifier = null,
	@Active bit = null
AS
BEGIN
WITH orderSearch_cte AS(
	SELECT * FROM dbo.Opportunity o
	WHERE
	([o].[Address] = @Address or @Address is null) AND
	([o].[City] = @City or @City is null) AND
	([o].[State] = @State or @State is null) AND
	([o].[Zip] = @Zip or @Zip is null) AND
	([o].[EventDate] = @EventDate or @EventDate is null) AND
	([o].[Host_UserId] = @HostId or @HostId is null) AND
	([o].[Active] = @Active or @Active is null)
)
SELECT *, recordCount FROM orderSearch_cte
CROSS JOIN (SELECT Count(*) AS recordCount FROM orderSearch_cte) AS searchRecordCount

/* THIS WILL BE FOR PAGING IF/WHEN WE NEED IT */
/*order by 
	CASE WHEN @SortOrder IS null AND @SortColumn IS null then PK_Opportunity END ASC,
	PK_Opportunity
offset @Offset rows
Fetch next @pagesize rows only */

END
GO