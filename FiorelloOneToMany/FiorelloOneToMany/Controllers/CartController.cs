using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using FiorelloOneToMany.VıewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.ContentModel;

namespace FiorelloOneToMany.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productservice;
        private readonly IHttpContextAccessor _accessor;
        private readonly IBasketService _basketService;

        public CartController(IProductService productService, IHttpContextAccessor accessor,IBasketService basketService)
        {
            _productservice = productService;
            _accessor = accessor;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {

            List<BasketDetailVm> basketList = new();
            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
                foreach (var item in basketDatas)
                {
                    var dbProduct = await _productservice.GetByIdWithImagesAsync(item.Id);

                    if (dbProduct != null)
                    {
                        BasketDetailVm basketDetail = new()
                        {
                            Id = dbProduct.Id,
                            Name = dbProduct.Name,
                         
                        };
                    }


                }

            }



            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            //Product product = await _productservice.Products.FindAsync(id);

            //if (product is null)
            //{
            //    return NotFound();
            //}



            List<BasketVM> basket = GetBasketDatas();

            //AddProductToBasket(basket, product);




            return Ok(basket.Sum(m=>m.Count));


        }

        private List<BasketVM> GetBasketDatas()
        {
            List<BasketVM> basket;

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }

        private void AddProductToBasket(List<BasketVM> basket, Product product)
        {
            BasketVM existProduct = basket.FirstOrDefault(m => m.Id == product.Id);

            if (existProduct is null)
            {
                basket.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1

                });
            }
            else
            {
                existProduct.Count++;
            }

_accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

        }



        [ActionName("Delete")]
        [HttpPost]
   
        public IActionResult DeleteProductFromBasket(int? id)
        {
            //_basketService.DeleteProduct(id);



            return Ok();
        }
    }
}
