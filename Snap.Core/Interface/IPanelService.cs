using Snap.Core.ViewModels;
using Snap.Data.Layer.Entities;

namespace Snap.Core.Interface
{
    public interface IPanelService : IDisposable
    {
        Task<UserDetail> GetUserDetailsAsync(string userName);
        string GetRoleName(string userName);
        bool UpdateUserDetailsProfile(Guid id, UserDetailProfileViewModel viewModel);
    }
}
