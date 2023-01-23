using Microsoft.EntityFrameworkCore;
using Snap.Core.Generators;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Admin;
using Snap.Data.Layer.Context;
using Snap.Data.Layer.Entities;

namespace Snap.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly DatabaseContext _context;

       
        public AdminService(DatabaseContext context)
        {
            _context = context;
        }

        #region Car
        public async Task<List<CarViewModel>> GetCars()
        {
            var res= await _context.Car.Select(c => new CarViewModel
            {
                CarId = c.Id,
                Name = c.Name
            }).OrderBy(x=>x.Name).ToListAsync();
            return res;
        }

        public async Task<CarViewModel?> GetCarById(Guid id)
        {
            return await _context.Car.Select(x=>new CarViewModel
            {
                CarId = x.Id,
                Name = x.Name
            }).FirstOrDefaultAsync(x => x.CarId == id);
        }

        public void AddCar(CarViewModel car)
        {
            var model = new Car
            {
                Id = CodeGenerators.GetId(),
                Name = car.Name
            };
            _context.Add(model);
            SaveAsync();
        }

        public void UpdateCar(Guid id,CarViewModel car)
        {
           var entity=_context.Car.Find(id);
           if (entity != null)
           {
               entity.Name = car.Name;
               _context.Update(entity);
           }

           SaveAsync();
        }

        public bool DeleteCar(Guid id)
        {
            var entity = _context.Car.Find(id);
            _context.Remove(entity);
            SaveAsync();
            return true;
        }

        public void SaveAsync()
        {
            _context.SaveChangesAsync();
        }
#endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
