using ECommerceMarket.Data;
using ECommerceMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMarket.Controllers
{
    public class ProductViewController : Controller
    {
        private readonly ECommerceDbContext _db;
        public ProductViewController(ECommerceDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index(int subCategoryId)
        {
            List<Category> categories = _db.Categories
                .Include(c => c.SubCategories)
                .ToList();            

            SubCategory subCategory = (from sc in _db.SubCategories
                .Include(s => s.Presets)
                .ThenInclude(p => p.Products)
                .ThenInclude(p => p.ProductAttributes)
                .ThenInclude(p => p.Attribute)
                .ToList()
            where sc.Id == subCategoryId
            select sc).ToList()[0];

            ViewBag.Categories = categories;
            ViewBag.SubCategory = subCategory;

            return View();
        }
    }
}
