namespace Carpet.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Services.Data;
    using Carpet.Services.Data.EmployeeService;
    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.InputModels.Administration.Employees.Delete;
    using Carpet.Web.InputModels.Administration.Employees.Edit;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Employees.AllUsers;
    using Carpet.Web.ViewModels.Administration.Employees.Edit;
    using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Details(int id)
        {
            return this.View();
        }

        // GET: Employees/Create
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            // TODO: Refactor this
            var employeeViewModel = await this.employeesService.GetNotHiredUserAsync(id);

            var roles = await this.rolesService.GetAllWithoutAdministratorAsync().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToListAsync();

            employeeViewModel.RoleList = roles;

            return this.View(employeeViewModel);
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(EmployeeCreateInputModel employeeCreate)
        {
            if (!this.ModelState.IsValid)
            {
                var errorModel = AutoMapper.Mapper.Map<EmployeeCreateViewModel>(employeeCreate);

                var roles = await this.rolesService.GetAllWithoutAdministratorAsync().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToListAsync();

                errorModel.RoleList = roles;

                return this.View(errorModel);
            }

            var result = await this.employeesService.CreateAsync(employeeCreate);

            if (!this.ModelState.IsValid)
            {
                return this.View(result);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(string id, EmployeeEditInputModel employeeEdit)
        {
            if (!this.ModelState.IsValid)
            {
                var errorModel = AutoMapper.Mapper.Map<EmployeeEditViewModel>(employeeEdit);

                var roles = await this.rolesService.GetAllWithoutAdministratorAsync().Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToListAsync();

                errorModel.RoleList = roles;

                return this.View(errorModel);
            }

            var result = await this.employeesService.EditByIdAsync(id, employeeEdit);

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            var employeToDelete = await this.employeesService.GetByIdAsync<EmployeeDeleteViewModel>(id);
            return this.View(employeToDelete);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(string id, EmployeeDeleteInputModel employeeDelete)
        {
            var employee = await this.employeesService.DeleteByIdAsync(employeeDelete.Id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
