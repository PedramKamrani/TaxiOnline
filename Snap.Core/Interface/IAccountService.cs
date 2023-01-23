using Snap.Core.ViewModels;
using Snap.Data.Layer.Entities;

namespace Snap.Core.Interface
{
    public interface IAccountService:IDisposable
    {
        Task<bool> CheckExistsUserAsync(RegisterViewModel registerViewModel);
        Task<User?> RegisterAsync(RegisterViewModel registerViewModel);
        Task<Guid> GetRoleIdAsync(string roleName);
        Task<User?> ActiveUser(ActiveCodeViewModel activeCodeViewModel);
        Task<User?> RegisterDriverAsync(RegisterViewModel registerViewModel);
    }
}
