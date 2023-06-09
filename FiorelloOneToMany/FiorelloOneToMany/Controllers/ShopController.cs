using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.VıewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.ContentModel;

namespace FiorelloOneToMany.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
     
        public ShopController(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor =accessor;
       
        }

        public async Task<IActionResult> Index()
        {

            IEnumerable<Product> products = await _context.Products.Where(m => m.SoftDelete).Take(4).ToListAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> ShowMore(int skip)
        {
            IEnumerable<Product> products = await _context.Products.Where(m => m.SoftDelete).Skip(skip).Take(4).ToListAsync();
            return View(products);
            

        }


    }
}
