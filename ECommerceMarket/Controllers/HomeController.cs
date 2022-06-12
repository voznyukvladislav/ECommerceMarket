using ECommerceMarket.Data;
using ECommerceMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ECommerceMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ECommerceDbContext _db;

        public HomeController(ECommerceDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> categories = _db.Categories
                .Include(c => c.SubCategories)
                .ToList();

            ViewBag.Categories = categories;

            return View();
        }
    }
}