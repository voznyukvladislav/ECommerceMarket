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

        public int CurrentPhotoOrder { get; set; }

        public ProductViewController(ECommerceDbContext db)
        {
            _db = db;

            CurrentPhotoOrder = 0;
        }

        [HttpGet]
        public IActionResult Index(int subCategoryId)
        {
            // More optimized database query
            /*SubCategories = _db.SubCategories.Include(s => s.Presets).ToList();
            for (int i = 0; i < SubCategories.Count; i++)
            {
                for (int j = 0; j < SubCategories[i].Presets.Count; j++)
                {
                    *//*SubCategories[i].Presets[j].Products = _db.Products
                        .Where(p => p.PresetId == SubCategories[i].Presets[j].Id)
                        .Include(p => p.Discount)
                        .Include(p => p.ProductAttributes)
                        .ThenInclude(p => p.Attribute)
                        .ThenInclude(p => p.PresetAttributes)
                        .ToList();*//*
                    SubCategories[i].Presets[j].Products = _db.Products
                        .Where(p => p.PresetId == SubCategories[i].Presets[j].Id)
                        .Include(p => p.Discount)
                        .Include(p => p.ProductAttributes)
                        .ToList();
                    for (int k = 0; k < SubCategories[i].Presets[j].Products.Count; k++)
                    {
                        for (int l = 0; l < SubCategories[i].Presets[j].Products[k].ProductAttributes.Count; l++)
                        {
                            SubCategories[i].Presets[j].Products[k].ProductAttributes = _db.ProductAttributes
                                .Where(p => p.ProductId == SubCategories[i].Presets[j].Products[k].Id)
                                .Include(p => p.Attribute)
                                .ThenInclude(a => a.PresetAttributes)
                                .ToList();
                        }
                    }
                }
            }*/

            // Low performance query
            /*SubCategories = _db.SubCategories
                .Include(s => s.Presets)
                .ThenInclude(p => p.Products)
                .ThenInclude(p => p.Discount)
                .ThenInclude(p => p.Products)
                .ThenInclude(p => p.ProductAttributes)
                .ThenInclude(p => p.Attribute)
                .ThenInclude(p => p.PresetAttributes)
                .ToList();*/

            Categories = _db.Categories
                .Include(c => c.SubCategories)
                .ToList();

            SubCategory subCategory = _db.SubCategories
                .Include(s => s.Presets)
                .Where(s => s.Id == subCategoryId)
                .ToList()[0];

            for (int j = 0; j < subCategory.Presets.Count; j++)
            {
                /*SubCategories[i].Presets[j].Products = _db.Products
                    .Where(p => p.PresetId == SubCategories[i].Presets[j].Id)
                    .Include(p => p.Discount)
                    .Include(p => p.ProductAttributes)
                    .ThenInclude(p => p.Attribute)
                    .ThenInclude(p => p.PresetAttributes)
                    .ToList();*/
                subCategory.Presets[j].Products = _db.Products
                    .Where(p => p.PresetId == subCategory.Presets[j].Id)
                    .Include(p => p.Discount)
                    .Include(p => p.ProductAttributes)
                    .ToList();
                for (int k = 0; k < subCategory.Presets[j].Products.Count; k++)
                {
                    for (int l = 0; l < subCategory.Presets[j].Products[k].ProductAttributes.Count; l++)
                    {
                        subCategory.Presets[j].Products[k].ProductAttributes = _db.ProductAttributes
                            .Where(p => p.ProductId == subCategory.Presets[j].Products[k].Id)
                            .Include(p => p.Attribute)
                            .ThenInclude(a => a.PresetAttributes)
                            .ToList();
                    }
                }
            }

            ViewBag.Categories = this.Categories;
            ViewBag.SubCategory = subCategory;

            return View();
        }

        [HttpGet]
        public IActionResult ProductPage(int productId)
        {
            if (_db.Products.Any(p => p.Id == productId))
            {
                Categories = _db.Categories
                .Include(c => c.SubCategories)
                .ToList();

                Product product = _db.Products
                    .Include(p => p.Discount)
                    .Include(p => p.ProductAttributes)
                    .ThenInclude(p => p.Attribute)
                    .Where(p => p.Id == productId)
                    .ToList()[0];

                ViewBag.Product = product;
                ViewBag.Categories = this.Categories;

                return View();
            }

            else return NotFound();            
        }

        [HttpGet]       
        public IActionResult ProductPhotoView(int productId, bool direction, int currentPhotoOrder)
        {
            // if direction = false, move to left else move to right
            if(_db.Products.Any(p => p.Id == productId))
            {
                CurrentPhotoOrder = currentPhotoOrder;
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
                ViewBag.CurrentPhotoOrder = CurrentPhotoOrder;
                ViewBag.PhotosCount = photos.Count;

                return PartialView();
            }

            return NotFound();
        }
    }
}
