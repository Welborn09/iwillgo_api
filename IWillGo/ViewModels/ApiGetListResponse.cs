namespace IWillGo.ViewModels
{
    public class ApiGetListResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
    }
}
