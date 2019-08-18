namespace Carpet.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Garage;
    using Carpet.Web.ViewModels.Administration.Garage.Add;
    using Carpet.Web.ViewModels.Administration.Garage.Remove;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class GarageService : IGarageService
    {
        private readonly IDeletableEntityRepository<VehicleEmployee> garageRepository;
        private readonly IVehiclesService vehiclesService;
        private readonly IEmployeesService employeesService;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;

        public GarageService(IDeletableEntityRepository<VehicleEmployee> garageRepository, IVehiclesService vehiclesService, IEmployeesService employeesService, IDeletableEntityRepository<Employee> employeeRepository)
        {
            this.garageRepository = garageRepository;
            this.vehiclesService = vehiclesService;
            this.employeesService = employeesService;
            this.employeeRepository = employeeRepository;
        }

        public async Task<EmployeeAddViewModel> GetByIdAsync(string id, ModelStateDictionary modelState)
        {
            var vehicles = await this.GetVehicleNames();

            var employeeFromDb = await this.employeesService.GetByIdAsync<EmployeeAddViewModel>(id);

            if (employeeFromDb == null)
            {
                Exception innerException = new Exception(nameof(employeeFromDb.Id));
                throw new NullReferenceException(string.Format(GarageConstants.NullReferenceId, employeeFromDb.Id), innerException);
            }

            if (vehicles == null || vehicles.Count == 0)
            {
                vehicles = new System.Collections.Generic.List<SelectListItem>();
            }

            employeeFromDb.VehicleList = vehicles;

            return employeeFromDb;
        }

        public async Task<bool> RemoveVehicleToEmployeeAsync(EmployeeRemoveInputModel employeeFromView)
        {
            var employee = await this.employeesService.GetByIdAsync<Employee>(employeeFromView.Id);

            if (employee == null)
            {
                Exception innerException = new Exception(nameof(employeeFromView.Id));
                throw new NullReferenceException(string.Format(GarageConstants.NullReferenceId, employeeFromView.Id), innerException);
            }

            var vehicleEmployee = await this.garageRepository.All().FirstOrDefaultAsync(x => x.EmployeeId == employeeFromView.Id && x.Vehicle.RegistrationNumber == employeeFromView.RegistrationNumber);

            if (vehicleEmployee == null)
            {
                Exception innerException = new Exception(nameof(employeeFromView.RegistrationNumber));
                throw new NullReferenceException(string.Format(GarageConstants.NullReferenceId, employeeFromView.RegistrationNumber), innerException);
            }

            this.garageRepository.Delete(vehicleEmployee);

            await this.garageRepository.SaveChangesAsync();

            return true;
        }

        public async Task<EmployeeAddViewModel> SetVehicleToEmployeeAsync(EmployeeAddInputModel employeeFromView, ModelStateDictionary modelState)
        {
            var vehicles = await this.GetVehicleNames();

            if (!modelState.IsValid)
            {
                var errorModel = employeeFromView.To<Employee>().To<EmployeeAddViewModel>();

                errorModel.VehicleList = vehicles;

                return errorModel;
            }

            var employee = await this.employeesService.GetByIdAsync<Employee>(employeeFromView.Id);

            if (employee == null)
            {
                Exception innerException = new Exception(nameof(employeeFromView.Id));
                throw new NullReferenceException(string.Format(GarageConstants.NullReferenceId, employeeFromView.Id), innerException);
            }

            var vehicleEmployee = await this.garageRepository.All().FirstOrDefaultAsync(x => x.EmployeeId == employeeFromView.Id);

            if (vehicleEmployee != null)
            {
                modelState.AddModelError(nameof(vehicleEmployee.Vehicle.RegistrationNumber), string.Format(GarageConstants.ArgumentExceptionVehicleEmployeeExist, vehicleEmployee.Vehicle.RegistrationNumber));

                var errorModel = employeeFromView.To<Employee>().To<EmployeeAddViewModel>();

                errorModel.VehicleList = vehicles;

                return errorModel;
            }

            var vehicle = await this.vehiclesService.GetAllAsync<Vehicle>().FirstOrDefaultAsync(x => x.RegistrationNumber == employeeFromView.RegistrationNumber);

            if (vehicle == null)
            {
                Exception innerException = new Exception(nameof(employeeFromView.RegistrationNumber));
                throw new ArgumentNullException(string.Format(GarageConstants.ArgumentExceptionVehicleNotExist, employeeFromView.RegistrationNumber), innerException);
            }

            employee.VehicleEmployees.Add(new VehicleEmployee { EmployeeId = employee.Id, VehicleId = vehicle.Id });

            this.employeeRepository.Update(employee);

            await this.employeeRepository.SaveChangesAsync();

            return employee.To<EmployeeAddViewModel>();
        }

        private async Task<System.Collections.Generic.List<SelectListItem>> GetVehicleNames()
        {
            return await this.vehiclesService.GetAllAsync<Vehicle>().Where(x => !x.IsDamaged).Select(x => new SelectListItem { Value = x.RegistrationNumber, Text = x.RegistrationNumber }).ToListAsync();
        }
    }
}
