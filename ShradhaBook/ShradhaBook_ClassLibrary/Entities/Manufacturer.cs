using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_ClassLibrary.Entities;

public class Manufacturer
{
    public int Id { get; set; }

    [Required] public string Code { get; set; }

    [Required] public string Name { get; set; }

    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; }

    public static explicit operator Manufacturer(string v)
    {
        throw new NotImplementedException();
    }
}