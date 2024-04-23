using System.ComponentModel.DataAnnotations;

namespace CurdApplication.Models.Cascade
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; } =default;
    }
}
