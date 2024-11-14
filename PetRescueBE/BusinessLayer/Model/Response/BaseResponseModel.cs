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
        public int TotalCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
