namespace ShradhaBook_API.ViewModels
{
    public class ViewProduct
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public int Category_id { get; set; }
        public decimal? Price { get; set; }
        public long? Quantity { get; set; }
        public int? ManufacturerId { get; set; }
        public string Description { get; set; }
        public string Intro { get; set; }
        public string FeatureImagePath { get; set; }
        public string FeatureImagePathName { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

    }

}
