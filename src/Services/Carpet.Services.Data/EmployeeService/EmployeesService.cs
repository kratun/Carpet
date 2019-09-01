namespace Carpet.Services.Data.EmployeeService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.InputModels.Administration.Employees.Delete;
    using Carpet.Web.InputModels.Administration.Employees.Edit;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Employees.Edit;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

    public class EmployeesService : IEmployeesService
    {
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly ApplicationDbContext context;

        public EmployeesService(IDeletableEntityRepository<Employee> employeeRepository, ApplicationDbContext context)
        {
            this.employeeRepository = employeeRepository;
            this.context = context;
        }

        public async Task<EmployeeCreateViewModel> CreateAsync(EmployeeCreateInputModel employeeFromView, string pictureUrl)
        {
            var checkForId = await this.employeeRepository.All().FirstOrDefaultAsync(x => x.PhoneNumber == employeeFromView.PhoneNumber);

            // If customer with phone number exists return existing view model
            if (checkForId != null)
            {
                throw new ArgumentException(string.Format(EmployeeConstants.ArgumentExceptionPhoneNumberExist, employeeFromView.PhoneNumber));
            }

            var user = await this.context.Users.FirstOrDefaultAsync(x => x.Id == employeeFromView.Id);

            if (user == null)
            {
                throw new NullReferenceException(string.Format(EmployeeConstants.NullReferenceUserId, employeeFromView.Id));
            }

            var role = await this.context.Roles.Where(x => x.Name != GlobalConstants.AdministratorRoleName).FirstOrDefaultAsync(x => x.Name == employeeFromView.RoleName);

            if (role == null)
            {
                throw new ArgumentNullException(string.Format(EmployeeConstants.ArgumentExceptionRoleNotExist, employeeFromView.RoleName), new Exception(nameof(employeeFromView.RoleName)));
            }

            user.Roles.Add(new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id });

            var employeeToDb = employeeFromView.To<Employee>();
            employeeToDb.Id = Guid.NewGuid().ToString();
            employeeToDb.User = user;
            employeeToDb.Picture = pictureUrl;

            await this.employeeRepository.AddAsync(employeeToDb);

            await this.employeeRepository.SaveChangesAsync();

            return employeeToDb.To<EmployeeCreateViewModel>();
        }

        public async Task<EmployeeDeleteViewModel> DeleteByIdAsync(string id)
        {
            var employee = await this.employeeRepository.All().FirstOrDefaultAsync(d => d.Id == id);

            if (employee == null)
            {
                throw new NullReferenceException(string.Format(EmployeeConstants.NullReferenceId, id));
            }

            if (!string.IsNullOrEmpty(employee.UserId))
            {
                var roles = employee.User.Roles;
                employee.UserId = null;
                employee.RoleName = null;
                this.context.UserRoles.RemoveRange(roles);
                await this.context.SaveChangesAsync();
            }

            this.employeeRepository.Delete(employee);
            await this.employeeRepository.SaveChangesAsync();

            return employee.To<EmployeeDeleteViewModel>();
        }

        public async Task<EmployeeEditViewModel> EditByIdAsync(string id, EmployeeEditInputModel employeeFromView, string pictureUrl)
        {
            var employeeFromDb = await this.employeeRepository.All().FirstOrDefaultAsync(x => x.Id == employeeFromView.Id);

            if (employeeFromDb == null)
            {
                throw new NullReferenceException(string.Format(EmployeeConstants.NullReferenceId, employeeFromView.Id));
            }

            var user = await this.context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == employeeFromView.PhoneNumber);

            if (user == null)
            {
                throw new ArgumentNullException(string.Format(EmployeeConstants.ArgumentExceptionPhoneNumberNotExist, employeeFromView.PhoneNumber), new Exception(nameof(employeeFromView.PhoneNumber)));
            }

            var role = await this.context.Roles.Where(x => x.Name != GlobalConstants.AdministratorRoleName).FirstOrDefaultAsync(x => x.Name == employeeFromView.RoleName);

            if (role == null)
            {
                throw new ArgumentNullException(string.Format(EmployeeConstants.ArgumentExceptionRoleNotExist, employeeFromView.RoleName), new Exception(nameof(employeeFromView.RoleName)));
            }

            if (employeeFromDb.RoleName != GlobalConstants.AdministratorRoleName)
            {
                user.Roles.Clear();
            }

            user.Roles.Add(new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id });
            employeeFromDb.User = user;
            employeeFromDb.FirstName = employeeFromView.FirstName;
            employeeFromDb.LastName = employeeFromView.LastName;
            employeeFromDb.PhoneNumber = employeeFromView.PhoneNumber;
            employeeFromDb.RoleName = employeeFromView.RoleName;
            employeeFromDb.Salary = employeeFromView.Salary;
            employeeFromDb.Picture = pictureUrl;

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

        public async Task<string> GetIdByUserNameAsync(string username)
        {
            var result = await this.employeeRepository.All().Where(x => x.User.UserName == username).Select(x => x.Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeCreateViewModel> GetNotHiredUserAsync(string id)
        {
            var notHiredUser = await this.context.Users.Where(x => x.Id == id).Where(x => !x.Employees.Any(e => !e.IsDeleted)).Select(x => x.To<EmployeeCreateViewModel>()).FirstOrDefaultAsync();

            return notHiredUser;
        }
    }
}
