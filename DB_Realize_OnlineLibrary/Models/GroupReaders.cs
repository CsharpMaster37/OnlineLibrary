using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_Realize_OnlineLibrary.Models
{
    [Table("GroupReaders")]
    public class GroupReaders
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; } // ID 

        [Required(ErrorMessage = "Пожалуйста введите название группы")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; } // название 
    }
}
