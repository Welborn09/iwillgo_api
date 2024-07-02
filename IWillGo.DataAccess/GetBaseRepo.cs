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

namespace IWillGo.DataAccess
{
    public abstract class GetBaseRepo<T> : IDisposable, IGetBaseRepo<T> where T : BaseModel
    {

        public IDbConnection dbConnection { get; set; }
        protected string tableName = string.Empty;
        protected string primaryKey = string.Empty;
        string sqlGet = string.Empty;

        public GetBaseRepo(IDbConnection connection, string tableName, string primaryKey, string sqlGet, string sqlGetByIds = null)
        {
            dbConnection = connection;
            this.tableName = tableName;
            this.primaryKey = primaryKey;
            this.sqlGet = sqlGet;

            if (!string.IsNullOrEmpty(sqlGetByIds))
                this.sqlGetByIds = sqlGetByIds;
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
                using (var reader = await conn.ExecuteReaderAsync(sqlGet, parms, trans, commandType: CommandType.StoredProcedure, commandTimeout: 99999))
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

        

        protected virtual void PopulateBaseFromReader(T item, IDataReader reader)
        {
            if (reader.HasColumn("CreatedBy"))
                item.CreatedBy =  reader.GetString("CreatedBy");
            if (reader.HasColumn("CreatedDate"))
                item.CreatedDate = reader.GetNullableDate("CreatedDate");
            if (reader.HasColumn("ModifiedBy"))
                item.ModifiedBy = reader.GetString("ModifiedBy");
            if (reader.HasColumn("ModifiedDate"))
                item.ModifiedDate = reader.GetNullableDate("ModifiedDate");
        }        

        public abstract Task<T> PopulateFromReader(IDataReader reader);
        public abstract T MapDataReaderToObject(IDataReader reader);

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
