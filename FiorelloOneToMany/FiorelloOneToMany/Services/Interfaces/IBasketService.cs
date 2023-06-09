using FiorelloOneToMany.Models;
using FiorelloOneToMany.VıewModels;

namespace FiorelloOneToMany.Services.Interfaces
{
    public interface IBasketService
    {
        List<BasketVM> GetAll();
        void AddProduct(List<BasketVM> basket, Product product);

        void DeleteProduct(int id);
        int GetCount();
    }
}
