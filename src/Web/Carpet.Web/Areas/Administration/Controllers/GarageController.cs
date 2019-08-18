namespace Carpet.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Services.Data;
    using Carpet.Web.InputModels.Administration.Garage;
    using Microsoft.AspNetCore.Mvc;

    public class GarageController : AdministrationController
    {
        private readonly IGarageService garageService;

        public GarageController(IGarageService garageService)
        {
            this.garageService = garageService;
        }

        // GET: Garage
        public IActionResult Index()
        {
            return this.View();
        }

        // GET: Garage/Add
        [HttpGet(Name = GlobalConstants.ActionAddName)]
        public async Task<IActionResult> Add(string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/{GlobalConstants.AreaAdministrationName}/{GlobalConstants.ContollerEmployeesName}");
            }

            var result = await this.garageService.GetByIdAsync(id, this.ModelState);

            if (!string.IsNullOrEmpty(result.RegistrationNumber))
            {
                this.ModelState.AddModelError(nameof(result.RegistrationNumber), string.Format(GarageConstants.ArgumentExceptionVehicleEmployeeExist, result.RegistrationNumber));
            }

            return this.View(result);
        }

        // POST: Garage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(EmployeeAddInputModel employeeCreate)
        {
            var result = await this.garageService.SetVehicleToEmployeeAsync(employeeCreate, this.ModelState);

            if (!this.ModelState.IsValid)
            {
                return this.View(result);
            }

            return this.Redirect($"/{GlobalConstants.AreaAdministrationName}/{GlobalConstants.ContollerEmployeesName}");
        }

        // POST: Garage/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(string id, EmployeeRemoveInputModel employeeToRemove)
        {
            var employee = await this.garageService.RemoveVehicleToEmployeeAsync(employeeToRemove);

            return this.Redirect($"/{GlobalConstants.AreaAdministrationName}/{GlobalConstants.ContollerEmployeesName}");
        }
    }
}
