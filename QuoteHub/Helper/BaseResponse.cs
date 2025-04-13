using QuoteHub.Enums;

namespace QuoteHub.Helper;

public class BaseResponse
{
    public BaseResponse(ResponseStatus status) => Status = status;
    public ResponseStatus Status { get; set; }
    public string? Message { get; set; }
    public string? StackTrace { get; set; }
    public Dictionary<string, List<string>>? Errors { get; set; }
}

public class BaseResponse<T> : BaseResponse
{
    public T? Data { get; set; }

    public BaseResponse(ResponseStatus status) : base(status)
    {
    }
}