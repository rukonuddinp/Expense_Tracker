using Expense_Tracker3.Data;
using Expense_Tracker3.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker3.Controllers
{
    public class ExpenseController : Controller
    {
        private ApplicationContext db;
        IWebHostEnvironment webHostEnvironment;
       

        public ExpenseController(ApplicationContext _db, IWebHostEnvironment _webHostEnvironment)
        {
            db = _db;
            webHostEnvironment = _webHostEnvironment;
        }
        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            var endexvar = db.Expenses.Include(m=>m.Category);
            if (startDate.HasValue && endDate.HasValue)
            {
                return View(endexvar.Where(w=>w.Date >=startDate && w.Date <=endDate).AsEnumerable());
            }
            else
            {
                return View(endexvar.ToList());
            }
            
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CategoryId_Fk"] = new SelectList (db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(Expense expense)
        {

            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
             bool isSaved=  await db.SaveChangesAsync()>0;


                if (isSaved)
                {
                    ViewBag.Message = "Save Successfull!";
                }

            }

            ViewData["CategoryId_Fk"] = new SelectList(db.Categories, "CategoryId", "Name", expense.CategoryId_Fk);
            return View(expense);
        }

       public IActionResult Edit(int id)
        {
            var ExpeEdit = db.Expenses.SingleOrDefault(E=>E.ExpenseId==id);
            var result = new Expense()
            {
                ExpenseId = ExpeEdit.ExpenseId,
                CategoryId_Fk = ExpeEdit.CategoryId_Fk,
                Date = ExpeEdit.Date,
                Amount = ExpeEdit.Amount
            };

            ViewData["CategoryId_Fk"] = new SelectList(db.Categories, "CategoryId", "Name", ExpeEdit.CategoryId_Fk);
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit (Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Update(expense);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewData["CategoryId_Fk"] = new SelectList(db.Categories, "CategoryId", "Name", expense.CategoryId_Fk);
            return View(expense);
            
        }

        public IActionResult Delete(int id)
        {
            var deresult = db.Expenses.SingleOrDefault(D=>D.ExpenseId==id);
            db.Expenses.Remove(deresult);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        



    }
}
