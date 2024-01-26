using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace CurdApplication.Models.ViewModel
{
    public class ImageCreateModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter name")]
        public string Name { get; set; }

        //IFormFile is an interface in ASP.NET Core that represents a file that has been uploaded with an HTTP request.
        //IFormFile is typically used in an ASP.NET Core controller to handle file uploads:

        [Required(ErrorMessage ="Choose Image/File")]
        [Display(Name="Choose Image")]
        public IFormFile ImagePath { get; set; }
    }
}
