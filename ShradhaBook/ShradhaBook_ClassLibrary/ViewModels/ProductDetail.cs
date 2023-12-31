﻿namespace ShradhaBook_ClassLibrary.ViewModels;

public class ProductDetail
{


    public ProductDetail()
    {
    }

    public ProductDetail(int id, string code, string name, CategoryModelGet category, decimal price, long quantity, ManufacturerModelGet manufacturer, AuthorModelGet author, string? description, string? intro, 
        string? imageProductPath, string? imageProductName, string? status, string? slug, float? star, long? viewCount, DateTime? createdAt, DateTime? updatedAt)
    {
        Id = id;
        Code = code;
        Name = name;
        Category = category;
        Price = price;
        Quantity = quantity;
        Manufacturer = manufacturer;
        Author = author;
        Description = description;
        Intro = intro;
        ImageProductPath = imageProductPath;
        ImageProductName = imageProductName;
        Status = status;
        Slug = slug;
        Star = star;
        ViewCount = viewCount;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public int Id { get; set; }
    public string Code { get; set; }

    public string Name { get; set; }

    public CategoryModelGet Category { get; set; }
    public decimal Price { get; set; }
    public long Quantity { get; set; }
    public ManufacturerModelGet Manufacturer { get; set; }
    public AuthorModelGet Author { get; set; }
    public string? Description { get; set; }
    public string? Intro { get; set; }
    public string? ImageProductPath { get; set; }
    public string? ImageProductName { get; set; }
    public string? Status { get; set; }
    public string? Slug { get; set; }
    public float? Star { get; set; }

    public long? ViewCount { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}