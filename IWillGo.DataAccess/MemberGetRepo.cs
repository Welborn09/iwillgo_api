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

namespace IWillGo.DataAccess
{
    public class MemberGetRepo : GetBaseRepo<Member>, IGetMemberRepo
    {
        private readonly IServiceProvider _serviceProvider;

        public MemberGetRepo(IDbConnection dbConnection, IServiceProvider serviceProvider)
             : base(dbConnection, "Member", "PK_Member", "Member_Search", "Member_GetByIds")
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Member> LoadMember(string memberId)
        {
            return await GetAsync(memberId);
        }

        public Task<IEnumerable<Member>> LoadMembers()
        {
            throw new NotImplementedException();
        }

        /* GET METHODS */

        public override Member MapDataReaderToObject(IDataReader reader)
        {
            var ret = new Member();
            ret.Id = reader.GetGuid("PK_Member");
            ret.FirstName = reader.GetString("FirstName");
            ret.LastName = reader.GetString("LastName");
            ret.Email = reader.GetString("Email");
            return ret;
        }

        public async override Task<Member> PopulateFromReader(IDataReader reader)
        {
            var id = reader.GetGuid("PK_Member");
            Member ret = await GetAsync(id);

            ret = LoadClient(reader);
            return ret;
        }

        private Member LoadClient(IDataReader reader)
        {
            var ret = new Member();
            ret.Id = reader.GetGuid("PK_Member");
            if (reader.HasColumn("FirstName"))
                ret.FirstName = reader.GetString("FirstName");
            if (reader.HasColumn("LastName"))
                ret.LastName = reader.GetString("LastName");
            ret.Email = reader.GetString("Email");
            return ret;
        }
    }
}
