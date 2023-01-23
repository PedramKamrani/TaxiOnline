using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Admin;

namespace Snap.Site.Controllers
{
    public class CarController : Controller
    {
        private readonly IAdminService _service;

        public CarController(IAdminService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var cars=await _service.GetCars();
            return View(cars);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarViewModel carViewModel)
        {
            _service.AddCar(carViewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var viewModel= await _service.GetCarById(id);
            return View(viewModel);
        }

        [HttpPost]
        public  IActionResult Edit(CarViewModel model)
        {
           _service.UpdateCar(model.CarId, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public  IActionResult Delete(Guid id)
        {
            _service.DeleteCar(id);
            return RedirectToAction("Index");
        }
    }
}
