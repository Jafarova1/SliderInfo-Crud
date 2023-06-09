using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloOneToMany.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        //public async Task<IActionResult> Index()=>View(_productService.GetMapped)

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetWithIncludesAsync((int)id);
            if (product is null) return NotFound();
            return View(_productService.GetMappedData(product));
        }
      
    }
}
