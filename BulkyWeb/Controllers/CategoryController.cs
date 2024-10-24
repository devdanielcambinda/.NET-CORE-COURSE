﻿using BulkyWeb.Data;
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
            this._db.Categories.Add(obj); //operation being executed
            this._db.SaveChanges(); // save to database
            return RedirectToAction("Index"); // (Action, Controller)
        }
    }
}
