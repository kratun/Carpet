namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public interface IEmployeesService
    {
        IQueryable<TViewModel> GetAllAsync<TViewModel>();

        Task<EmployeeCreateViewModel> CreateAsync(EmployeeCreateInputModel employeeFromView, ModelStateDictionary modelState);

        Task<TViewModel> GetByIdAsync<TViewModel>(string id);

        Task<EmployeeCreateViewModel> GetNotHiredUserAsync(string id);

        //Task<EmployeeEditViewModel> EditByIdAsync(string id, EmployeeEditInputModel customerFromView, ModelStateDictionary modelState);

        //Task<EmployeeDeleteViewModel> DeleteByIdAsync(string id);
    }
}
