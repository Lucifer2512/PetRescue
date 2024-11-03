namespace Pages.Model
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
}
