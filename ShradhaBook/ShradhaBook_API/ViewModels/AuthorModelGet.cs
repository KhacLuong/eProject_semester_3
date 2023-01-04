namespace ShradhaBook_API.ViewModels;

public class AuthorModelGet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}