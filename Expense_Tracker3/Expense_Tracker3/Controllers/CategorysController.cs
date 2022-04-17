using Expense_Tracker3.Data;
using Expense_Tracker3.Models;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker3.Controllers
{
    public class CategorysController : Controller
    {

        private ApplicationContext db;
        public CategorysController(ApplicationContext _db)
        {
            this.db = _db;

        }
        public IActionResult Index()
        {
            
           var result= db.Categories.ToList();
            return View(result);
            
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            
            Category category = db.Categories.FirstOrDefault(d => d.CategoryId == id);
            if (category != null)
            {
                db.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Edit(int id)
        {
            var catEdi = db.Categories.SingleOrDefault(E => E.CategoryId == id);
            var result = new Category()
            {
                CategoryId = catEdi.CategoryId,
                Name = catEdi.Name
            };
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            var catEdi = new Category()
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
            db.Categories.Update(catEdi);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
