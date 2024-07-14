using IWillGo.Model;
using System.Data;
using IWillGo.Search.SearchOptions;

namespace IWillGo.DataAccess.Interfaces
{
    public interface IGetBaseRepo<T> where T : BaseModel
    {
        Task<(IEnumerable<T> items, int totalCount)> GetAsync(BaseSearchOptions parms, IDbConnection conn = null, IDbTransaction trans = null);
        
        Task<List<T>> GetAsync(string id, IDbConnection conn = null, IDbTransaction trans = null);
        Task<T> PopulateFromReader(IDataReader reader);
    }
}