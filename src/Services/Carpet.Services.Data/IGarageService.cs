namespace Carpet.Services.Data
{
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Garage;
    using Carpet.Web.ViewModels.Administration.Garage.Add;
    using Carpet.Web.ViewModels.Administration.Garage.Remove;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public interface IGarageService
    {
        Task<EmployeeAddViewModel> GetByIdAsync(string id, ModelStateDictionary modelState);

        Task<EmployeeAddViewModel> SetVehicleToEmployeeAsync(EmployeeAddInputModel employeeFromView, ModelStateDictionary modelState);

        Task<bool> RemoveVehicleToEmployeeAsync(EmployeeRemoveInputModel employeeFromView);
    }
}
