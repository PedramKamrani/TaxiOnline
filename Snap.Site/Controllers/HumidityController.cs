using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Admin;
using Snap.Data.Layer.Entities;

namespace Snap.Site.Controllers
{
    public class HumidityController : Controller
    {
        
            private IAdminService _admin;

            public HumidityController(IAdminService admin)
            {
                _admin = admin;
            }

            public async Task<IActionResult> Index()
            {
                var result = await _admin.GetHumiditys();

                return View(result);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Create(MonthTypeViewModel viewModel)
            {
                if (ModelState.IsValid)
                {
                    _admin.AddHumidity(viewModel);

                    return RedirectToAction(nameof(Index));
                }

                return View(viewModel);
            }

            public async Task<IActionResult> Edit(Guid id)
            {
                Humidity humidity = await _admin.GetHumidityById(id);

                MonthTypeViewModel viewModel = new MonthTypeViewModel()
                {
                    End = humidity.End,
                    Name = humidity.Name,
                    Percent = humidity.Precent,
                    Start = humidity.Start
                };

                return View(viewModel);
            }

            [HttpPost]
            public IActionResult Edit(Guid id, MonthTypeViewModel viewModel)
            {
                if (ModelState.IsValid)
                {
                    bool result = _admin.UpdateHumidity(viewModel, id);

                    if (result)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                return View(viewModel);
            }

            public IActionResult Delete(Guid id)
            {
                _admin.DeleteHumidity(id);

                return RedirectToAction(nameof(Index));
            }
        }
    }

