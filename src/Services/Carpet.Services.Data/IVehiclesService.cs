namespace Carpet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Vehicles;
    using Carpet.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IVehiclesService
    {
        IQueryable<TViewModel> GetAllAsync<TViewModel>();

        Task<VehicleCreateViewModel> CreateAsync(VehicleCreateInputModel vehicleFromView, ModelStateDictionary modelState);

        Task<TViewModel> GetByIdAsync<TViewModel>(int id);

        Task<VehicleEditViewModel> EditByIdAsync(int id, VehicleEditInputModel vehicleFromView, ModelStateDictionary modelState);

        Task<VehicleDeleteViewModel> DeleteByIdAsync(int id);

        Task<List<SelectListItem>> GetVehicleNames();
    }
}
