﻿namespace ShradhaBook_ClassLibrary.Response;

public class PaginationResponse<T> where T : class
{
    public List<T>? Data { get; set; }
    public int ItemPerPage { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
}