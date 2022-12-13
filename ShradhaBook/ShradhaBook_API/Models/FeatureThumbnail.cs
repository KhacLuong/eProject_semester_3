namespace ShradhaBook_API.Models
{
    public class FeatureThumbnail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ThumbnailImagePath { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

    }
}
