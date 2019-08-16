namespace Carpet.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Services.Data;
    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.InputModels.Administration.Employees;
    using Carpet.Web.InputModels.Administration.Employees.Edit;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Employees;
    using Carpet.Web.ViewModels.Administration.Employees.AllUsers;
    using Carpet.Web.ViewModels.Administration.Employees.Edit;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class EmployeesController : AdministrationController
    {
        private readonly IEmployeesService employeesService;
        private readonly IRolesService rolesService;
        private readonly ICarpetUsersService usersService;

        public EmployeesController(IEmployeesService employeesService, IRolesService rolesService, ICarpetUsersService usersService)
        {
            this.employeesService = employeesService;
            this.rolesService = rolesService;
            this.usersService = usersService;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await this.employeesService.GetAllAsync<EmployeeIndexViewModel>()
               .OrderByDescending(x => x.CreatedOn)
               .ToListAsync();

            return this.View(employees);
        }

        // GET: Potential Employees
        public async Task<IActionResult> AllUsers()
        {
            var employees = await this.usersService.GetAllAsync()
                .Where(x => x.Employees.Count == 0)
               .OrderByDescending(x => x.CreatedOn)
               .Select(x => new EmployeeAllUsersViewModel
               {
                   Id = x.Id,
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   PhoneNumber = x.PhoneNumber,
                   Email = x.Email,
                   CreatedOn = x.CreatedOn,
               })
               .ToListAsync();

            return this.View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return this.View();
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create(string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            // TODO: Refactor this
            var employeeViewModel = await this.employeesService.GetNotHiredUserAsync(id);

            return this.View(employeeViewModel);
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateInputModel employeeCreate)
        {
            var result = await this.employeesService.CreateAsync(employeeCreate, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.View(result);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            var employeeViewModel = await this.employeesService.GetByIdAsync<EmployeeEditViewModel>(id);
            var roleList = await this.rolesService.GetAllWithoutAdministratorAsync().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToListAsync();
            employeeViewModel.RoleList = roleList;

            return this.View(employeeViewModel);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EmployeeEditInputModel employeeEdit)
        {
            var result = await this.employeesService.EditByIdAsync(id, employeeEdit, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.View(result);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return this.View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}
