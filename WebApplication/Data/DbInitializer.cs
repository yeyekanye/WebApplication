using WebApplication.Models;
using System.Linq;

namespace WebApplication.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CarContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cars.Any())
            {
                return;
            }

            var cars = new Car[]
            {
                new Car { Model="BMW X5", Color="Чорний", Year=2020, BodyType="Кросовер" },
                new Car { Model="Audi A6", Color="Білий", Year=2019, BodyType="Седан" },
                new Car { Model="Toyota Corolla", Color="Сірий", Year=2021, BodyType="Седан" },
                new Car { Model="Ford Mustang", Color="Червоний", Year=2018, BodyType="Купе" }
            };

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }
    }
}
