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

        #region Humidity

        Task<List<Humidity>> GetHumiditys();

        Task<Humidity> GetHumidityById(Guid id);

        void AddHumidity(MonthTypeViewModel viewModel);

        bool UpdateHumidity(MonthTypeViewModel viewModel, Guid id);

        void DeleteHumidity(Guid id);

        #endregion

        #region Temperature

        Task<List<Temperature>> GetTemperatures();

        Task<Temperature> GetTemperatureById(Guid id);

        void AddTemperature(MonthTypeViewModel viewModel);

        bool UpdateTemperature(MonthTypeViewModel viewModel, Guid id);

        void DeleteTemperature(Guid id);

        #endregion


        #region Role

        Task<List<Role>> GetRoles();

        Task<Role> GetRoleById(Guid id);

        void AddRole(RoleViewModel viewModel);

        bool UpdateRole(RoleViewModel viewModel, Guid id);

        void DeleteRole(Guid id);

        #endregion

        #region User

        bool CheckUserName(string userName);
        void AddUser(UserViewModel viewModel);
        string GetRoleName(Guid roleId);

        Task<List<User>> GetUsers();
        void DeleteUser(Guid id);
        Task<UserEditViewModel> GetUserForUpdateById(string userName);
        Task<bool> EditUser(UserEditViewModel viewModel, Guid id);
       Task<Guid> GetRoleIdByRoleName(string roleName);
       Task<Guid> GetUserIdByUserName(string userName);
       Task<Driver> GetDriverById(Guid userid);
      Task<bool> UserDriverProp(Guid id, DriverPropViewModel viewModel);
      Task<Driver> GetDriver(Guid id);
      bool UpdateDriverCertificate(Guid id, DriverImgViewModel viewModel);
      bool UpdateDriverCar(Guid id, DriverCarViewModel viewModel);
        #endregion

       

        #region Discount

        Task<List<Discount>> GetDiscounts();

        Task<Discount> GetDiscountById(Guid id);

        void AddDiscount(AdminDiscountViewModel viewModel);

        bool UpdateDiscount(AdminDiscountViewModel viewModel, Guid id);

        void DeleteDiscount(Guid id);

        #endregion

        #region Report

        Task<int> WeeklyFactor(string date);

        int MonthlyFactor(string month);
        int WeeklyRegister(string strEndDate);
        int MonthlyRegister(string strMonth);
        #endregion

        #region Transact

        Task<List<Transact>> GetAllTransact();
        void DeleteTransact(Guid id);
        Task<List<TransactRate>> GetAllTransactRate(Guid id);

        #endregion



    }

}
