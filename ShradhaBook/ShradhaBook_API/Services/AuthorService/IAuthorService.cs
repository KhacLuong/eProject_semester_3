﻿namespace ShradhaBook_API.Services.AuthorService;

public interface IAuthorService
{
    Task<object> GetAllAuthorAsync(string? name, string? phone, int? sortBy = 0, int pageSize = 20, int pageIndex = 1);
    Task<AuthorModelGet> GetAuthorAsync(int id);
    Task<int> AddAuthorAsync(AuthorModelPost model);
    Task DeleteAuthorAsync(int id);
    Task<int> UpdateAuthorAsync(int id, AuthorModelPost model);
    Task<List<AuthorModelGet>> GetAllAuthorAsync();
}