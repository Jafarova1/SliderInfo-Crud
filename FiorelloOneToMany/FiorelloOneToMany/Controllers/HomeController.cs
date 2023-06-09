using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using FiorelloOneToMany.VıewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FiorelloOneToMany.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        public HomeController(AppDbContext context,IProductService productService)
        {
            _context= context;
            _productService= productService;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Expert>experts=await _context.Experts.Include(m=>m.Position).Where(m=>m.SoftDelete).ToListAsync();

            IEnumerable<Product> products = await _productService.GetProducts();

            HomeVM home = new()
            {
                Experts = experts,
                Products= products
            };
            return View(home);
        }
      
        
   
    }
}