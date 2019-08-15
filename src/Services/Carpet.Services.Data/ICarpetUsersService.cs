namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Data.Models;
    using Carpet.Web.ViewModels.Administration.Customers;

    public interface ICarpetUsersService
    {
        IQueryable<CarpetUser> GetAllAsync();

        Task<EmployeeCreateViewModel> GetByIdAsync(string id);
    }
}
