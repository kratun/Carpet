namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Web.ViewModels.Customers;

    public interface ICustomersService
    {
        IQueryable<CustomerIndexViewModel> GetAllCustomers();

        Task<bool> Create(CustomerIndexViewModel customerInput);

        Task<CustomerIndexViewModel> GetById(string id);

        Task<bool> Edit(int id, CustomerIndexViewModel customerEdit);

        Task<bool> Delete(string id);
    }
}
