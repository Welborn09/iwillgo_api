using Dapper;
using IWillGo.DataAccess.Interfaces;
using IWillGo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.DataAccess
{
    public abstract class SaveBaseRepo<T> : IDisposable, ISaveBaseRepo<T> where T : BaseModel
    {
        string sqlInsert = string.Empty;
        string sqlUpdate = string.Empty;
        protected string tableName = string.Empty;
        protected string primaryKey = string.Empty;


        public IDbConnection dbConnection { get; set; }

        private SaveBaseRepo(IDbConnection connection, string tableName, string primaryKey, string sqlInsert, string sqlUpdate) 
        { 
            this.sqlUpdate = sqlUpdate;
            this.tableName = tableName;
            this.primaryKey = primaryKey;
            this.sqlInsert = sqlInsert;
            dbConnection = connection;
        }
        public async Task SaveAsync(T model)
        {
            try
            {
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                using (var trans = dbConnection.BeginTransaction())
                {
                    await SaveAsync(model, dbConnection, trans);
                    trans.Commit();
                }

                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public async Task SaveAsync(T model, IDbConnection conn, IDbTransaction trans)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Id))
                    await Insert(model, conn, trans);
                else
                    await Update(model, conn, trans);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task Insert(T model, IDbConnection conn, IDbTransaction trans)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Id))
                    model.Id = Guid.NewGuid().ToString();
                //need these added to model for caching, don't delete!
                model.CreatedDate = DateTime.Now; //need this added to model for caching, don't delete!
                model.CreatedBy = Thread.CurrentPrincipal?.Identity?.Name != null ? Thread.CurrentPrincipal.Name : null;
                if (String.IsNullOrWhiteSpace(model.CreatedBy))
                    model.CreatedBy = "Username Not Configured";

                var parameters = LoadSaveParamsFromModel(model);
                if ((parameters as IDictionary<string, object>).ContainsKey("ModifiedBy"))
                    (parameters as IDictionary<string, object>).Remove("ModifiedBy");
                if ((parameters as IDictionary<string, object>).ContainsKey("ModifiedDate"))
                    (parameters as IDictionary<string, object>).Remove("ModifiedDate");

                if (!(parameters as IDictionary<string, object>).ContainsKey("CreatedBy"))
                    (parameters as IDictionary<string, object>).Add("CreatedBy", model.CreatedBy);
                if (!(parameters as IDictionary<string, object>).ContainsKey("CreatedDate"))
                    (parameters as IDictionary<string, object>).Add("CreatedDate", model.CreatedDate);

                await conn.ExecuteAsync(sqlInsert, parameters, trans, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task Update(T model, IDbConnection conn, IDbTransaction trans)
        {
            try
            {
                //need these added to model for caching, don't delete!
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = Thread.CurrentPrincipal?.Identity?.Name != null ? Thread.CurrentPrincipal.Identity.Name : null;

                var parameters = LoadSaveParamsFromModel(model);
                if ((parameters as IDictionary<string, object>).ContainsKey("CreatedBy"))
                    (parameters as IDictionary<string, object>).Remove("CreatedBy");
                if ((parameters as IDictionary<string, object>).ContainsKey("CreatedDate"))
                    (parameters as IDictionary<string, object>).Remove("CreatedDate");

                if (!(parameters as IDictionary<string, object>).ContainsKey("ModifiedBy"))
                    (parameters as IDictionary<string, object>).Add("ModifiedBy", model.ModifiedBy);
                if (!(parameters as IDictionary<string, object>).ContainsKey("ModifiedDate"))
                    (parameters as IDictionary<string, object>).Add("ModifiedDate", model.ModifiedDate);

                await conn.ExecuteAsync(sqlUpdate, parameters, trans, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected abstract object LoadSaveParamsFromModel(T model);
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
