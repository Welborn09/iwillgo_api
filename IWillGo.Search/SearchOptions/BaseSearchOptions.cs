using System.Collections.Specialized;

namespace IWillGo.Search.SearchOptions
{
    public abstract class BaseSearchOptions
    {
        public BaseSearchOptions(NameValueCollection options)
        {
            if (options != null)
            {
                LoadBaseOptions(options);
                LoadOptions(options);
            }
        }
        public BaseSearchOptions() { }
        public string SortColumn { get; set; }
        public int? SortOrder { get; set; }
        public DateTime? CreatedDate { get; set; }
        public object SqlParameters
        {
            get
            {
                var parmsObj = AddSqlParameters();
                parmsObj.SortColumn = SortColumn;
                parmsObj.SortOrder = SortOrder;
                return parmsObj;
            }
        }
        protected abstract void LoadOptions(NameValueCollection options);
        protected abstract dynamic AddSqlParameters();

        private void LoadBaseOptions(NameValueCollection options)
        {
            SortColumn = options.AllKeys.Contains("SortColumn") ? options["SortColumn"] : null;
            SortOrder = options.AllKeys.Contains("SortOrder") ? Convert.ToInt32(options["SortOrder"]) as int? : null;
            CreatedDate = options.AllKeys.Contains("CreatedDate") ? Convert.ToDateTime(options["CreatedDate"]) as DateTime? : null;
        }
    }
}