using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Admin;
using Snap.Data.Layer.Entities;

namespace Snap.Site.Controllers
{
    public class PriceTypeController : Controller
    {
        private readonly IAdminService _service;

        public PriceTypeController(IAdminService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var priceType =await _service.GetPricesType();
            return View(priceType);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(PriceTypeViewModel viewModel)
        {
            _service.AddPrice(viewModel);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var entity =await _service.GetPricesBy(id);
            var viewModel = new PriceTypeViewModel
            {
                Start = entity.Start,
                End = entity.End,
                Name = entity.Name,
                Price = entity.Price
            };
           return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id,PriceTypeViewModel model)
        {
            if (model != null)
            {
                await _service.EditPriceType(id, model);
            }
          
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var entity = _service.DeletePriceType(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
