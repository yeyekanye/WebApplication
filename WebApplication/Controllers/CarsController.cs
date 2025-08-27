using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Data;
using System.Linq;

namespace CarApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarContext _context;

        public CarsController(CarContext context)
        {
            _context = context;
        }

        // Завдання 1: список
        public IActionResult Index()
        {
            return View(_context.Cars.ToList());
        }

        // Завдання 2: деталі
        public IActionResult Details(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        // Завдання 3: видалення
        public IActionResult Delete(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
