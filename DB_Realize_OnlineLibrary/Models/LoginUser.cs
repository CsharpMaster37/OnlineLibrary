using System.ComponentModel.DataAnnotations;

namespace DB_Realize_OnlineLibrary.Models
{
    public class LoginUser
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
