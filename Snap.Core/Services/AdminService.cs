using Microsoft.EntityFrameworkCore;
using Snap.Core.Generators;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Admin;
using Snap.Data.Layer.Context;
using Snap.Data.Layer.Entities;
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
        { _context.SaveChanges();
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
                entity.Name=settings.Name;
                entity.Description = settings.Desc;
                entity.Tel = settings.Tel;
                entity.Fax=settings.Fax;
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

        public async Task<bool>  DeletePriceType(Guid id)
        {
            var entity =(PriceType)await _context.PriceTypes.FindAsync(id);
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
                End =colorAdminViewModel.End,
                Start = colorAdminViewModel.Start,
                Id = CodeGenerators.GetId(),
                Precent = colorAdminViewModel.End
            };
            _context.Add(model);
            SaveAsync();
        }

        public bool UpdateMonthType(MonthTypeViewModel viewModel, Guid id)
        {
            var entity=_context.MonthTypes.Find(id);
            var model = new MonthType
            {
               End = viewModel.End,
               Start = viewModel.Start,
               Name = viewModel.Name,
               Precent = viewModel.Percent,
               Id = entity.Id
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






        #region Dispose
        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion



    }
}
