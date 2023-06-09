using FiorelloOneToMany.Areas.Admin.ViewModels.Product;
using FiorelloOneToMany.Models;

namespace FiorelloOneToMany.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetByIdAsync(int? id);
        Task<Product> GetByIdWithImagesAsync(int? id);
        //List<ProductVM> GetMappedDatas(List<Product> products);
        //Task<Product> GetAllWithIncludesAsync();

        Task<Product> GetWithIncludesAsync(int id);

        ProductDetailVM GetMappedData(Product product);

    }
}
