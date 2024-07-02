using IWillGo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.DataAccess.Interfaces
{
    public interface IDeleteBaseRepo<T> where T : BaseModel
    {        
        Task<(IDbTransaction trans, IDbConnection conn)> Delete(string id, bool handleCommit = true);
        Task Delete(string id, IDbConnection conn, IDbTransaction trans);

    }
}
