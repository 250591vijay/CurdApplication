using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurdApplication.Models.ImageImplementation
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
 
        public string? Path { get; set; }
        [NotMapped]
        // To get local path for uploading image we use IFormFile
        [Display(Name ="Choose Image")]
        public IFormFile ImagePath { get; set; }
    }
}
