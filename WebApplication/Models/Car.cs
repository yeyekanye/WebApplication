namespace CarApp.Models
{
    public class Car
    {
        public int Id { get; set; }        // Унікальний ідентифікатор
        public string Model { get; set; }  // Модель авто
        public string Color { get; set; }  // Колір
        public int Year { get; set; }      // Рік випуску
        public string BodyType { get; set; } // Тип кузова
    }
}
