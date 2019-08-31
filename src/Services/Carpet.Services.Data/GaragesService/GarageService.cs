namespace Carpet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Data.EmployeeService;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Garage;
    using Carpet.Web.ViewModels.Administration.Garage.Add;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class GarageService : IGarageService
    {
        private readonly IDeletableEntityRepository<VehicleEmployee> garageRepository;
        private readonly IVehiclesService vehiclesService;
        private readonly IEmployeesService employeesService;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly IDeletableEntityRepository<Order> orderRepository;

        public GarageService(IDeletableEntityRepository<VehicleEmployee> garageRepository, IVehiclesService vehiclesService, IEmployeesService employeesService, IDeletableEntityRepository<Employee> employeeRepository, IDeletableEntityRepository<Order> orderRepository)
        {
            this.garageRepository = garageRepository;
            this.vehiclesService = vehiclesService;
            this.employeesService = employeesService;
            this.employeeRepository = employeeRepository;
            this.orderRepository = orderRepository;
        }

        public async Task<EmployeeAddViewModel> GetByIdAsync(string id, ModelStateDictionary modelState)
        {
            var vehicles = await this.vehiclesService.GetVehicleNames();

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

            // TODO: CHECK ORDERS BY STATUS AND DO NOT REMOVE IF is in status StatusPickUpArrangeHourRangeWaiting, StatusPickUpArrangedDateWaiting, StatusPickUpArrangedDateCоnfirmed, StatusPickedUpConfirm, StatusDeliveryArrangeHourRangeWaiting, StatusDeliveryArrangedDateCоnfirmed, StatusDeliverConfirmed

            var isInNotCorrectStatus = await this.orderRepository.AllAsNoTracking()
                .Where(x => x.PickUpVehicles.Count < 2 || x.DeliveryVehicles.Count < 2)
                .Where(x => x.Status.Name == OrderConstants.StatusPickUpArrangeHourRangeWaiting ||
                x.Status.Name == OrderConstants.StatusPickUpArrangedDateWaiting ||
                x.Status.Name == OrderConstants.StatusPickUpArrangedDateCоnfirmed ||
                x.Status.Name == OrderConstants.StatusDeliveryArrangeHourRangeWaiting ||
                x.Status.Name == OrderConstants.StatusDeliveryArrangedDateWaiting ||
                x.Status.Name == OrderConstants.StatusDeliveryArrangedDateCоnfirmed)
                .AnyAsync(x => x.PickUpVehicles.Any(z => z.VehicleEmployee.Id == vehicleEmployee.Id) ||
                x.DeliveryVehicles.Any(z => z.VehicleEmployee.Id == vehicleEmployee.Id));

            // TODO: Return false and in controller add error to modelState
            if (isInNotCorrectStatus)
            {
                Exception innerException = new Exception(nameof(employeeFromView.RegistrationNumber));
                throw new ArgumentException(string.Format(GarageConstants.ArgumentExceptionVehicleInIncorrectStatus, employeeFromView.RegistrationNumber), innerException);
            }

            this.garageRepository.Delete(vehicleEmployee);

            await this.garageRepository.SaveChangesAsync();

            return true;
        }

        public async Task<EmployeeAddViewModel> SetVehicleToEmployeeAsync(EmployeeAddInputModel employeeFromView, ModelStateDictionary modelState)
        {
            var vehicles = await this.vehiclesService.GetVehicleNames();

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

        public async Task<List<SelectListItem>> GetVehicleNames()
        {
            return await this.garageRepository.All().Where(x => !x.Vehicle.IsDamaged).Select(x => new SelectListItem { Value = x.Id, Text = x.Vehicle.RegistrationNumber }).ToListAsync();
        }
    }
}
