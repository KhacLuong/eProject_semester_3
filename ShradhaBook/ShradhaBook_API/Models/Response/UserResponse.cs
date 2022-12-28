namespace ShradhaBook_API.Models.Response
{
    public class UserResponse
    {
        public List<User> Users { get; set; } = new List<User>();
        public int UserPerPage { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
