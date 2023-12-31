﻿using System.Text.Json.Serialization;

namespace ShradhaBook_ClassLibrary.Entities;

public class UserInfo
{
    public int Id { get; set; }
    public string? Avatar { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public DateTime? DateofBirth { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime? UpdateAt { get; set; }

    [JsonIgnore] public User User { get; set; } = null!;

    public int UserId { get; set; }
    public List<Address>? Addresses { get; set; }
}