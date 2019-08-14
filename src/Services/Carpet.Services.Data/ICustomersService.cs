namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Customers;

    public interface ICustomersService
    {
        IQueryable<TViewModel> GetAllCustomersAsync<TViewModel>();

        Task<CustomerIndexViewModel> CreateAsync(CustomerCreateInputModel customerFromView);

        Task<TViewModel> GetByIdAsync<TViewModel>(string id);

        Task<CustomerEditViewModel> EditByIdAsync(string id, CustomerEditInputModel customerFromView);

        Task<CustomerDeleteViewModel> DeleteByIdAsync(string id);
    }
}
