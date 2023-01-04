using System.Text.Json.Serialization;

namespace ShradhaBook_API.Models.Entities;

public class Address
{
    public int Id { get; set; }
    public string AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int? Postcode { get; set; }
    public string Country { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime? UpdateAt { get; set; }

    [JsonIgnore] public UserInfo UserInfo { get; set; } = null!;

    public int UserInfoId { get; set; }
}