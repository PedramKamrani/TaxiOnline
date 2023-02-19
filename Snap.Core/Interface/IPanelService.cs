using Microsoft.EntityFrameworkCore;
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

        #region Transact

        void AddTransact(Transact viewModel);
        void UpdatePayment(Guid id);
        void UpdateRate(Guid id, int rate);
        Task<Transact> GetTransactById(Guid id);
        Task<List<Transact>> GetUserTransacts(Guid id);
        Task<List<Transact>> GetDriverTransacts(Guid id);
        void UpdateDriver(Guid id, Guid driverId);
        void UpdateDriverRate(Guid id, bool rate);
        void UpdateStatus(Guid id, int status);
        public void UpdateStatus(Guid id, Guid? driverId, int status);
        #endregion

        Guid GetUserId(string? identityName);
        User GetUser(string? identityName);
        Guid? ExistsUserTransact(Guid userId);
        Transact GetUserTransact(Guid transactId);
        List<Transact> GetTransactsNotAccept();
        User GetUserById(Guid id);
        

        #region Request

        Transact GetDriverConfrimTransact(Guid id);
        Transact GetUserConfrimTransact(Guid id);
        void EndRequest(Guid id);
        void AddRate(Guid id, int rate);

        #endregion
    }
}
