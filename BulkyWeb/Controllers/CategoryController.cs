using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCaterogryList = this._db.Categories.ToList();
            return View(objCaterogryList);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder", "The Display Order cannot exactly match the Name.");
            }
            //if (obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value.");
            //}
            if (ModelState.IsValid)
            {
                this._db.Categories.Add(obj); //operation being executed
                this._db.SaveChanges(); // save to database
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index"); // (Action, Controller)
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0) 
            {
                return NotFound();
            }
            
            
            Category? categoryFromDb = this._db.Categories.Find(Id);
            //Category? categoryFromDb1 = this._db.Categories.FirstOrDefault(u=>u.Id == categoryId);
            //Category? categoryFromDb2 = this._db.Categories.Where(u => u.Id == categoryId).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                this._db.Categories.Update(obj); //operation being executed
                this._db.SaveChanges(); // save to database
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index"); // (Action, Controller)
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }


            Category? categoryFromDb = this._db.Categories.Find(id);
            //Category? categoryFromDb1 = this._db.Categories.FirstOrDefault(u=>u.Id == categoryId);
            //Category? categoryFromDb2 = this._db.Categories.Where(u => u.Id == categoryId).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categoryFromDb = this._db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            this._db.Categories.Remove(categoryFromDb);
            this._db.SaveChanges();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }


    }
}
