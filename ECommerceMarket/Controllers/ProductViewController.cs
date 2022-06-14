using ECommerceMarket.Data;
using ECommerceMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ECommerceMarket.Controllers
{
    public class ProductViewController : Controller
    {
        private readonly ECommerceDbContext _db;
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }

        public int CurrentPhotoOrder { get; set; } = 0;

        public ProductViewController(ECommerceDbContext db)
        {
            _db = db;

            Categories = _db.Categories
                .Include(c => c.SubCategories)
                .ToList();

            SubCategories = _db.SubCategories
                .Include(s => s.Presets)
                .ThenInclude(p => p.Products)
                .ThenInclude(p => p.ProductAttributes)
                .ThenInclude(p => p.Attribute)
                .ThenInclude(p => p.PresetAttributes)
                .ToList();

            List<PhotoDTO> photos = JsonSerializer.Deserialize<List<PhotoDTO>>(SubCategories[0].Presets[0].Products[0].PhotosJson);
            int kek = 0;

        }

        [HttpGet]
        public IActionResult Index(int subCategoryId)
        {
            SubCategory subCategory = (from sc in this.SubCategories
            where sc.Id == subCategoryId
            select sc).ToList()[0];            

            ViewBag.Categories = this.Categories;
            ViewBag.SubCategory = subCategory;

            return View();
        }

        [HttpGet]
        public IActionResult ProductPage(int productId)
        {
            if (_db.Products.Any(p => p.Id == productId))
            {
                Product product = _db.Products.Find(productId);

                ViewBag.Product = product;
                ViewBag.Categories = this.Categories;

                return View();
            }

            else return NotFound();            
        }

        [HttpGet]       
        public IActionResult ProductPhotoView(int productId, bool direction)
        {
            // if direction = false, move to left else move to right
            if(_db.Products.Any(p => p.Id == productId))
            {
                Product product = _db.Products.Find(productId);
                List<PhotoDTO> photos = JsonSerializer.Deserialize<List<PhotoDTO>>(product.PhotosJson);
                if(direction)
                {
                    if (CurrentPhotoOrder + 1 < photos.Count) CurrentPhotoOrder++;                    
                } else
                {
                    if (CurrentPhotoOrder - 1 >= 0) CurrentPhotoOrder--;
                }

                ViewBag.Product = product;
                ViewBag.Photo = photos[CurrentPhotoOrder];

                return PartialView();
            }

            return NotFound();
        }
    }
}
