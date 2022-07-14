using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cars.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsDbContext _db;
        public CarsController(CarsDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewAd(int id)
        {
            Car c = _db.Cars.Find(id);
            if (c == null) return RedirectToAction("NotFound");
            return View(c);
        }
        public ActionResult GetImage(int id)
        {
            Car c = _db.Cars.Find(id);
            
            if (c.Photo == null)
            {
                return null;
            }
            return File(c.Photo, "image/jpg");
        }

        public IActionResult AddAd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddedAd(Car car, IFormFile files)
        {
            if (ModelState.IsValid)
            {
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        using (var target = new MemoryStream())
                        {
                            files.CopyTo(target);
                            car.Photo = target.ToArray();
                        }
                    }
                }
                car.Password = "123";

                _db.Cars.Add(car);
                _db.SaveChanges();
                
            }
            return View(car);
        }

        public IActionResult DeleteAd(int id)
        {
            Car c = _db.Cars.Find(id);
            if (c == null) return RedirectToAction("NotFound");
            return View(c);
        }

        [HttpDelete]
        public IActionResult DeleteAd(int id,string pass)
        {
            var car = _db.Cars.FirstOrDefault(u => u.Id == id);
            if (car == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }else if(car.Password != pass)
            {
                return Json(new { success = false, message = "Wrong password!" });
            }
            _db.Cars.Remove(car);
            _db.SaveChanges();
            //return RedirectToAction("Index");
            return Json(new { success = true, message = "Delete successful" });
        }
        public IActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Cars.ToListAsync() });
        }
    }
}