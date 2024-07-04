using IWillGo.DataAccess.Interfaces;
using IWillGo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.DataAccess
{
    public class MemberSaveRepo : SaveBaseRepo<Member>, ISaveMemberRepo
    {
        private readonly IServiceProvider _serviceProvider;

        public MemberSaveRepo(IDbConnection dbConnection, IServiceProvider serviceProvider)
             : base(dbConnection, "Member", "PK_Member", "Member_Insert", "Member_Update")
        {
            _serviceProvider = serviceProvider;
        }

        protected override object LoadSaveParamsFromModel(Member model)
        {
            dynamic expando = new ExpandoObject();
            expando.PK_Member = model.Id;
            expando.FirstName = model.FirstName;
            expando.LastName = model.LastName;
            expando.Email = model.Email;
            expando.City = model.City;
            expando.State = model.State;
            expando.Zip = model.Zip;
            return expando;
        }
    }
}
