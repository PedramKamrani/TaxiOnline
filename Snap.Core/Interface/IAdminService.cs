using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap.Core.ViewModels.Admin;
using Snap.Data.Layer.Entities;

namespace Snap.Core.Interface
{
    public interface IAdminService:IDisposable
    {
        Task<List<CarViewModel>> GetCars();
        Task<CarViewModel?> GetCarById(Guid id);
        void AddCar(CarViewModel car);
        void UpdateCar(Guid id,CarViewModel car);
        bool DeleteCar(Guid id);
        void SaveAsync();
    }
}
