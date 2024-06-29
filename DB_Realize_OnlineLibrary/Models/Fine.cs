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
        public int ReaderId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int BookId { get; set; }

        [Required]
        [Display(Name = "ФИО читателя")]
        public string NameReader { get; set; }

        [Display(Name = "Порча книги")]
        public bool IsMess { get; set; }
        [Required]
        [Display(Name = "Дней просрока")]
        public int OverdueDays { get; set; }
        [Required]
        [Display(Name = "Сумма штрафа")]
        public int Price { get; set; }
    }
}
