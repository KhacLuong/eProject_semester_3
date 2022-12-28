using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class ProductTagGet
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }

        public string? TagName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ProductTagGet(int id, string? productName, string? tagName, DateTime? createdDate, DateTime? updatedAt)
        {
            Id = id;
            ProductName = productName;
            TagName = tagName;
            CreatedDate = createdDate;
            UpdatedAt = updatedAt;
        }

        public ProductTagGet()
        {
        }
    }
}
