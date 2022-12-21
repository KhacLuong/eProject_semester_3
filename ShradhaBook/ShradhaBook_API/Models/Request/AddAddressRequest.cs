namespace ShradhaBook_API.Models.Request
{
    public class AddAddressRequest
    {
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        public string District { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int? Postcode { get; set; }
        public int UserInfoId { get; set; }
    }
}
