namespace EasyCommerce;

public static class ApiResponseExtensions
{
    public static ApiResponse<TContent> Created<TContent>(this ApiResponse<TContent> apiResponse, TContent content)
    {
        apiResponse.ResponseStatusCode = 201;
        apiResponse.ResponseStatusMessage = "Created";
        apiResponse.RequestModel = typeof(TContent).Name;
        apiResponse.Content = content;
        return apiResponse;
    }
    public static ApiResponse<TContent> NoContent<TContent>(this ApiResponse<TContent> apiResponse)
    {
        apiResponse.ResponseStatusCode = 200;
        apiResponse.ResponseStatusMessage = "No Content";
        apiResponse.RequestModel = typeof(TContent).Name;
        return apiResponse;
    }
    public static ApiResponse<TContent> Ok<TContent>(this ApiResponse<TContent> apiResponse, TContent content)
    {
        apiResponse.ResponseStatusCode = 200;
        apiResponse.ResponseStatusMessage = "Ok";
        apiResponse.RequestModel = typeof(TContent).Name;
        apiResponse.Content = content;
        return apiResponse;
    }
    public static ApiResponse<TContent> BadRequest<TContent>(this ApiResponse<TContent> apiResponse, TContent content)
    {
        apiResponse.ResponseStatusCode = 400;
        apiResponse.ResponseStatusMessage = "Bad Request";
        apiResponse.RequestModel = typeof(TContent).Name;
        apiResponse.Content = content;
        return apiResponse;
    }
    public static ApiResponse<TContent> NotFound<TContent>(this ApiResponse<TContent> apiResponse, TContent content)
    {
        apiResponse.ResponseStatusCode = 404;
        apiResponse.ResponseStatusMessage = "Not found";
        apiResponse.RequestModel = typeof(TContent).Name;
        apiResponse.Content = content;
        return apiResponse;
    }
    public static ApiResponse<TContent> InternalServerError<TContent>(this ApiResponse<TContent> apiResponse)
    {
        apiResponse.ResponseStatusCode = 500;
        apiResponse.ResponseStatusMessage = "Internal Server Error";
        apiResponse.RequestModel = typeof(TContent).Name;
        return apiResponse;
    }

    public static ApiResponse<TContent> Created<TContent>(this ApiResponse<TContent> apiResponse, TContent content, string requestModelName)
    {
        apiResponse.ResponseStatusCode = 201;
        apiResponse.ResponseStatusMessage = "Created";
        apiResponse.RequestModel = requestModelName;
        apiResponse.Content = content;
        return apiResponse;
    }
    public static ApiResponse<TContent> NoContent<TContent>(this ApiResponse<TContent> apiResponse,  string requestModelName)
    {
        apiResponse.ResponseStatusCode = 200;
        apiResponse.ResponseStatusMessage = "No Content";
        apiResponse.RequestModel = requestModelName;
        return apiResponse;
    }
    public static ApiResponse<TContent> Ok<TContent>(this ApiResponse<TContent> apiResponse, TContent content, string requestModelName)
    {
        apiResponse.ResponseStatusCode = 200;
        apiResponse.ResponseStatusMessage = "Ok";
        apiResponse.RequestModel = requestModelName;
        apiResponse.Content = content;
        return apiResponse;
    }
    public static ApiResponse<TContent> BadRequest<TContent>(this ApiResponse<TContent> apiResponse, TContent content, string requestModelName)
    {
        apiResponse.ResponseStatusCode = 400;
        apiResponse.ResponseStatusMessage = "Bad Request";
        apiResponse.RequestModel = requestModelName;
        apiResponse.Content = content;
        return apiResponse;
    }
    public static ApiResponse<TContent> NotFound<TContent>(this ApiResponse<TContent> apiResponse, TContent content, string requestModelName)
    {
        apiResponse.ResponseStatusCode = 404;
        apiResponse.ResponseStatusMessage = "Not found";
        apiResponse.RequestModel = requestModelName;
        apiResponse.Content = content;
        return apiResponse;
    }
    public static ApiResponse<TContent> InternalServerError<TContent>(this ApiResponse<TContent> apiResponse, string requestModelName)
    {
        apiResponse.ResponseStatusCode = 500;
        apiResponse.ResponseStatusMessage = "Internal Server Error";
        apiResponse.RequestModel = requestModelName;
        return apiResponse;
    }

    public static ApiResponse<TContent> BadRequestForGet<TContent>(this ApiResponse<TContent> apiResponse, object otherContent)
    {
        apiResponse.ResponseStatusCode = 400;
        apiResponse.ResponseStatusMessage = "Bad Request";
        apiResponse.RequestModel = typeof(TContent).Name;
        apiResponse.OtherContent = otherContent;
        return apiResponse;
    }
    public static ApiResponse<TContent> NotFoundForGet<TContent>(this ApiResponse<TContent> apiResponse, object otherContent)
    {
        apiResponse.ResponseStatusCode = 404;
        apiResponse.ResponseStatusMessage = "Not found";
        apiResponse.RequestModel = typeof(TContent).Name;
        apiResponse.OtherContent = otherContent;
        return apiResponse;
    }
    public static ApiResponse<TContent> NoContentForGet<TContent>(this ApiResponse<TContent> apiResponse, object otherContent)
    {
        apiResponse.ResponseStatusCode = 200;
        apiResponse.ResponseStatusMessage = "No Content";
        apiResponse.RequestModel = typeof(TContent).Name;
        apiResponse.OtherContent = otherContent;
        return apiResponse;
    }

    public static ApiResponse<TContent> OkForGet<TContent>(this ApiResponse<TContent> apiResponse, TContent content, string requestModelName)
    {
        apiResponse.ResponseStatusCode = 200;
        apiResponse.ResponseStatusMessage = "Ok";
        apiResponse.RequestModel = requestModelName;
        apiResponse.Content = content;
        return apiResponse;
    }
    public static ApiResponse<TContent> BadRequestForGet<TContent>(this ApiResponse<TContent> apiResponse, object otherContent, string requestModelName)
    {
        apiResponse.ResponseStatusCode = 400;
        apiResponse.ResponseStatusMessage = "Bad Request";
        apiResponse.RequestModel = requestModelName;
        apiResponse.OtherContent = otherContent;
        return apiResponse;
    }
    public static ApiResponse<TContent> NotFoundForGet<TContent>(this ApiResponse<TContent> apiResponse, object otherContent, string requestModelName)
    {
        apiResponse.ResponseStatusCode = 404;
        apiResponse.ResponseStatusMessage = "Not found";
        apiResponse.RequestModel = requestModelName;
        apiResponse.OtherContent = otherContent;
        return apiResponse;
    }
    public static ApiResponse<TContent> NoContentForGet<TContent>(this ApiResponse<TContent> apiResponse, object otherContent, string requestModelName)
    {
        apiResponse.ResponseStatusCode = 200;
        apiResponse.ResponseStatusMessage = "No Content";
        apiResponse.RequestModel = requestModelName;
        apiResponse.OtherContent= otherContent;
        return apiResponse;
    }
}
