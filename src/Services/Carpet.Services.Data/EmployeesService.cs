namespace Carpet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class EmployeesService : IEmployeesService
    {
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly IRolesService rolesService;
        private readonly UserManager<CarpetUser> userManager;

        public EmployeesService(IDeletableEntityRepository<Employee> employeeRepository, IRolesService rolesService, UserManager<CarpetUser> userManager)
        {
            this.employeeRepository = employeeRepository;
            this.rolesService = rolesService;
            this.userManager = userManager;
        }

        public async Task<EmployeeCreateViewModel> CreateAsync(EmployeeCreateInputModel employeeFromView, ModelStateDictionary modelState)
        {
            var roles = await this.rolesService.GetAllAsync().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToListAsync();
            var errorModel = new EmployeeCreateViewModel();
            errorModel.RoleList = roles;
            if (!modelState.IsValid)
            {
                errorModel = employeeFromView.To<Employee>().To<EmployeeCreateViewModel>();

                return errorModel;
            }

            var checkForPhoneNumber = await this.employeeRepository.All().FirstOrDefaultAsync(x => x.PhoneNumber == employeeFromView.PhoneNumber);

            // If customer with phone number exists return existing view model
            if (checkForPhoneNumber != null)
            {
                modelState.AddModelError(nameof(employeeFromView.PhoneNumber), string.Format(EmployeeConstants.ArgumentExceptionPhoneNumberExist, employeeFromView.PhoneNumber));
                errorModel = employeeFromView.To<Employee>().To<EmployeeCreateViewModel>();
                return errorModel;
            }

            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == employeeFromView.PhoneNumber);

            if (user == null)
            {
                modelState.AddModelError(nameof(employeeFromView.PhoneNumber), string.Format(EmployeeConstants.ArgumentExceptionPhoneNumberNotExist, employeeFromView.PhoneNumber));
                errorModel = employeeFromView.To<Employee>().To<EmployeeCreateViewModel>();
                return errorModel;
            }

            var role = roles.FirstOrDefault(x => x.Text == employeeFromView.RoleName);

            await this.userManager.AddToRoleAsync(user, role.Text);

            var employeeToDb = employeeFromView.To<Employee>();

            employeeToDb.User = user;

            await this.employeeRepository.AddAsync(employeeToDb);

            await this.employeeRepository.SaveChangesAsync();
           
            return employeeToDb.To<EmployeeCreateViewModel>();
        }

        public IQueryable<TViewModel> GetAllAsync<TViewModel>()
        {
            return this.employeeRepository.All().Select(x => x.To<TViewModel>());
        }

        public Task<TViewModel> GetByIdAsync<TViewModel>(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeCreateViewModel> GetNotHiredUserAsync(string id)
        {
            var notHiredUser = await this.userManager.Users.Where(x => x.Id == id).Select(x => x.To<EmployeeCreateViewModel>()).FirstOrDefaultAsync();

            var roles = await this.rolesService.GetAllAsync().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToListAsync();

            notHiredUser.RoleList = roles;

            return notHiredUser;
        }
    }
}
