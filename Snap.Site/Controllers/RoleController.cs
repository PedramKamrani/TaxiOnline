using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Admin;

namespace Snap.Site.Controllers
{
    public class RoleController : Controller
    {
        private readonly IAdminService _service;

        public RoleController(IAdminService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _service.GetRoles();
            return View(result);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel viewModel)
        {
             _service.AddRole(viewModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var entity = await _service.GetRoleById(id);
            var viewModel = new RoleViewModel
            {
                Title = entity.Title,
                Name = entity.Name,
                Id =entity.Id
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel viewModel)
        {
             _service.UpdateRole(viewModel,viewModel.Id);
           return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            _service.DeleteRole(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
