namespace EasyCommerce
{
    public class ApiResponse<TContent> : ApiResponse
    {
        public TContent Content { get; set; }
    }

    public class ApiResponse
    {
        public string RequestModel { get; set; }
        public string ResponseStatus { get; set; }

    }
}
