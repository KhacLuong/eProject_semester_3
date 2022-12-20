namespace ShradhaBook_API.Models.Dto
{
    public class UserInfoDto
    {
        public string Phone { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public DateTime? DateofBirth { get; set; }
        public AddressDto? Address { get; set; }
    }
}
