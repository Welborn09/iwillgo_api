using IWillGo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWillGo.Model;

namespace IWillGo.DataAccess.Interfaces
{
    public interface ISaveBaseRepo<T> where T : BaseModel
    {
        Task SaveAsync(T model);
        Task SaveAsync(T model, IDbConnection conn, IDbTransaction trans);
    }
}
