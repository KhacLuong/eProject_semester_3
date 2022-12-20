using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models.Dto
{
    public class UserInfoDto
    {
        [Required, RegularExpression("^0[0-9]{9}$")]
        public string Phone { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public DateTime? DateofBirth { get; set; }
        //public AddressDto? Address { get; set; }
    }
}
