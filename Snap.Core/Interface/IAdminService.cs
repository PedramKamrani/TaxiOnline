using Snap.Core.ViewModels.Admin;
using Snap.Data.Layer.Entities;

namespace Snap.Core.Interface
{
    public interface IAdminService : IDisposable
    {
        #region Car
        Task<List<CarViewModel>> GetCars();
        Task<CarViewModel?> GetCarById(Guid id);
        void AddCar(CarViewModel car);
        void UpdateCar(Guid id, CarViewModel car);
        bool DeleteCar(Guid id);
        void SaveAsync();
        #endregion
        #region Color
        Task<List<Color?>> GetAllColor();
        Task<Color?> GetColorById(Guid id);
        void AddColor(ColorAdminViewModel colorAdminViewModel);
       bool UpdateColor(ColorAdminViewModel viewModel, Guid id);
        Task<bool> DeleteColor(Guid id);
        #endregion

        #region RateType

        Task<List<RateType>> GetRateTypes();
        Task<RateType> GetRateTypeById(Guid id);
        void AddRateType(RateTypeViewModel viewModel);
        Task<bool> UpdateRate(Guid id, RateTypeViewModel model);
        Task<bool> DeleteRateType(Guid id);

        #endregion

        #region Settings

        Task<Settings> GetSiteSetting();
        bool UpdateSetting(SiteSettingViewModel settings);
        bool UpdatePriceSetting(PriceSettingViewModel viewModel);
        bool UpdateAboutSetting(AboutSettingViewModel viewModel);

        bool UpdateTermsSetting(TermsSettingViewModel viewModel);




        #endregion

        #region PriceType

        Task<List<PriceType>> GetPricesType();
        Task<PriceType> GetPricesBy(Guid id);
        void AddPrice(PriceTypeViewModel priceTypeViewModel);
         Task<bool> EditPriceType(Guid id, PriceTypeViewModel priceSettingViewModel);
        Task<bool> DeletePriceType(Guid id);

        #endregion

        #region MonthType

        Task<List<MonthType>> GetMonthTypes();

        Task<MonthType> GetMonthTypeById(Guid id);

        void AddMonthType(MonthTypeViewModel viewModel);

        bool UpdateMonthType(MonthTypeViewModel viewModel, Guid id);

        void DeleteMonthType(Guid id);

        #endregion


    }

}
