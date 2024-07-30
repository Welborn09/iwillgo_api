
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Opportunity_MyEvent_Search
	@MemberId uniqueidentifier = null
AS
BEGIN
WITH orderSearch_cte AS(	
		SELECT [o].* 
		FROM dbo.MemberEvents me
		JOIN dbo.Members m on m.PK_Members = me.FK_Members
		JOIN dbo.Opportunity o on o.PK_Opportunity = me.FK_Opportunity
		WHERE
		([m].[PK_Members] = @MemberId or @MemberId is null)	
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
