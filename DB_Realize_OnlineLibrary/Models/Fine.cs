using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_Realize_OnlineLibrary.Models
{
    [Table("Fines")]
    public class Fine
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReaderId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int BookId { get; set; }
        [Display(Name = "Книга")]
        public Book Book { get; set; }

        [Display(Name = "Порча книги")]
        public bool IsMess { get; set; }
        [Required]
        [Display(Name = "Дней просрока")]
        public int OverdueDays { get; set; }
        [Required]
        [Display(Name = "Сумма штрафа")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "Статус")]
        public string Status { get; set; }
    }
}
