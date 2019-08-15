namespace Carpet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class CarpetUsersService : ICarpetUsersService
    {
        private readonly IDeletableEntityRepository<CarpetUser> userRepository;
        private readonly UserManager<CarpetUser> userManager;

        public CarpetUsersService(IDeletableEntityRepository<CarpetUser> userRepository, UserManager<CarpetUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public IQueryable<CarpetUser> GetAllAsync()
        {
            return this.userRepository.All();
        }

        public async Task<EmployeeCreateViewModel> GetByIdAsync(string id)
        {
            var userFormDb = await this.userManager.FindByIdAsync(id);
            var employee = userFormDb.To<EmployeeCreateViewModel>();
            return employee;
        }
    }
}
