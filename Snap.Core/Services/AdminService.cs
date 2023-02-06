using Microsoft.EntityFrameworkCore;
using Snap.Core.Generators;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Admin;
using Snap.Data.Layer.Context;
using Snap.Data.Layer.Entities;
using Snap.Data.Layer.Migrations;
using RateType = Snap.Data.Layer.Entities.RateType;

namespace Snap.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly DatabaseContext _context;


        public AdminService(DatabaseContext context)
        {
            _context = context;
        }

        #region Car

        public async Task<List<CarViewModel>> GetCars()
        {
            var res = await _context.Car.Select(c => new CarViewModel
            {
                CarId = c.Id,
                Name = c.Name
            }).OrderBy(x => x.Name).ToListAsync();
            return res;
        }

        public async Task<CarViewModel?> GetCarById(Guid id)
        {
            return await _context.Car.Select(x => new CarViewModel
            {
                CarId = x.Id,
                Name = x.Name
            }).FirstOrDefaultAsync(x => x.CarId == id);
        }

        public void AddCar(CarViewModel car)
        {
            var model = new Car
            {
                Id = CodeGenerators.GetId(),
                Name = car.Name
            };
            _context.Add(model);
            SaveAsync();
        }

        public void UpdateCar(Guid id, CarViewModel car)
        {
            var entity = _context.Car.Find(id);
            if (entity != null)
            {
                entity.Name = car.Name;
                _context.Update(entity);
            }

            SaveAsync();
        }

        public bool DeleteCar(Guid id)
        {
            var entity = _context.Car.Find(id);
            _context.Remove(entity);
            SaveAsync();
            return true;
        }

        public void SaveAsync()
        {
            _context.SaveChanges();
        }

        #endregion

        #region Color

        public async Task<List<Color?>> GetAllColor()
        {
            return await _context.Colors.ToListAsync();
        }

        public async Task<Color?> GetColorById(Guid id)
        {
            return await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void AddColor(ColorAdminViewModel colorAdminViewModel)
        {
            var model = new Color
            {
                Name = colorAdminViewModel.Name,
                Code = colorAdminViewModel.Code,
                Id = CodeGenerators.GetId()
            };
            _context.Add(model);
            SaveAsync();
        }

        public bool UpdateColor(ColorAdminViewModel viewModel, Guid id)
        {

            var model = new Color
            {
                Name = viewModel.Name,
                Code = viewModel.Code,
                Id = viewModel.Id
            };
            _context.Update(model);
            SaveAsync();
            return true;

        }


        public async Task<bool> DeleteColor(Guid id)
        {
            var entity = await GetColorById(id);
            if (entity != null)
            {
                _context.Remove(entity);
                SaveAsync();
                return true;
            }

            return false;

        }

        #endregion

        #region Rate

        public async Task<List<RateType>> GetRateTypes()
        {
            return await _context.RateTypes.ToListAsync();
        }

        public async Task<RateType> GetRateTypeById(Guid id)
        {
            return await _context.RateTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void AddRateType(RateTypeViewModel viewModel)
        {
            var model = new RateType
            {
                Id = CodeGenerators.GetId(),
                Name = viewModel.Name,
                OrderView = viewModel.OrderView,
                OK = viewModel.OK
            };
            _context.Add(model);
            SaveAsync();
        }

        public async Task<bool> UpdateRate(Guid id, RateTypeViewModel model)
        {
            var entity = await GetRateTypeById(id);
            _context.Update(entity);
            SaveAsync();
            return true;
        }

        public async Task<bool> DeleteRateType(Guid id)
        {
            var entity = await GetRateTypeById(id);
            _context.Remove(entity);
            SaveAsync();
            return true;
        }



        #endregion

        #region Settings

        public async Task<Settings> GetSiteSetting()
        {
            return await _context.Settings.FirstOrDefaultAsync();
        }

        public bool UpdateSetting(SiteSettingViewModel settings)
        {
            var entity = _context.Settings.FirstOrDefault();
            if (entity != null)
            {
                entity.Name = settings.Name;
                entity.Description = settings.Desc;
                entity.Tel = settings.Tel;
                entity.Fax = settings.Fax;
                _context.Update(entity);
                SaveAsync();
                return true;
            }

            return false;
        }

        public bool UpdatePriceSetting(PriceSettingViewModel viewModel)
        {
            Settings setting = _context.Settings.FirstOrDefault();

            if (setting != null)
            {
                setting.IsDistance = viewModel.IsDistance;
                setting.IsWeatherPrice = viewModel.IsWeatherPirce;

                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateAboutSetting(AboutSettingViewModel viewModel)
        {
            Settings setting = _context.Settings.FirstOrDefault();

            if (setting != null)
            {
                setting.About = viewModel.About;

                _context.SaveChanges();

                return true;
            }

            return false;

        }

        public bool UpdateTermsSetting(TermsSettingViewModel viewModel)
        {
            Settings setting = _context.Settings.FirstOrDefault();

            if (setting == null) return false;
            setting.Trems = viewModel.Terms;

            _context.SaveChanges();

            return true;

        }



        #endregion

        #region PriceType

        public async Task<List<PriceType>> GetPricesType()
        {
            return await _context.PriceTypes.ToListAsync();
        }

        public async Task<PriceType> GetPricesBy(Guid id)
        {
            return await _context.PriceTypes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void AddPrice(PriceTypeViewModel priceTypeViewModel)
        {
            var model = new PriceType
            {
                Name = priceTypeViewModel.Name,
                Id = CodeGenerators.GetId(),
                Price = priceTypeViewModel.Price,
                End = priceTypeViewModel.End,
                Start = priceTypeViewModel.Start
            };
            _context.Add(model);
            SaveAsync();
        }

        public async Task<bool> EditPriceType(Guid id, PriceTypeViewModel priceTypeViewModel)
        {
            var entity = await GetPricesBy(id);
            if (entity == null) return false;
            var model = new PriceType
            {
                Price = priceTypeViewModel.Price,
                End = priceTypeViewModel.End,
                Start = priceTypeViewModel.Start,
                Name = priceTypeViewModel.Name,
                Id = id

            };
            _context.PriceTypes.Update(model);
            SaveAsync();
            return true;
        }

        public async Task<bool> DeletePriceType(Guid id)
        {
            var entity = (PriceType)await _context.PriceTypes.FindAsync(id);
            _context.Remove(entity);
            SaveAsync();
            return true;
        }

        #endregion

        #region MonthType

        public async Task<List<MonthType>> GetMonthTypes()
        {
            return await _context.MonthTypes.ToListAsync();
        }

        public async Task<MonthType> GetMonthTypeById(Guid id)
        {
            return await _context.MonthTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void AddMonthType(MonthTypeViewModel colorAdminViewModel)
        {
            var model = new MonthType
            {
                Name = colorAdminViewModel.Name,
                End = colorAdminViewModel.End,
                Start = colorAdminViewModel.Start,
                Id = CodeGenerators.GetId(),
                Precent = colorAdminViewModel.Percent
            };
            _context.Add(model);
            SaveAsync();
        }

        public bool UpdateMonthType(MonthTypeViewModel viewModel, Guid id)
        {
            var entity = _context.MonthTypes.AsNoTracking().FirstOrDefault(x => x.Id == id);
            var model = new MonthType
            {
                End = viewModel.End,
                Start = viewModel.Start,
                Name = viewModel.Name,
                Precent = viewModel.Percent,
                Id = id
            };
            _context.Update(model);
            SaveAsync();
            return true;

        }


        public void DeleteMonthType(Guid id)
        {
            var entity = _context.MonthTypes.Find(id);

            _context.Remove((object)entity);
            SaveAsync();


        }

        #endregion


        #region Humidity


        public async Task<List<Humidity>> GetHumiditys()
        {
            return await _context.Humidities.OrderBy(h => h.Name).ToListAsync();
        }

        public async Task<Humidity> GetHumidityById(Guid id)
        {
            return await _context.Humidities.FindAsync(id);
        }

        public void AddHumidity(MonthTypeViewModel viewModel)
        {
            Humidity humidity = new Humidity()
            {
                End = viewModel.End,
                Id = CodeGenerators.GetId(),
                Name = viewModel.Name,
                Precent = viewModel.Percent,
                Start = viewModel.Start
            };

            _context.Humidities.Add(humidity);
            _context.SaveChanges();
        }

        public bool UpdateHumidity(MonthTypeViewModel viewModel, Guid id)
        {
            Humidity humidity = _context.Humidities.Find(id);

            if (humidity != null)
            {
                humidity.End = viewModel.End;
                humidity.Name = viewModel.Name;
                humidity.Precent = viewModel.Percent;
                humidity.Start = viewModel.Start;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public void DeleteHumidity(Guid id)
        {
            Temperature temperature = _context.Temperatures.Find(id);

            if (temperature != null)
            {
                _context.Temperatures.Remove(temperature);
                _context.SaveChanges();
            }
        }

        #endregion

        #region Temperatures

        public async Task<List<Temperature>> GetTemperatures()
        {
            return await _context.Temperatures.OrderBy(h => h.Name).ToListAsync();
        }

        public async Task<Temperature> GetTemperatureById(Guid id)
        {
            return await _context.Temperatures.FindAsync(id);
        }

        public void AddTemperature(MonthTypeViewModel viewModel)
        {
            Temperature temperature = new Temperature()
            {
                End = viewModel.End,
                Id = CodeGenerators.GetId(),
                Name = viewModel.Name,
                Precent = viewModel.Percent,
                Start = viewModel.Start
            };

            _context.Temperatures.Add(temperature);
            _context.SaveChanges();
        }

        public bool UpdateTemperature(MonthTypeViewModel viewModel, Guid id)
        {
            Temperature temperature = _context.Temperatures.Find(id);

            if (temperature != null)
            {
                temperature.End = viewModel.End;
                temperature.Name = viewModel.Name;
                temperature.Precent = viewModel.Percent;
                temperature.Start = viewModel.Start;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public void DeleteTemperature(Guid id)
        {
            Temperature temperature = _context.Temperatures.Find(id);

            if (temperature != null)
            {
                _context.Temperatures.Remove(temperature);
                _context.SaveChanges();
            }
        }

        #endregion

        #region Role

        public async Task<List<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(Guid id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public void AddRole(RoleViewModel viewModel)
        {
            var model = new Role
            {
                Id = CodeGenerators.GetId(),
                Name = viewModel.Name,
                Title = viewModel.Title
            };
            _context.Add(model);
            SaveAsync();
        }

        public bool UpdateRole(RoleViewModel viewModel, Guid id)
        {
            var entity = _context.Roles.Find(id);
            if (entity != null)
            {
                entity.Title = viewModel.Title;
                entity.Name = viewModel.Name;
                SaveAsync();
            }

            return false;
        }

        public void DeleteRole(Guid id)
        {
            var entity = _context.Roles.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                SaveAsync();
            }
        }

        #endregion

        #region User

        public bool CheckUserName(string userName)
        {
            return _context.Users.Any(x => x.UserName == userName);
        }

        public void AddUser(UserViewModel viewModel)
        {
            if (!CheckUserName(viewModel.Username))
            {
                var User = new User()
                {
                    Id = CodeGenerators.GetId(),
                    UserName = viewModel.Username,
                    IsActive = viewModel.IsActive,
                    RoleId = viewModel.RoleId,
                    Wallet = 0,
                    Password = CodeGenerators.GetActiveCode(),
                    Token = "",

                };
                _context.Users.Add(User);
                var userDetail = new UserDetail
                {
                    BirthDate = "",
                    Date = DateTimeGenerators.ShamsiDate(),
                    Time = DateTimeGenerators.ShamsiTime(),
                    Fullname = "",
                    UserId = User.Id,
                };
                _context.UserDetails.Add(userDetail);
                if (GetRoleName(viewModel.RoleId) == "driver")
                {
                    Driver driver = new Driver
                    {
                        UserId = User.Id,
                        Address = "",
                        Avatar = "",
                        Image = "",
                        Telephone = "",
                        IsConfirm = false,
                        NationalCode = "",
                        Code = CodeGenerators.GetActiveCode()
                    };
                    _context.Drivers.Add(driver);
                }

                SaveAsync();
            }

        }

        public string GetRoleName(Guid roleId)
        {
            return _context.Roles.Find(roleId)?.Name ?? "";
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.Include(x => x.Role).OrderBy(x => x.Id).ToListAsync();
        }

        public void DeleteUser(Guid id)
        {
            var entity = _context.Users.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                SaveAsync();
            }
        }

        public async Task<UserEditViewModel> GetUserForUpdateById(string userName)
        {
            var res = await _context.UserDetails.Include(x => x.User).ThenInclude(x => x.Driver)
                .Select(x => new UserEditViewModel
                {
                    BirthDate = x.BirthDate,
                    IsActive = x.User.IsActive,
                    Username = x.User.UserName,
                    FullName = x.Fullname,
                    RoleId = x.User.RoleId,
                })
                .FirstOrDefaultAsync(x => x.Username == userName);
            return res;
        }

        public async Task<bool> EditUser(UserEditViewModel viewModel, Guid id)
        {
            var user = GetUserForUpdateById(viewModel.Username).Result;
            var roleid = await GetRoleIdByRoleName("driver");
            Driver? driver;
            if (user.RoleId == roleid)
            {
                driver = new Driver
                {
                    IsConfirm = false,
                    Address = "",
                    Avatar = "",
                    UserId = id,
                    Image = "",
                    NationalCode = "",
                    Telephone = "",
                    Code = CodeGenerators.GetActiveCode()

                };
                _context.Drivers.Add(driver);
            }

            driver = await GetDriverById(id);
            _context.Drivers.Remove(driver);
            SaveAsync();
            return true;
        }

        public async Task<Guid> GetRoleIdByRoleName(string roleName)
        {
            return await _context.Roles.Where(x => x.Title == roleName).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<Guid> GetUserIdByUserName(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<Driver> GetDriverById(Guid userid)
        {
            return await _context.Drivers.Where(x => x.UserId == userid).FirstOrDefaultAsync();
        }

        public async Task<bool> UserDriverProp(Guid id, DriverPropViewModel viewModel)
        {
            if (viewModel.Avatar.FileName != null)
            {
                var strEx = Path.GetExtension(viewModel.Avatar.FileName);
                if (strEx != ".jpg")
                {
                    return false;
                }

                string filePath = "";
                viewModel.AvatarName = CodeGenerators.GetFileName() + strEx;
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/driver/", viewModel.AvatarName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Avatar.CopyTo(stream);
                }

                Driver driver = _context.Drivers.Find(id);
                if (driver != null)
                {
                    driver.Avatar = viewModel.AvatarName;
                    driver.Address = viewModel.Address;
                    driver.NationalCode = viewModel.NationalCode;
                    driver.Telephone = viewModel.Tel;
                }

                SaveAsync();
                return true;
            }

            return false;
        }

        public async Task<Driver> GetDriver(Guid id)
        {
            return await _context.Drivers.FindAsync(id);
        }

        public bool UpdateDriverCertificate(Guid id, DriverImgViewModel viewModel)
        {
            Driver driver = _context.Drivers.Find(id);

            if (viewModel.Img != null)
            {
                string strExt = Path.GetExtension(viewModel.Img.FileName);

                if (strExt != ".jpg")
                {
                    return false;
                }

                string filePath = "";

                viewModel.ImgName = CodeGenerators.GetFileName() + strExt;
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/driver/", viewModel.ImgName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Img.CopyTo(stream);
                }

                driver.Image = viewModel.ImgName;
                driver.IsConfirm = viewModel.IsConfirm;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UpdateDriverCar(Guid id, DriverCarViewModel viewModel)
        {
            Driver driver = _context.Drivers.Find(id);

            driver.Code = viewModel.CarCode;
            driver.CarId = viewModel.CarId;
            driver.ColorId = viewModel.ColorId;

            _context.SaveChanges();

            return true;
        }

        #endregion

        #region  Discount

        public async Task<List<Discount>> GetDiscounts()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<Discount> GetDiscountById(Guid id)
        {
            return await _context.Discounts.FindAsync(id);
        }

        public void AddDiscount(AdminDiscountViewModel viewModel)
        {
            var discount = new Discount
            {
                Price = viewModel.Price,
                Code = viewModel.Code,
                Desc = viewModel.Desc,
                Expire =   viewModel.Expire,
                Name = viewModel.Name,
                Start = viewModel.Start,
                Percent = viewModel.Percent,
                Id = CodeGenerators.GetId()
            };
            _context.Add(discount);
            _context.SaveChanges();
        }

        public bool UpdateDiscount(AdminDiscountViewModel viewModel, Guid id)
        {
            var discount = _context.Discounts.Find(id);

            if (discount == null) return false;
            
                discount.Code = viewModel.Code;
                discount.Start = viewModel.Start;
                discount.Expire = viewModel.Expire;
                discount.Desc = viewModel.Desc;
                discount.Name = viewModel.Name;
                discount.Percent = viewModel.Percent;
                discount.Price = viewModel.Price;
        
            _context.Discounts.Update(discount);
            _context.SaveChanges();
            return true;
        }

        public void DeleteDiscount(Guid id)
        {
            var entity=_context.Discounts.Find(id);

            if (entity == null)return;
            _context.Discounts.Remove(entity);
            _context.SaveChanges();

            
        }


        #endregion

        #region Dispose

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}