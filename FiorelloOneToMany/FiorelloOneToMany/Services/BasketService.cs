using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using FiorelloOneToMany.VıewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FiorelloOneToMany.Services
{
    public class BasketService:IBasketService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public BasketService(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public void AddProduct(List<BasketVM> basket, Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);

            var data = basketDatas.FirstOrDefault(m => m.Id == id);
            basketDatas.Remove(data);

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));

            //return RedirectToActionResult(nameof(Index));
        }


        public List<BasketVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public int GetCount()
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

            return basket.Sum(m => m.Count);
        }
    }
}
