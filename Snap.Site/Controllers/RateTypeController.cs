using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Admin;

namespace Snap.Site.Controllers
{
    public class RateTypeController : Controller
    {
        private readonly IAdminService _service;

        public RateTypeController(IAdminService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var rate =await _service.GetRateTypes();
            
            return View(rate);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RateTypeViewModel rateType)
        {
             _service.AddRateType(rateType);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var entity =await _service.GetRateTypeById(id);
            var model = new RateTypeViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                OK = entity.OK,
                OrderView = entity.OrderView,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RateTypeViewModel viewModel,Guid id)
        {
             _service.UpdateRate(id,viewModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete( Guid id)
        {
            await _service.DeleteRateType(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
