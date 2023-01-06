namespace ShradhaBook_ClassLibrary.ViewModels;

public class ProductTagPost
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public int TagId { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}