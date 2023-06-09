using FiorelloOneToMany.Areas.Admin.ViewModels.Product;
using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloOneToMany.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Include(m => m.Image).Where(m => m.SoftDelete).ToListAsync();
        }

        public async Task<Product> GetByIdAync(int? id) => await _context.Products.FindAsync(id);

        public Task<Product> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetByIdWithImagesAsync(int? id)
        {
           return await _context.Products.Include(m => m.Image).FirstOrDefaultAsync(m => m.Id == id);
        }

     
        public async Task<Product> GetWithIncludesAsync(int id)
        {
           return await _context.Products.Where(m => m.Id == id).Include(m => m.Image).Include(m => m.Category).Include(m => m.Discount).FirstOrDefaultAsync();
        }

        public ProductDetailVM GetMappedData(Product product)
        {
            return new ProductDetailVM
            {
                Name = product.Name,
                Description = product.Description,
                Price= product.Price.ToString(),
                Discount= product.Discount,
                CategoryName=product.Category,
                CreateDate=product.CreateDate.ToString("MM//dd/yyyy"),

              //Images = product.Image.Select(m => m.Image);


        };
        }

        //public List<ProductVM> GetMappedDatas(List<Product> products)
        //{
           
        //}

        //public async Task<Product> GetAllWithIncludesAsync() =>  await _context.Products.Include(m => m.Image).Include(m => m.Category).Include(m => m.Discount).ToListAsync();
       
    }
}
