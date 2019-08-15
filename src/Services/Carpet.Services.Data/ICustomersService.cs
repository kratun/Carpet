namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public interface ICustomersService
    {
        IQueryable<TViewModel> GetAllCustomersAsync<TViewModel>();

        Task<CustomerCreateViewModel> CreateAsync(CustomerCreateInputModel customerFromView, ModelStateDictionary modelState);

        Task<TViewModel> GetByIdAsync<TViewModel>(string id);

        Task<CustomerEditViewModel> EditByIdAsync(string id, CustomerEditInputModel customerFromView, ModelStateDictionary modelState);

        Task<CustomerDeleteViewModel> DeleteByIdAsync(string id);
    }
}
