using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DB_Realize_OnlineLibrary.Models
{
    [Table("History")]
    public class HistoryItem
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ReaderId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int BookId { get; set; }

        [Display(Name = "Дата взятия")]
        public DateTime DateofCapture { get; set; }

        [Display(Name = "Дата сдачи")]
        public DateTime? DateofDelivery { get; set; }
        [Display(Name = "Ожидаемая дата сдачи")]
        public DateTime? ExpectedDateOfDelivery { get; set; }
        [Required]
        [Display(Name = "ФИО читателя")]
        public string NameReader { get; set; }
    }
}
