using System.ComponentModel.DataAnnotations;

namespace CurdApplication.Models.ViewModel
{
    public class SignUpUserViewModel
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm password")]
        [Compare("Password",ErrorMessage ="Confirm password can not matched")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter email")]

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
       
        [Required(ErrorMessage = "Please enter mobile number")]
        [Display(Name ="Mobile number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter valid mobile number.")]
        // To make nullable we use ? , to solve error The value " is invalid
        public long? Mobile { get; set; }

        [Display(Name ="Active")]
        public bool IsActive { get; set; }
    }
}
