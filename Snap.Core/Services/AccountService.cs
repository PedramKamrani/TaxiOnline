using System.CodeDom.Compiler;
using Microsoft.EntityFrameworkCore;
using Snap.Core.Generators;
using Snap.Core.Interface;
using Snap.Core.Securities;
using Snap.Core.Senders;
using Snap.Core.ViewModels;
using Snap.Data.Layer.Context;
using Snap.Data.Layer.Entities;

namespace Snap.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext _context;

        public AccountService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckExistsUserAsync(RegisterViewModel registerViewModel)
        {
            return await _context.Users.AnyAsync(x => x.UserName == registerViewModel.Username);
        }

        public async Task<User?> GetUserByAsync(RegisterViewModel registerViewModel)
        {
            var user= await _context.Users.FirstOrDefaultAsync(x => x.UserName == registerViewModel.Username);
            return user;
        }
        public async Task<Guid> GetRoleIdAsync(string rolename)
        {
            var role= await _context.Roles.SingleOrDefaultAsync(x => x.Name ==rolename);
            if (role != null) return role.Id;
            return Guid.NewGuid();
        }
        
        public async Task<User?> RegisterAsync(RegisterViewModel registerViewModel)
        {
            //string codeHash = HashEncode.GetHashCode(HashEncode.GetHashCode( CodeGenerators.GetActiveCode()));
            string code = CodeGenerators.GetActiveCode();
            
            if (!await CheckExistsUserAsync(registerViewModel))
            {
                User? user = new User
                {
                    IsActive = false,
                    UserName = registerViewModel.Username,
                    Password =code  ,
                    RoleId = await GetRoleIdAsync("user"),
                    Token = "",
                    Id = CodeGenerators.GetId()
                };
               await _context.AddAsync(user);
            
                UserDetail userDetail = new UserDetail
                {
                    UserId = user.Id,
                    BirthDate = "",
                    Date = DateTimeGenerators.ShamsiDate(),
                    Time = DateTimeGenerators.ShamsiTime(),
                    Fullname = ""
                };
               await _context.AddAsync(userDetail);
               await _context.SaveChangesAsync();
               return user;
            }
            Sms(registerViewModel.Username,code);
            return await GetUserByAsync(registerViewModel);
        }


        public async Task<User?> ActiveUser(ActiveCodeViewModel activeCodeViewModel)
        {
            //var password = HashEncode.GetHashCode(HashEncode.GetHashCode(activeCodeViewModel.Code));
            User? user = await _context.Users.SingleOrDefaultAsync(x => x != null && x.Password == activeCodeViewModel.Code && x.UserName == activeCodeViewModel.UserName);
            if (user != null)
            {
                user.IsActive = true;
                user.Password = CodeGenerators.GetActiveCode();
                await _context.SaveChangesAsync();
            }

            return await Task.FromResult(user);
        }

        public async Task<User?> RegisterDriverAsync(RegisterViewModel registerViewModel)
        {
            var code = CodeGenerators.GetActiveCode();
            if (!await CheckExistsUserAsync(registerViewModel))
            {
                User user = new User
                {
                    UserName = registerViewModel.Username,
                    IsActive = false,
                    Id = CodeGenerators.GetId(),
                    Wallet = 0,
                    RoleId =await GetRoleIdAsync("driver"),
                    Token = "",
                    Password = code
                };
                _context.Add(user);
                UserDetail userDetail = new UserDetail
                {
                    UserId = user.Id,
                    BirthDate = "",
                    Date = DateTimeGenerators.ShamsiDate(),
                    Time = DateTimeGenerators.ShamsiTime(),
                    Fullname = ""
                };
                _context.Add(userDetail);
                Driver driver = new Driver
                {
                    UserId = user.Id,
                    IsConfirm = false,
                    Address = "",
                    Avatar = "",
                    Code = "",
                    Telephone = "",
                    NationalCode = "",
                    Image = "",
                    
                };
                _context.Add(driver);
               await _context.SaveChangesAsync();
               Sms(registerViewModel.Username,code);
               return user;
            }
            Sms(registerViewModel.Username,code);
            return new User();
        }

        private void Sms(string username,string code)
        {
            try
            {
                SmsSender.VerifySender(username,"RegisterVerify",code) ;
            }
            catch
            {
               
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
