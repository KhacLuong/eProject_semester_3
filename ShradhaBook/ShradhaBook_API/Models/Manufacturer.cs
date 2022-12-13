namespace ShradhaBook_API.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public DateTime CreatAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
