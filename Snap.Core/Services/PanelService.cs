using Microsoft.EntityFrameworkCore;
using Snap.Core.Generators;
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
                .SingleOrDefault(c => c.UserName == userName)?.Role.Name ?? "";
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

        #region Payment

        public void AddFactor(Factor factor)
        {
            _context.Add(factor);
            _context.SaveChanges();
        }

        public bool UpdateFactor(Guid userid, string orderNumber, long price)
        {
            var factor = _context.Factors.SingleOrDefault(c => c.UserId == userid && c.BankName == null);
            if (factor != null)
            {
                factor.OrderNumber = orderNumber;
                factor.Price = Convert.ToInt32(price);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public Guid GetFactorById(string orderNumber)
        {
            return _context.Factors.SingleOrDefault(x => x.OrderNumber == orderNumber).Id;
        }

        public void UpdatePayment(Guid id, string date, string time, string desc, string bank, string trace, string refId)
        {
            Factor factor = _context.Factors.Find(id);
            User user = _context.Users.Find(factor.UserId);
            factor.Date = DateTimeGenerators.ShamsiDate();
            factor.Time = DateTimeGenerators.ShamsiTime();
            factor.Desc = desc;
            factor.TraceNumber = trace;
            factor.BankName = bank;
            factor.RefNumber = refId;
            user.Wallet += factor.Price;
            _context.SaveChanges();
        }

        public async Task<Factor> GetFactor(Guid id)
        {
            return await _context.Factors.FindAsync(id);
        }


        #endregion

        #region Price

        public long GetPriceType(double id)
        {
            var priceType = _context.PriceTypes.FirstOrDefault(x => x.Start >= id && x.End <= id);
            if (priceType == null)
                return 0;
            return priceType.Price;
        }
        public float GetTempPercent(double id)
        {
            var temp = _context.Temperatures.FirstOrDefault(x => x.End >= id && x.Start <= id);

            if (temp == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToSingle(temp.Precent / 100);
            }
        }
        public float GetHumidityPercent(double id)
        {
            var hum = _context.Humidities.FirstOrDefault(x => x.End >= id && x.Start <= id);

            if (hum == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToSingle(hum.Precent / 100);
            }
        }

        #endregion
    }
}
