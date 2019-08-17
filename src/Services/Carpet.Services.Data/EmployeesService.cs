namespace Carpet.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.InputModels.Administration.Employees.Delete;
    using Carpet.Web.InputModels.Administration.Employees.Edit;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Employees.Edit;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.EntityFrameworkCore;

    using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

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
            var roles = await this.rolesService.GetAllWithoutAdministratorAsync().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToListAsync();

            if (!modelState.IsValid)
            {
                var errorModel = employeeFromView.To<Employee>().To<EmployeeCreateViewModel>();

                errorModel.RoleList = roles;

                return errorModel;
            }

            var checkForId = await this.employeeRepository.All().FirstOrDefaultAsync(x => x.Id == employeeFromView.Id);

            // If customer with phone number exists return existing view model
            if (checkForId != null)
            {
                Exception innerException = new Exception(nameof(employeeFromView.Id));
                throw new NullReferenceException(string.Format(EmployeeConstants.NullReferenceId, employeeFromView.Id), innerException);
            }

            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.Id == employeeFromView.Id);

            if (user == null)
            {
                Exception innerException = new Exception(nameof(employeeFromView.Id));
                throw new NullReferenceException(string.Format(EmployeeConstants.NullReferenceId, employeeFromView.Id), innerException);
            }

            var role = roles.FirstOrDefault(x => x.Text == employeeFromView.RoleName);

            await this.userManager.AddToRoleAsync(user, role.Text);

            var employeeToDb = employeeFromView.To<Employee>();
            employeeToDb.Id = Guid.NewGuid().ToString();
            employeeToDb.User = user;

            await this.employeeRepository.AddAsync(employeeToDb);

            await this.employeeRepository.SaveChangesAsync();

            return employeeToDb.To<EmployeeCreateViewModel>();
        }

        public async Task<EmployeeDeleteViewModel> DeleteByIdAsync(string id)
        {
            var employee = await this.employeeRepository.All().FirstOrDefaultAsync(d => d.Id == id);

            if (employee == null)
            {
                throw new NullReferenceException(string.Format(CustomerConstants.NullReferenceCustomerId, id), new Exception(nameof(id)));
            }

            if (!string.IsNullOrEmpty(employee.UserId))
            {
                employee.UserId = null;
            }

            this.employeeRepository.Delete(employee);
            await this.employeeRepository.SaveChangesAsync();

            return employee.To<EmployeeDeleteViewModel>();
        }

        public async Task<EmployeeEditViewModel> EditByIdAsync(string id, EmployeeEditInputModel employeeFromView, ModelStateDictionary modelState)
        {
            var roles = await this.rolesService.GetAllWithoutAdministratorAsync().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToListAsync();

            if (!modelState.IsValid)
            {
                var errorModel = employeeFromView.To<Employee>().To<EmployeeEditViewModel>();
                errorModel.RoleList = roles;

                return errorModel;
            }

            var employeeFromDb = await this.employeeRepository.All().FirstOrDefaultAsync(x => x.Id == employeeFromView.Id);

            // If customer with phone number exists but employeeFromDb.Id != employeeFromView.Id do NOT Edit return view model
            if (employeeFromDb != null && employeeFromDb.Id != employeeFromView.Id)
            {
                Exception innerException = new Exception(nameof(employeeFromView.Id));
                throw new NullReferenceException(string.Format(EmployeeConstants.NullReferenceId, employeeFromView.Id), innerException);
            }

            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == employeeFromView.PhoneNumber);

            if (user == null)
            {
                modelState.AddModelError(nameof(employeeFromView.PhoneNumber), string.Format(EmployeeConstants.ArgumentExceptionPhoneNumberNotExist, employeeFromView.PhoneNumber));

                var errorModel = employeeFromView.To<Employee>().To<EmployeeEditViewModel>();
                errorModel.RoleList = roles;

                return errorModel;
            }

            var role = roles.FirstOrDefault(x => x.Text == employeeFromView.RoleName);

            await this.userManager.RemoveFromRoleAsync(user, employeeFromDb.RoleName);

            await this.userManager.AddToRoleAsync(user, role.Text);

            employeeFromDb.FirstName = employeeFromView.FirstName;
            employeeFromDb.LastName = employeeFromView.LastName;
            employeeFromDb.PhoneNumber = employeeFromView.PhoneNumber;
            employeeFromDb.RoleName = employeeFromView.RoleName;
            employeeFromDb.Salary = employeeFromView.Salary;

            this.employeeRepository.Update(employeeFromDb);

            await this.employeeRepository.SaveChangesAsync();

            return employeeFromDb.To<EmployeeEditViewModel>();
        }

        public IQueryable<TViewModel> GetAllAsync<TViewModel>()
        {
            return this.employeeRepository.All().Select(x => x.To<TViewModel>());
        }

        public async Task<TViewModel> GetByIdAsync<TViewModel>(string id)
        {
            return await Task.FromResult(this.employeeRepository.All().FirstOrDefault(x => x.Id == id).To<TViewModel>());
        }

        public async Task<EmployeeCreateViewModel> GetNotHiredUserAsync(string id)
        {
            var notHiredUser = this.userManager.Users.Where(x => x.Id == id).Select(x => x.To<EmployeeCreateViewModel>()).FirstOrDefault();

            var roles = await this.rolesService.GetAllWithoutAdministratorAsync().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToListAsync();

            notHiredUser.RoleList = roles;

            return notHiredUser;
        }
    }
}
