using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Snap.Core.Interface;
using Snap.Core.ViewModels;
using Snap.Core.ViewModels.Admin;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace Snap.Site.Controllers
{
    public class UserController : Controller
    {
        private readonly IAdminService _service;

        public UserController(IAdminService service)
        {
            StiLicense.LoadFromString("6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OJN40hxJjK5JbrxU+NrJ3E0OUAve6MDSIxK3504G4vSTqZezogz9ehm+xS8zUyh3tFhCWSvIoPFEEuqZTyO744uk+ezyGDj7C5jJQQjndNuSYeM+UdsAZVREEuyNFHLm7gD9OuR2dWjf8ldIO6Goh3h52+uMZxbUNal/0uomgpx5NklQZwVfjTBOg0xKBLJqZTDKbdtUrnFeTZLQXPhrQA5D+hCvqsj+DE0n6uAvCB2kNOvqlDealr9mE3y978bJuoq1l4UNE3EzDk+UqlPo8KwL1XM+o1oxqZAZWsRmNv4Rr2EXqg/RNUQId47/4JO0ymIF5V4UMeQcPXs9DicCBJO2qz1Y+MIpmMDbSETtJWksDF5ns6+B0R7BsNPX+rw8nvVtKI1OTJ2GmcYBeRkIyCB7f8VefTSOkq5ZeZkI8loPcLsR4fC4TXjJu2loGgy4avJVXk32bt4FFp9ikWocI9OQ7CakMKyAF6Zx7dJF1nZw");
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _service.GetUsers();
            return View(model);
        }

        #region Reports

        public IActionResult ShowPrint()
        {
            return View("ShowPrint");
        }

        public IActionResult Print()
        {
            StiReport report = new StiReport();

            report.Load(StiNetCoreHelper.MapPath(this, "wwwroot/reports/Report2.mrt"));

            var users = _service.GetUsers().Result;

            List<Report2ViewModel> report2s = new List<Report2ViewModel>();

            foreach (var item in users)
            {
                Report2ViewModel report2 = new Report2ViewModel()
                {
                    NationalCode = "ندارد",
                    BirthDate = item.UserDetail.BirthDate,
                    FullName = item.UserDetail.Fullname,
                    Username = item.UserName
                };

                report2s.Add(report2);
            }

            report.RegData("dt2", report2s);
            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

        #endregion

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
