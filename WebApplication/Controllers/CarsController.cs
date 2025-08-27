using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;
using System.Linq;

namespace WebApplication.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarContext _context;

        public CarsController(CarContext context)
        {
            _context = context;
        }

        // ======= INDEX (список авто) =======
        public IActionResult Index()
        {
            var cars = _context.Cars.ToList();
            return View(cars);
        }

        // ======= DETAILS (деталі авто) =======
        public IActionResult Details(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        // ======= CREATE (GET) =======
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ======= CREATE (POST) =======
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // ======= EDIT (GET) =======
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        // ======= EDIT (POST) =======
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Car car)
        {
            if (id != car.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Cars.Update(car);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // ======= DELETE (GET) =======
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        // ======= DELETE (POST) =======
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();

            _context.Cars.Remove(car);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
