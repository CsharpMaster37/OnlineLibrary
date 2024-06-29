using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_Realize_OnlineLibrary.Models
{
    [Table("Readers")]
    public class Reader : IdentityUser
    {
        [Required(ErrorMessage = "Пожалуйста введите ФИО пользователя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "ФИО")]
        public string Name { get; set; }

        [Range(typeof(DateTime), "1/1/1900", "1/1/2023", ErrorMessage = "Недопустимая дата")]
        [Display(Name = "Дата рождения")]
        public DateTime? DateofBirth { get; set; }

        [Display(Name = "Номер телефона")]
        public string? Phone {  get; set; }
    }
}
