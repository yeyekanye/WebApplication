using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;      // <- потрібен для IWebHostEnvironment
using Microsoft.AspNetCore.Http;        // <- потрібен для IFormFile
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarContext _context;
        private readonly IWebHostEnvironment _env;

        public CarsController(CarContext context, IWebHostEnvironment env)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        // INDEX
        public IActionResult Index()
        {
            var cars = _context.Cars.ToList();
            return View(cars);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        // CREATE (GET)
        [HttpGet]
        public IActionResult Create() => View();

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (!ModelState.IsValid) return View(car);

            if (car.ImageFile != null && car.ImageFile.Length > 0)
            {
                var saved = SaveImageToWwwroot(car.ImageFile);
                if (saved == null)
                {
                    ModelState.AddModelError("ImageFile", "Підтримуються лише JPG/JPEG/PNG/GIF до 5 МБ.");
                    return View(car);
                }
                car.ImagePath = saved;
            }

            _context.Cars.Add(car);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Car formCar)
        {
            if (id != formCar.Id) return NotFound();
            if (!ModelState.IsValid) return View(formCar);

            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();

            car.Model = formCar.Model;
            car.Color = formCar.Color;
            car.Year = formCar.Year;
            car.BodyType = formCar.BodyType;

            if (formCar.ImageFile != null && formCar.ImageFile.Length > 0)
            {
                var saved = SaveImageToWwwroot(formCar.ImageFile);
                if (saved == null)
                {
                    ModelState.AddModelError("ImageFile", "Підтримуються лише JPG/JPEG/PNG/GIF до 5 МБ.");
                    return View(formCar);
                }

                DeletePhysicalFileIfExists(car.ImagePath);
                car.ImagePath = saved;
            }

            _context.Cars.Update(car);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // DELETE (GET)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();

            DeletePhysicalFileIfExists(car.ImagePath);

            _context.Cars.Remove(car);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // --- file helpers ---
        private string? SaveImageToWwwroot(IFormFile file)
        {
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(ext) || !allowed.Contains(ext)) return null;
            if (file.Length > 5 * 1024 * 1024) return null; // 5 MB

            var imagesDir = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(imagesDir)) Directory.CreateDirectory(imagesDir);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var physicalPath = Path.Combine(imagesDir, fileName);

            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"/images/{fileName}";
        }

        private void DeletePhysicalFileIfExists(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return;
            var physical = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (System.IO.File.Exists(physical))
            {
                try { System.IO.File.Delete(physical); } catch { /* ignore */ }
            }
        }
    }
}
