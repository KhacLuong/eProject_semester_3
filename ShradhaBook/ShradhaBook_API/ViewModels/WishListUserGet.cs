﻿using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class WishListUserGet
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? ProductName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateedAt { get; set; }

        public WishListUserGet(int id, string? userNme, string? productName, DateTime? createdAt, DateTime? updateedAt)
        {
            Id = id;
            UserName = userNme;
            ProductName = productName;
            CreatedAt = createdAt;
            UpdateedAt = updateedAt;
        }

        public WishListUserGet()
        {
        }
    }
}
