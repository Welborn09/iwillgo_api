DROP PROCEDURE IF EXISTS [dbo].[Opportunity_Search]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Opportunity_Search
	@EventId uniqueidentifier = null,
	@MemberId uniqueidentifier = null,
	@City varchar(500) = null,
	@State varchar(500) = null,
	@Zip varchar(10) = null,
	@EventDateFrom varchar(50) = null,
	@EventDateTo varchar(50) = null,
	@HostId uniqueidentifier = null,
	@Active bit = null,
	@Offset int,
	@PageSize INT,
	@SortColumn VARCHAR(50) = NULL,
	@SortOrder BIT = null
AS
BEGIN
WITH orderSearch_cte AS(	
		SELECT * FROM dbo.Opportunity o
		WHERE
		([o].[PK_Opportunity] = @EventId or @EventId is null) AND
		([o].[Host_UserId] = @HostId or @HostId is null) AND
		([o].[City] = @City or @City is null) AND
		([o].[State] = @State or @State is null) AND
		([o].[Zip] = @Zip or @Zip is null) AND
		([o].[EventDate] >= @EventDateFrom or @EventDateFrom is null) and
		([o].[EventDate] <= @EventDateTo or @EventDateTo is null) AND
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
