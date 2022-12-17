namespace ShradhaBook_API.ViewModels
{
    public class ProductThumbnailModel
    {
        public int Id { get; set; }
        public string ThumbnailName { get; set; }
        public string ThumbnailPath { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public int ProductId { get; set; }


    }
}
