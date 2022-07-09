namespace EasyCommerce;

public class ApiResponse<TContent> : ApiResponse
{
    public TContent Content { get; set; }
    public object OtherContent { get; set; }
}

public class ApiResponse
{
    public string RequestModel { get; set; }
    public string ResponseStatusMessage { get; set; }
    public int ResponseStatusCode { get; set; }

}
