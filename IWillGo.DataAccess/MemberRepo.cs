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

namespace IWillGo.DataAccess
{
    public class MemberRepo : IMemberRepo
    {
        private readonly IServiceProvider _serviceProvider;


        public MemberRepo(IDbConnection dbConnection, IServiceProvider serviceProvider) 
        {

            _serviceProvider = serviceProvider;
        }

        /* GET METHODS */
        public Task<(IEnumerable<Member> items, int totalCount)> GetAsync(BaseSearchOptions options, IDbConnection conn = null, IDbTransaction trans = null)
        {
            throw new NotImplementedException();
        }

        public Task<Member> GetAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Member> GetAsync(IDataReader reader)
        {
            throw new NotImplementedException();
        }

        public Task<List<Member>> GetAsync(IEnumerable<string> ids, IDbConnection conn = null, IDbTransaction trans = null)
        {
            throw new NotImplementedException();
        }

        /* SAVE METHODS */
        public Task SaveAsync(Member model)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Member model, IDbConnection conn, IDbTransaction trans)
        {
            throw new NotImplementedException();
        }

        /* DELETE METHODS */
        public Task<(IDbTransaction trans, IDbConnection conn)> Delete(string id, bool handleCommit = true)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id, IDbConnection conn, IDbTransaction trans)
        {
            throw new NotImplementedException();
        }

        public Member MapDataReaderToObject(IDataReader reader)
        {
            var ret = new Member();
            ret.Id = reader.GetGuid("PK_Member");
            ret.FirstName = reader.GetString("FirstName");
            ret.LastName = reader.GetString("LastName");
            ret.Email = reader.GetString("Email");
            return ret;
        }

        public Task<Member> PopulateFromReader(IDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
