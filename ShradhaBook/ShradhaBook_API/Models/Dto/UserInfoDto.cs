namespace ShradhaBook_API.Models.Dto;

public class UserInfoDto
{
    public int Id { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public DateTime? DateofBirth { get; set; }
    public List<AddressDto>? Addresses { get; set; }
}