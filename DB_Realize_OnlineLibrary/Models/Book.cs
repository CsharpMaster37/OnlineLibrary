using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DB_Realize_OnlineLibrary.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }


        [Required(ErrorMessage = "Пожалуйста введите название")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 50 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите имя автора")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 50 символов")]
        [Display(Name = "ФИО автора")]
        public string Author { get; set; }

        [Required]
        [Range(0, 50000, ErrorMessage = "Недопустимая цена")]
        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Required]
        [Range(1895, 2024, ErrorMessage = "Недопустимый год")]
        [Display(Name = "Год выпуска")]
        public int Year { get; set; }

        [ForeignKey("GenreId")]
        [HiddenInput(DisplayValue = false)]
        public int? GenreId { get; set; }

        [ForeignKey("PublisherId")]
        [HiddenInput(DisplayValue = false)]
        public int? PublisherId { get; set; }

        [Display(Name = "Жанр")]
        public Genre Genre { get; set; }
        [Display(Name = "Издательство")]
        public Publisher Publisher { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 50 символов")]
        [Display(Name = "Состояние")]
        public string Condition { get; set; }

        [Required]
        [Range(0, 50000, ErrorMessage = "Недопустимое кол-во")]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string? ImageUrl { get; set; }
    }
}
