using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть модель авто")]
        [StringLength(50, ErrorMessage = "Модель не може перевищувати 50 символів")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть колір авто")]
        public string Color { get; set; } = string.Empty;

        [Range(1900, 2100, ErrorMessage = "Рік має бути в діапазоні 1900–2100")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Введіть тип кузова")]
        [Display(Name = "Тип кузова")]
        public string BodyType { get; set; } = string.Empty;

        [StringLength(255)]
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
