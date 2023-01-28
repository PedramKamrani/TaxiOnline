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


        public async Task<IActionResult> DriverProp(Guid id)
        {
            var result = await _service.GetDriver(id);

            DriverPropViewModel viewModel = new DriverPropViewModel()
            {
                Address = result.Address,
                AvatarName = result.Avatar,
                NationalCode = result.NationalCode,
                Tel = result.Telephone
            };

            ViewBag.IsError = false;
            ViewBag.IsSuccess = false;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DriverProp(Guid id, DriverPropViewModel viewModel)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                status = await _service.UserDriverProp(id, viewModel);
            }

            if (status)
            {
                ViewBag.IsError = false;
                ViewBag.IsSuccess = true;
            }
            else
            {
                ViewBag.IsError = true;
                ViewBag.IsSuccess = false;
            }

            var result = await _service.GetDriver(id);


            DriverPropViewModel model = new DriverPropViewModel()
            {
                Address = result.Address,
                AvatarName = result.Avatar,
                NationalCode = result.NationalCode,
                Tel = result.Telephone
            };

            return View(model);
        }

        public async Task<IActionResult> DriverCertificate(Guid id)
        {
            var driver = await _service.GetDriver(id);

            DriverImgViewModel model = new DriverImgViewModel()
            {
                ImgName = driver.Image,
                IsConfirm = driver.IsConfirm
            };

            ViewBag.IsSuccess = false;
            ViewBag.IsError = false;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DriverCertificate(Guid id, DriverImgViewModel viewModel)
        {
            var driver = await _service.GetDriver(id);

            bool result = false;

            if (ModelState.IsValid)
            {
                result = _service.UpdateDriverCertificate(id, viewModel);
            }

            if (result)
            {
                ViewBag.IsSuccess = true;
                ViewBag.IsError = false;
            }
            else
            {
                ViewBag.IsSuccess = false;
                ViewBag.IsError = true;
            }

            DriverImgViewModel model = new DriverImgViewModel()
            {
                ImgName = driver.Image,
                IsConfirm = driver.IsConfirm
            };

            return View(model);
        }

        public async Task<IActionResult> DriverCar(Guid id)
        {
            var driver = await _service.GetDriver(id);

            DriverCarViewModel viewModel = new DriverCarViewModel()
            {
                CarCode = driver.Code,
                CarId = driver.CarId,
                ColorId = driver.ColorId
            };

            if (driver.CarId == null)
            {
                ViewBag.CarId = new SelectList(await _service.GetCars(), "CarId", "Name");
            }
            else
            {
                ViewBag.CarId = new SelectList(await _service.GetCars(), "CarId", "Name", driver.CarId);
            }

            if (driver.ColorId == null)
            {
                ViewBag.ColorId = new SelectList(await _service.GetAllColor(), "Id", "Name");
            }
            else
            {
                ViewBag.ColorId = new SelectList(await _service.GetAllColor(), "Id", "Name", driver.ColorId);
            }

            ViewBag.IsSuccess = false;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DriverCar(Guid id, DriverCarViewModel viewModel)
        {
            bool result = false;

            if (ModelState.IsValid)
            {

                result = _service.UpdateDriverCar(id, viewModel);

            }

            if (result)
            {
                ViewBag.IsSuccess = true;
            }
            else
            {
                ViewBag.IsSuccess = false;
            }

            ViewBag.CarId = new SelectList(await _service.GetCars(), "CarId", "Name", viewModel.CarId);
            ViewBag.ColorId = new SelectList(await _service.GetAllColor(), "Id", "Name", viewModel.ColorId);

            return View(viewModel);
        }
    }
}
