namespace ShradhaBook_API.ViewModels
{
    public class TagModelGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? CreatedAt { get; set; }

        public string? UpdatedAt { get; set; }

        public TagModelGet(int id, string name, string? createdAt, string? updatedAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public TagModelGet()
        {
        }
    }


}
