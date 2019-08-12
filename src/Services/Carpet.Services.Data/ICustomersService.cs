namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.ViewModels.Customers;

    public interface ICustomersService
    {
        IQueryable<TViewModel> GetAllCustomersAsync<TViewModel>();

        Task<CustomerIndexViewModel> CreateAsync(CustomerCreateInputModel customerFromView);

        Task<TViewModel> GetByIdAsync<TViewModel>(int id);

        Task<CustomerEditViewModel> EditByIdAsync(int id, CustomerEditInputModel customerFromView);

        Task<CustomerDeleteViewModel> DeleteByIdAsync(int id);
    }
}
