﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IWillGo.Search.SearchOptions
{
    public class MemberSearchOptions : BaseSearchOptions
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MemberId { get; set; }
        public string Email { get; set; }
        public string Active { get; set; }



        public MemberSearchOptions(NameValueCollection options) : base(options)
        { }

        public MemberSearchOptions() { }

        protected override dynamic AddSqlParameters()
        {
            dynamic val = new ExpandoObject();
            val.MemberId = MemberId;
            val.Email = Email;
            val.FirstName = FirstName;
            val.LastName = LastName;
            val.Active = Active;
            val.Offset = PageNumber != null ? (PageNumber - 1) * PageSize : 0;
            val.PageSize = PageSize != null ? PageSize : 9999999;
            return val;
        }

        protected override void LoadOptions(NameValueCollection options)
        {
            PageNumber = options.AllKeys.Contains("PageNumber") ? Convert.ToInt32(options["PageNumber"]) : null;
            PageSize = options.AllKeys.Contains("PageSize") ? Convert.ToInt32(options["PageSize"]) : null;
            FirstName = options.AllKeys.Contains("FirstName") ? options["FirstName"] : null;
            LastName = options.AllKeys.Contains("LastName") ? options["LastName"] : null;
            MemberId = options.AllKeys.Contains("MemberId") ? options["MemberId"] : null;
            Email = options.AllKeys.Contains("Email") ? options["Email"] : null;
            Active = options.AllKeys.Contains("Active") ? options["Active"] : null;
        }
    }
}
