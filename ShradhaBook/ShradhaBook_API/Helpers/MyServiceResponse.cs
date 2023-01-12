namespace ShradhaBook_API.Helpers;

public class MyServiceResponse<T>
{
    public MyServiceResponse(T? data, bool? status, string? message)
    {
        Data = data;
        Status = status;
        Message = message;
    }

    public MyServiceResponse(T? data)
    {
        Data = data;
    }

    public MyServiceResponse(bool? status, string? message)
    {
        Status = status;
        Message = message;
    }

    public T? Data { get; set; }
    public bool? Status { get; set; } = true;
    public string? Message { get; set; } = string.Empty;
}