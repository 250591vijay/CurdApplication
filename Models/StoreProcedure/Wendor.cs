using System.ComponentModel.DataAnnotations;

namespace CurdApplication.Models.StoreProcedure
{
    public class Wendor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public int PinCode { get; set; }
    }
}
