using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DB_Realize_OnlineLibrary.Models
{
    public class Publisher
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите название издательства")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}
