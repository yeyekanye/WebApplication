using Microsoft.AspNetCore.Mvc;
using CarApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarApp.Controllers
{
    public class CarsController : Controller
    {
        // "База даних" у пам’яті (для прикладу)
        private static List<Car> _cars = new List<Car>
        {
            new Car { Id=1, Model="BMW X5", Color="Чорний", Year=2020, BodyType="Кросовер" },
            new Car { Id=2, Model="Audi A6", Color="Білий", Year=2019, BodyType="Седан" },
            new Car { Id=3, Model="Toyota Corolla", Color="Сірий", Year=2021, BodyType="Седан" }
        };

        // Завдання 1: Відобразити список
        public IActionResult Index()
        {
            return View(_cars);
        }

        // Завдання 2: Деталі
        public IActionResult Details(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        // Завдання 3: Видалення (GET + POST)
        public IActionResult Delete(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car != null) _cars.Remove(car);
            return RedirectToAction(nameof(Index));
        }
    }
}
