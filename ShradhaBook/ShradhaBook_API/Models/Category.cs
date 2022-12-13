using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class Category
    {
        public int Id { get; set; }


        public string Code { get; set; }


        public string Name { get; set; }


        public int ParentId { get; set; }


        public int Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }


    }
}
