using System.ComponentModel.DataAnnotations;

namespace DB_Realize_OnlineLibrary.Models
{
    public class RegisterUser
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }
        [Display(Name = "Имя")]
        public string? Name { get; set; }
        [Range(typeof(DateTime), "1/1/1900", "1/1/2023", ErrorMessage = "Недопустимая дата")]
        [Display(Name = "Дата рождения")]
        public DateTime? DateofBirth { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
    }
}
