using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Admin;

namespace Snap.Site.Controllers
{
    public class UserController : Controller
    {
        private readonly IAdminService _service;

        public UserController(IAdminService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _service.GetUsers();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            
            ViewBag.RoleId = new SelectList(await _service.GetRoles(), "Id", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _service.AddUser(viewModel);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RoleId = new SelectList(await _service.GetRoles(), "Id", "Title");
            return View(viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            _service.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string UserName)
        {
            var viewModel = await _service.GetUserForUpdateById(UserName);
            ViewBag.RoleId = new SelectList(await _service.GetRoles(), "Id", "Title", viewModel.RoleId);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userid = await _service.GetUserIdByUserName(viewModel.Username);
                _service.EditUser(viewModel,userid);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RoleId = new SelectList(await _service.GetRoles(), "Id", "Title", viewModel.RoleId);
            return View(viewModel);
        }
    }
}
