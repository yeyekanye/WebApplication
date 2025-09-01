using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;



namespace WebApplication.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть модель авто")]
        [StringLength(50)]
        public string Model { get; set; }

        [Required(ErrorMessage = "Введіть колір авто")]
        public string Color { get; set; }

        [Range(1900, 2100)]
        public int Year { get; set; }

        [Display(Name = "Тип кузова")]
        [Required]
        public string BodyType { get; set; }

        [Display(Name = "Зображення")]
        [StringLength(255)]
        public string? ImagePath { get; set; }

        [NotMapped]
        [Display(Name = "Завантажити картинку")]
        public IFormFile? ImageFile { get; set; }
    }
}
