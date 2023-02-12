using Snap.Core.ViewModels;
using Snap.Data.Layer.Entities;

namespace Snap.Core.Interface
{
    public interface IPanelService : IDisposable
    {
        Task<UserDetail> GetUserDetailsAsync(string userName);
        string GetRoleName(string userName);
        bool UpdateUserDetailsProfile(Guid id, UserDetailProfileViewModel viewModel);

        #region Payment
        void AddFactor(Factor factor);
        bool UpdateFactor(Guid userid, string orderNumber, long price);
        Guid GetFactorById(string orderNumber);
        void UpdatePayment(Guid id, string date, string time, string desc, string bank, string trace, string refId);
        Task<Factor> GetFactor(Guid id);
        #endregion

        #region Price
        long GetPriceType(double id);
        float GetTempPercent(double id);
        float GetHumidityPercent(double id);

        #endregion
    }
}
