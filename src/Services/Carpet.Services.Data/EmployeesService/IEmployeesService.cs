namespace Carpet.Services.Data.EmployeesService
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.InputModels.Administration.Employees.Delete;
    using Carpet.Web.InputModels.Administration.Employees.Edit;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Employees.Edit;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public interface IEmployeesService
    {
        IQueryable<TViewModel> GetAllAsync<TViewModel>();

        Task<EmployeeCreateViewModel> CreateAsync(EmployeeCreateInputModel employeeFromView, ModelStateDictionary modelState);

        Task<TViewModel> GetByIdAsync<TViewModel>(string id);

        Task<EmployeeCreateViewModel> GetNotHiredUserAsync(string id);

        Task<EmployeeEditViewModel> EditByIdAsync(string id, EmployeeEditInputModel employeeFromView, ModelStateDictionary modelState);

        Task<EmployeeDeleteViewModel> DeleteByIdAsync(string id);

        Task<string> GetIdByUserNameAsync(string name);
    }
}
