using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWillGo.DataAccess.Interfaces;
using IWillGo.Model;

namespace IWillGo.DataAccess.Interfaces
{
    public interface IMemberRepo : IGetBaseRepo<Member>, ISaveBaseRepo<Member>, IDeleteBaseRepo<Member>
    {

    }
}
