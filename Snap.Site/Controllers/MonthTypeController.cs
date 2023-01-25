using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Admin;

namespace Snap.Site.Controllers
{
    public class MonthTypeController : Controller
    {
        private readonly IAdminService _service;

        public MonthTypeController(IAdminService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetMonthTypes();

            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MonthTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _service.AddMonthType(viewModel);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var month = await _service.GetMonthTypeById(id);

            MonthTypeViewModel viewModel = new MonthTypeViewModel()
            {
                End = month.End,
                Name = month.Name,
                Percent = month.Precent,
                Start = month.Start
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(MonthTypeViewModel viewModel, Guid id)
        {
            if (ModelState.IsValid)
            {
                var result = _service.UpdateMonthType(viewModel, id);

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            _service.DeleteMonthType(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

