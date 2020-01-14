using System.ComponentModel.DataAnnotations;

namespace Boven.Areas.Identity.Models
{
    //LoginModel
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Du måste ange ett namn")]
        [Display(Name = "Användarnamn:")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Du måste ange ett lösenord")]
        [Display(Name = "Lösenord:")]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}


//public string Username { get; set; }

//[DataType(DataType.Password)]
//public string Password { get; set; }

//[Display(Name = "Remember Me")]
//public bool RememberMe { get; set; }

//public string ReturnUrl { get; set; }

