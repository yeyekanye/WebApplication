using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть модель авто")]
        [StringLength(50, ErrorMessage = "Модель не може перевищувати 50 символів")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Введіть колір авто")]
        public string Color { get; set; }

        [Range(1900, 2100, ErrorMessage = "Рік має бути в діапазоні 1900-2100")]
        public int Year { get; set; }

        [Display(Name = "Тип кузова")]
        [Required(ErrorMessage = "Введіть тип кузова")]
        public string BodyType { get; set; }
    }
}
