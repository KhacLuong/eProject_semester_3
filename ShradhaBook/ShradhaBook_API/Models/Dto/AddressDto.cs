namespace ShradhaBook_API.Models.Dto
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        public string District { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int? Postcode { get; set; }
    }
}
