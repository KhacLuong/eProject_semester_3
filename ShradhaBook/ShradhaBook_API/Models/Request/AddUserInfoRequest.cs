namespace ShradhaBook_API.Models.Request
{
    public class AddUserInfoRequest
    {
        public string? Avatar { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public DateTime? DateofBirth { get; set; }
        public int UserId { get; set; }
    }
}
