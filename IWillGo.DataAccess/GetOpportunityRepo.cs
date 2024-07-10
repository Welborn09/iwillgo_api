using Dapper;
using IWillGo.DataAccess.Interfaces;
using IWillGo.Model;
using IWillGo.Search.SearchOptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.DataAccess
{
    public class GetOpportunityRepo : GetBaseRepo<Opportunity>, IGetOpportunityRepo
    {

        public GetOpportunityRepo(IDbConnection dbConnection)
            : base(dbConnection, "Opportunity", "PK_Opportunity", "Opportunity_Search", "Opportunity_GetById")
        {
        
        }

        public async Task<Opportunity> GetById(string eventId)
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();

            var ret = new Opportunity();
            var parms = new { @EventId = eventId };
            using (var reader = await dbConnection.ExecuteReaderAsync("Opportunity_GetById", parms, commandType: CommandType.StoredProcedure))
            {
                if (reader.Read())
                {
                    ret = MapDataReaderToObject(reader);
                }                
            };

            return ret;
        }

        public override Opportunity MapDataReaderToObject(IDataReader reader)
        {
            var ret = new Opportunity();
            ret.EventId = reader.GetGuid("PK_Opportunity");
            ret.EventName = reader.GetString("EventName");
            ret.EventDate = reader.GetDate("EventDate").ToString();
            ret.EventTimeFrom = reader.GetString("EventTimeFrom");
            ret.EventTimeTo = reader.GetString("EventTimeTo");
            ret.City = reader.GetString("City");
            ret.State = reader.GetString("State");
            ret.Zip = reader.GetString("Zip");
            ret.Description = reader.GetString("Description");
            ret.Active = reader.GetBoolean("Active") ? "true" : "false";
            if (reader.HasColumn("MemberCount"))
                ret.MemberCount = reader.GetInt("MemberCount");
            return ret;
        }

        public async override Task<Opportunity> PopulateFromReader(IDataReader reader)
        {
            var id = reader.GetGuid("PK_Opportunity");
            Opportunity ret = await GetAsync(id);

            ret = MapDataReaderToObject(reader);
            return ret;
        }
    }
}
