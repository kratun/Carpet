namespace Carpet.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Services.Data;
    using Carpet.Web.InputModels.Administration.Vehicles;
    using Carpet.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class VehiclesController : AdministrationController
    {
        private readonly IVehiclesService vehiclesService;

        public VehiclesController(IVehiclesService vehiclesService)
        {
            this.vehiclesService = vehiclesService;
        }

        // GET: Vehicles
        [Route("/Administration/Vehicles")]
        public async Task<IActionResult> Index()
        {
            var vehicles = await this.vehiclesService.GetAllAsync<VehicleIndexViewModel>()
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            return this.View(vehicles);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var vehicleToDetail = await this.vehiclesService.GetByIdAsync<VehicleDetailsViewModel>(id);
            return this.View(vehicleToDetail);
        }

        // GET: Vehicles/Create
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleCreateInputModel vehicleCreate)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(vehicleCreate);
                }

                var result = await this.vehiclesService.CreateAsync(vehicleCreate);
                return this.RedirectToAction(nameof(this.Index));
            }
            catch (ArgumentException e)
            {
                // TODO: Error message that item name exist
                this.ModelState.AddModelError(e.ParamName, e.Message);
                return this.View(vehicleCreate);
            }
            catch (NullReferenceException e)
            {
                // TODO: Error message that item name exist
                this.ModelState.AddModelError(e.InnerException.Message, e.Message);
                return this.View(vehicleCreate);
            }
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var vehicleToEdit = await this.vehiclesService.GetByIdAsync<VehicleEditViewModel>(id);
            return this.View(vehicleToEdit);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleEditInputModel vehicleEdit)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(vehicleEdit);
                }

                var result = await this.vehiclesService.EditByIdAsync(id, vehicleEdit);

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (ArgumentException e)
            {
                // TODO: Error message that vehicle same RegistrationNumber as edited exist
                this.ModelState.AddModelError(e.ParamName, e.Message);
                return this.View(vehicleEdit);
            }
            catch (NullReferenceException e)
            {
                // TODO: Error message that vehicle does not exist
                this.ModelState.AddModelError(e.InnerException.Message, e.Message);
                return this.View(vehicleEdit);
            }
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var vehicleToDelete = await this.vehiclesService.GetByIdAsync<VehicleDeleteViewModel>(id);
            return this.View(vehicleToDelete);
        }

        // POST: Vehicles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, VehicleDeleteInputModel vehicleDelete)
        {
            try
            {
                var customer = await this.vehiclesService.DeleteByIdAsync(id);

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (NullReferenceException e)
            {
                // TODO: Error message that item not exist
                // this.ModelState.Root.AttemptedValue = "ModelOnly";
                // this.ModelState.Root.RawValue = e.Message;
                this.ModelState.AddModelError(e.InnerException.Message, e.Message);
                return this.View(vehicleDelete);
            }
        }
    }
}
