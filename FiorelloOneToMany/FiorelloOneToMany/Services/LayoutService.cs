using FiorelloOneToMany.Data;
using FiorelloOneToMany.Services.Interfaces;
using FiorelloOneToMany.VıewModels;
using Newtonsoft.Json;

namespace FiorelloOneToMany.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IBasketService _basketService;
        public LayoutService(AppDbContext context, IHttpContextAccessor accessor,IBasketService basketService)
        {
            _context = context;
            _accessor = accessor;
            _basketService = basketService;
        }


        public LayoutVM GetAllDatas()
        {

            int count = _basketService.GetCount();
         var datas= _context.Settings.AsEnumerable().ToDictionary(m=>m.Key,m=>m.Value);
            return new LayoutVM { BasketCount=count,SettingDatas=datas};
         
        }

        private int GetBasketCount()
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

                return basket.Sum(m=>m.Count);

            

        }
    }
}
