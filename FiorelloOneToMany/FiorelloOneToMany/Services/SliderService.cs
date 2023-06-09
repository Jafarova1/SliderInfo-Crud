using Elearn.Models;
using FiorelloOneToMany.Areas.Admin.ViewModels.Slider;
using FiorelloOneToMany.Data;
using FiorelloOneToMany.Helpers;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloOneToMany.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Slider>> GetAllAsync()
        {
            return await _context.Sliders.ToListAsync();
        }

        public SliderDetailVM GetMappedData(Slider slider)
        {
            SliderDetailVM model = new()
            {
                Logo = slider.Logo,
                Title = slider.Title,
                //CreatedDate = dbSlider.CreatedDate.ToString("MMMM dd, yyyy"),
                Status = slider.Status,
                Description = slider.Description,
            };
            return model;
        }

        public async Task<List<SliderVM>> GetAllMappedDatas()
        {
            List<SliderVM> sliderList = new();
            List<Slider> sliders = await GetAllAsync();
            foreach (Slider slider in sliders)
            {
                SliderVM model = new()
                {
                    Id = slider.Id,
                    Logo = slider.Logo,
                    Title = slider.Title,
                    Description = slider.Description,
                    //CreatedDate = slider.CreatedDate.ToString("MMMM dd, yyyy"),
                    Status = slider.Status,
                };

                sliderList.Add(model);
            }

            return sliderList;

        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateAsync(List<IFormFile> images)
        {

            foreach (var item in images)
            {
                string filename = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(filename, _env.WebRootPath, "img");


                Slider slider = new()
                {
                    Image = filename
                };


                await _context.Sliders.AddAsync(slider);


                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await GetByIdAsync(id);

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "img", slider.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }



        }

        public async Task EditAsync(Slider slider, IFormFile newIamge)
        {

            string oldPath = Path.Combine(_env.WebRootPath, "img", slider.Image);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newIamge.FileName;


            await newIamge.SaveFileAsync(fileName, _env.WebRootPath, "img");

            slider.Image = fileName;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Slider>> GetAllByStatusAsync()
        {

            return await _context.Sliders.Where(m => m.Status).ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Sliders.Where(m => m.Status).CountAsync();
        }

        public async Task<bool> ChangeStatusAsync(Slider slider)
        {
            if (slider.Status && await GetCountAsync()! == 1)
            {
                slider.Status = false;


            }
            else
            {
                slider.Status = true;
            }
            await _context.SaveChangesAsync();
            return slider.Status;
        }
    }
}
