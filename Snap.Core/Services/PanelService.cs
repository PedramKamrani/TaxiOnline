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

        #region Transact

        public  void AddTransact(Transact viewModel)
        {
            
            
            _context.Add(viewModel);
            _context.SaveChanges();
        }

        public void UpdatePayment(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRate(Guid id, int rate)
        {
            Transact transact = _context.Transacts.Find(id);

            transact.Rate = rate;
            _context.SaveChanges();
        }

        public async Task<Transact> GetTransactById(Guid id)
        {
            return await _context.Transacts.FindAsync(id);
        }

        public async Task<List<Transact>> GetUserTransacts(Guid id)
        {
            return await _context.Transacts.Where(x => x.UserId == id && x.Status == 2).OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<Transact>> GetDriverTransacts(Guid id)
        {
            return await _context.Transacts.Where(x => x.DriverId == id && x.Status == 2).OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public void UpdateDriver(Guid id, Guid driverId)
        {
            Transact transact = _context.Transacts.Find(id);

            transact.DriverId = driverId;
            _context.SaveChanges();
        }

        public void UpdateDriverRate(Guid id, bool rate)
        {
            Transact transact = _context.Transacts.Find(id);

            transact.DriverRate = rate;
            _context.SaveChanges();
        }

        public void UpdateStatus(Guid id, int status)
        {
            Transact transact = _context.Transacts.Find(id);

            transact.Status = status;
            _context.SaveChanges();
        }

        public void UpdateStatus(Guid id, Guid? driverId, int status)
        {
            Transact transact = _context.Transacts.Find(id);

            transact.Status = status;

            if (driverId != null)
            {
                transact.DriverId = driverId;
            }

            _context.SaveChanges();
        }

        public Guid GetUserId(string? identityName)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == identityName).Id;
        }

        public User GetUser(string? identityName)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == identityName);
        }

        public Guid? ExistsUserTransact(Guid userId)
        {
            // 0 == Create
            // 1 == Driver
            // 2 == Success
            // 3 == Cancel

            Transact transact = _context.Transacts.FirstOrDefault(x => x.UserId == userId && (x.Status == 0 || x.Status == 1));

            if (transact != null)
            {
                return transact.Id;
            }
            else
            {
                return null;
            }
        }

        public Transact GetUserTransact(Guid transactId)
        {
            return _context.Transacts.Find(transactId);
        }


        public List<Transact> GetTransactsNotAccept()
        {
            return _context.Transacts.Where(x => x.Status == 0).OrderByDescending(x => x.Date).ToList();
        }
        public User GetUserById(Guid id)
        {
            return _context.Users.Include(x => x.UserDetail).FirstOrDefault(x => x.Id == id);
        }

        #endregion

        #region Request

        public Transact GetDriverConfrimTransact(Guid id)
        {
            return _context.Transacts.FirstOrDefault(x => x.DriverId == id && x.Status == 1);
        }

        public Transact GetUserConfrimTransact(Guid id)
        {
            return _context.Transacts.FirstOrDefault(x => x.UserId == id && x.Status == 1);
        }

        public void EndRequest(Guid id)
        {
            Transact transact = _context.Transacts.Find(id);

            if (transact.IsCash == false)
            {
                User user = _context.Users.Find(transact.UserId);

                user.Wallet -= transact.Total;

            }

            transact.Status = 2;
            _context.SaveChanges();
        }

        public void AddRate(Guid id, int rate)
        {
            Transact transact = _context.Transacts.Find(id);

            transact.Rate = rate;
            _context.SaveChanges();
        }

        #endregion
    }
}
