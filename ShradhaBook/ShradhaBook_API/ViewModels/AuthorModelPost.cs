﻿namespace ShradhaBook_API.ViewModels;

public class AuthorModelPost
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }

    public string? Phone { get; set; }
    //public DateTime? CreatedAt { get; set; }
    //public DateTime? UpdatedAt { get; set; }
}