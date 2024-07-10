using IWillGo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.DataAccess.Interfaces
{
    public interface IGetOpportunityRepo : IGetBaseRepo<Opportunity>
    {
        Task<Opportunity> GetById(string eventId);
    }
}
