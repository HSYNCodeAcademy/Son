using Business.Services;
using DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EduhomePraktika._1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISlidersService _sliderService;

        public SliderController(ISlidersService sliderService, IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _env = env;
        }

        // GET: SliderController
        public async Task<IActionResult> Index()
        {
            var data = await _sliderService.GetAll();
            return View(data);
        }

        // GET: SliderController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var data = await _sliderService.Get(id);
            return View(data);
        }

        // GET: SliderController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: SliderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (slider.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View(slider);
            }

            if (!slider.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View(slider);
            }

            decimal size = (decimal)slider.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View(slider);
            }

            var fileName = slider.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64,64);
            }
               
            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using(FileStream fileStream = new FileStream (path, FileMode.Create))
            {
                await slider.ImageFile.CopyToAsync(fileStream);
            }
            slider.ImageURL = newFileName;
            slider.CreatedDate = DateTime.Now;
            await _sliderService.Create(slider);
            return RedirectToAction("index", "slider");
        }

        // GET: SliderController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var data = await _sliderService.Get(id);
            return View(data);
        }

        // POST: SliderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Slider slider)
        {
            if (slider.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View(slider);
            }

            if (!slider.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View(slider);
            }

            decimal size = (decimal)slider.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View(slider);
            }

            var fileName = slider.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await slider.ImageFile.CopyToAsync(fileStream);
            }

            var data = await _sliderService.Get(slider.Id);

            slider.ImageURL = newFileName;
            data.Title = slider.Title;
            data.Body = slider.Body;
            slider.UpdatedDate = DateTime.Now;
            await _sliderService.Update(data);
            return RedirectToAction("index", "slider");
        }

        // GET: SliderController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _sliderService.Get(id);
            return View(data);
        }

        // POST: SliderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Slider slider)
        {
            await _sliderService.Delete(slider.Id);
            return RedirectToAction("index", "slider");
        }
    }
}
