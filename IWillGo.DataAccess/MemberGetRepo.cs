using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IWillGo.Model;
using IWillGo.Search.SearchOptions;
using IWillGo.DataAccess.Interfaces;
using IWillGo.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Specialized;
using Dapper;

namespace IWillGo.DataAccess
{
    public class MemberGetRepo : GetBaseRepo<Member>, IGetMemberRepo
    {
        private readonly IServiceProvider _serviceProvider;

        public MemberGetRepo(IDbConnection dbConnection, IServiceProvider serviceProvider)
             : base(dbConnection, "Member", "PK_Members", "Member_Search_GetIds", "Member_GetById")
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Member> LoadMember(string memberId)
        {
            return GetAsync(memberId).Result.FirstOrDefault();
        }

        public Task<IEnumerable<Member>> LoadMembers()
        {
            throw new NotImplementedException();
        }

        /* GET METHODS */
        public async Task<int> GetMemberCount(string eventId)
        {
            {
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                int ret = 0;
                var parms = new { @EventId = eventId };
                using (var reader = await dbConnection.ExecuteReaderAsync("MemberEvents_GetMembersCount", parms, commandType: CommandType.StoredProcedure))
                {

                    if (reader.Read())
                    {
                        ret = reader.GetInt("MemberCount");
                    }
                };

                return ret;
            }
        }

        public override Member MapDataReaderToObject(IDataReader reader)
        {
            var ret = new Member();
            ret.Id = reader.GetGuid("PK_Members");
            ret.FirstName = reader.GetString("FirstName");
            ret.LastName = reader.GetString("LastName");
            ret.Email = reader.GetString("Email");
            ret.City = reader.GetString("City");
            ret.State = reader.GetString("State");
            ret.Zip = reader.GetString("Zip");            
            return ret;
        }

        public async override Task<Member> PopulateFromReader(IDataReader reader)
        {
            var ret = MapDataReaderToObject(reader);
            return ret;
        }
    }
}
