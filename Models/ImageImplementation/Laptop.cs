using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CurdApplication.Models.ImageImplementation
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Description { get; set; }
        public string? Path { get; set; }
        [NotMapped]
        [Display(Name = "Choose Image")]
        public IFormFile ImagePath { get; set; }
    }
}
