using Microsoft.EntityFrameworkCore;
using Snap.Core.Interface;
using Snap.Core.ViewModels;
using Snap.Data.Layer.Context;
using Snap.Data.Layer.Entities;

namespace Snap.Core.Services
{
    public class PanelService : IPanelService
    {
        private readonly DatabaseContext _context;

        public PanelService(DatabaseContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<UserDetail> GetUserDetailsAsync(string userName)
        {
            return await _context.UserDetails.Include(c => c.User)
                .SingleOrDefaultAsync(c => c.User.UserName == userName);
        }

        public string GetRoleName(string userName)
        {
            return _context.Users.Include(x => x.Role)
                .SingleOrDefault(c => c.UserName == userName)?.Role.Name??"";
        }
        public bool UpdateUserDetailsProfile(Guid id, UserDetailProfileViewModel viewModel)
        {
            UserDetail user = _context.UserDetails.Find(id);

            if (user != null)
            {
                user.Fullname = viewModel.FullName;
                user.BirthDate = viewModel.BirthDate;

                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
