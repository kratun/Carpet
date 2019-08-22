namespace Carpet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Garage;
    using Carpet.Web.ViewModels.Administration.Garage.Add;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IGarageService
    {
        Task<EmployeeAddViewModel> GetByIdAsync(string id, ModelStateDictionary modelState);

        Task<EmployeeAddViewModel> SetVehicleToEmployeeAsync(EmployeeAddInputModel employeeFromView, ModelStateDictionary modelState);

        Task<bool> RemoveVehicleToEmployeeAsync(EmployeeRemoveInputModel employeeFromView);

        Task<List<SelectListItem>> GetVehicleNames();
    }
}
