using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Admin;
using Snap.Data.Layer.Entities;

namespace Snap.Site.Controllers
{
    public class ColorController : Controller
    {
        private readonly IAdminService _Service;

        public ColorController(IAdminService service)
        {
            _Service = service;
        }


        public async Task<IActionResult> Index()
        {
            var colors =await _Service.GetAllColor();
            return View(colors);
        }
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public  IActionResult Create(ColorAdminViewModel viewModel)
        {
             _Service.AddColor(viewModel);
             return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult>Edit(Guid id)
        {
            var entity =await _Service.GetColorById(id);
            var viewModel = new ColorAdminViewModel
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(ColorAdminViewModel viewModel)
        {
            var entity = _Service.UpdateColor(viewModel,viewModel.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity =await _Service.DeleteColor(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
