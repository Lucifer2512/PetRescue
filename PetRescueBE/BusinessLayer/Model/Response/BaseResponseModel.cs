namespace BusinessLayer.Model.Response
{
    public class BaseResponseModel<T>
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
    }

    public class BaseResponseModel
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
       
    }

    public class TokenModel
    {
        public string? Token { get; set; }
    }
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        /*public PaginatedList(List<T> items, int pageIndex, int totalPages)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalPages = totalPages;
        }*/
    }
}
