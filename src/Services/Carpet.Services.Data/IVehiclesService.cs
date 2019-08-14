namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Vehicles;
    using Carpet.Web.ViewModels.Administration.Vehicles;

    public interface IVehiclesService
    {
        IQueryable<TViewModel> GetAllAsync<TViewModel>();

        Task<VehicleIndexViewModel> CreateAsync(VehicleCreateInputModel vehicleFromView);

        Task<TViewModel> GetByIdAsync<TViewModel>(int id);

        Task<VehicleEditViewModel> EditByIdAsync(int id, VehicleEditInputModel vehicleFromView);

        Task<VehicleDeleteViewModel> DeleteByIdAsync(int id);
    }
}
