﻿namespace ShradhaBook_API.ViewModels
{
    public class BlogModelGet
    {
        
        
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Content { get; set; }

        public string? avatar { get; set; }

        public int? AuthorId { get; set; }

        public string? Status { get; set; }

        public string? Slug { get; set; }

        public long? viewCount { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
    }
}
    
