﻿namespace ShradhaBook_API.Models.Response;

public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool Status { get; set; } = true;
    public string Message { get; set; } = string.Empty;
}