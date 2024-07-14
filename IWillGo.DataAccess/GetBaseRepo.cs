using IWillGo.DataAccess.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWillGo.Model;
using IWillGo.Search.SearchOptions;
using System.Data;
using System.Xml.Serialization;
using System.Reflection.PortableExecutable;
using IWillGo.DataAccess;
using System.IO;
using System.Diagnostics;

namespace IWillGo.DataAccess
{
    public abstract class GetBaseRepo<T> : IDisposable, IGetBaseRepo<T> where T : BaseModel
    {

        public IDbConnection dbConnection { get; set; }
        protected string tableName = string.Empty;
        protected string primaryKey = string.Empty;
        string sqlGet = string.Empty;
        string sqlGetByIds = string.Empty;

        public GetBaseRepo(IDbConnection connection, string tableName, string primaryKey, string sqlGet, string sqlGetByIds = null)
        {
            dbConnection = connection;
            this.tableName = tableName;
            this.primaryKey = primaryKey;
            this.sqlGet = sqlGet;

            if (sqlGetByIds != null) { 
                this.sqlGetByIds = sqlGetByIds;
            }

        }

        public virtual async Task<(IEnumerable<T> items, int totalCount)> GetAsync(BaseSearchOptions parms, IDbConnection conn = null, IDbTransaction trans = null)
        {
            try
            {
                if (conn == null)
                    conn = dbConnection;

                if (conn.State == ConnectionState.Closed)
                    conn.Open();


                var items = new List<T>();
                using (var reader = await conn.ExecuteReaderAsync(sqlGet, parms.SqlParameters, trans, commandType: CommandType.StoredProcedure, commandTimeout: 99999))
                {
                    while (reader.Read())
                    {
                        // Assuming you have a method to map the data reader to your object
                        T obj = MapDataReaderToObject(reader);
                        items.Add(obj);
                    }
                }

                return (items, items.Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public async Task<List<T>> GetAsync(string Id, IDbConnection conn = null, IDbTransaction trans = null)
        {
            try
            {

                if (conn == null)
                    conn = dbConnection;
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var ret = new List<T>();
                var parms = new { @MemberId = Id };
                using (var reader = await conn.ExecuteReaderAsync(sqlGetByIds, parms, trans, commandType: CommandType.StoredProcedure))
                {
                    ret.AddRange(await ProcessReader(reader));
                };

                return ret;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public async Task<List<T>> GetAsync(IEnumerable<string> ids, IDbConnection conn = null, IDbTransaction trans = null)
        {            
            try
            {

                if (conn == null)
                    conn = dbConnection;
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var ret = new List<T>();
                var parms = new { @Ids = ids.ToList().ToSqlXml() };
                using (var reader = await conn.ExecuteReaderAsync(sqlGetByIds, parms, trans, commandType: CommandType.StoredProcedure))
                {
                    ret.AddRange(await ProcessReader(reader));
                };

                return ret;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }        

        protected virtual void PopulateBaseFromReader(T item, IDataReader reader)
        {
            if (reader.HasColumn("CreatedBy"))
                item.CreatedBy =  reader.GetGuid("CreatedBy");
            if (reader.HasColumn("CreatedDate"))
                item.CreatedDate = reader.GetNullableDate("CreatedDate");
            if (reader.HasColumn("ModifiedBy"))
                item.ModifiedBy = reader.GetGuid("ModifiedBy");
            if (reader.HasColumn("ModifiedDate"))
                item.ModifiedDate = reader.GetNullableDate("ModifiedDate");
        }

        private async Task<List<T>> ProcessReader(IDataReader reader)
        {
            var ret = new List<T>();
            T lastItem = null;
            while (reader.Read())
            {
                var item = await PopulateFromReader(reader);
                PopulateBaseFromReader(item, reader);
                if (lastItem == null || item.Id != lastItem.Id)
                    ret.Add(item);
            }
            return ret;
        }

        public abstract Task<T> PopulateFromReader(IDataReader reader);
        public abstract T MapDataReaderToObject(IDataReader reader);

        public void Dispose()
        {
            
        }
    }
}
