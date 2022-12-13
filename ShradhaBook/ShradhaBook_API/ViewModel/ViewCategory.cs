using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class ViewCategory
    {
        public int Id { get; set; }


        public string Code { get; set; }


        public string Name { get; set; }


        public int ParentId { get; set; }


        public int Status { get; set; }


    }
}
