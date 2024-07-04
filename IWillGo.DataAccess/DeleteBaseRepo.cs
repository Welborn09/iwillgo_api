using Dapper;
using IWillGo.DataAccess.Interfaces;
using IWillGo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.DataAccess
{
    public abstract class DeleteBaseRepo<T> : IDisposable, IDeleteBaseRepo<T> where T : BaseModel
    {
        protected string sqlDelete = string.Empty;
        protected string tableName = string.Empty;
        protected string primaryKey = string.Empty;

        public IDbConnection dbConnection { get; set; }


        private DeleteBaseRepo(IDbConnection connection, string tableName, string primaryKey, string sqlDelete) 
        {
            this.sqlDelete = sqlDelete;
            this.tableName = tableName;
            this.primaryKey = primaryKey;
            this.dbConnection = connection;
        }        

        public async Task<(IDbTransaction trans, IDbConnection conn)> Delete(string id, bool handleCommit = true)
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();

            var trans = dbConnection.BeginTransaction();

            await Delete(id, dbConnection, trans);
            await DeleteMemberObjects(new List<string>() { id }, dbConnection, trans);
            if (handleCommit)
            {
                trans.Commit();
                trans.Dispose();
            }

            if (dbConnection.State == ConnectionState.Open && handleCommit)
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
            if (handleCommit)
                return (null, null);

            return (trans, dbConnection);
        }

        public async Task Delete(string id, IDbConnection conn, IDbTransaction trans)
        {
            var sql = string.Format("Delete from {0} where {1} = '{2}'", tableName, primaryKey, id);
            await conn.ExecuteAsync(sql, transaction: trans);
        }
        protected virtual Task DeleteMemberObjects(List<string> ids, IDbConnection conn, IDbTransaction trans)
        { return Task.CompletedTask; }

        public void Dispose()
        {
            
        }
    }
}
