using System.Text.Json.Serialization;

namespace ShradhaBook_API.Models.Db
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        public string District { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int? Postcode { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        [JsonIgnore]
        public UserInfo? UserInfo { get; set; }
        public int UserInfoId { get; set; }

    }
}
