namespace Carpet.Services.Data.CustomerService
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public interface ICustomersService
    {
        IQueryable<TViewModel> GetAllCustomersAsync<TViewModel>();

        Task<bool> IsCustomerExistAsync(string id);

        Task<CustomerCreateViewModel> CreateAsync(CustomerCreateInputModel customerFromView);

        Task<CustomerAddAddressViewModel> AddAddressToCustomerAsync(CustomerAddAddressInputModel customerFromView);

        Task<TViewModel> GetByIdAsync<TViewModel>(string id);

        Task<CustomerEditViewModel> EditByIdAsync(string id, CustomerEditInputModel customerFromView);

        Task<CustomerDeleteViewModel> DeleteByIdAsync(string id);
    }
}
