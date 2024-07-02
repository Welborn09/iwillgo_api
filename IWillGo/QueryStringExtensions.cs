namespace IWillGo
{
    public static class QueryStringExtensions
    {
        public static string ToProperString(this QueryString queryString)
        {
            var returnString = queryString.ToString();
            //have to remove the ?
            if (returnString.Length > 0 && returnString.Substring(1) == "?")
                return queryString.ToString().Substring(1);
            else
                return returnString;
        }
    }
}
