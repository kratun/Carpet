namespace Carpet.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Vehicles;
    using Carpet.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.EntityFrameworkCore;

    public class VehiclesService : IVehiclesService
    {
        private readonly IDeletableEntityRepository<Vehicle> vehicleRepository;

        public VehiclesService(IDeletableEntityRepository<Vehicle> vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleCreateViewModel> CreateAsync(VehicleCreateInputModel vehicleFromView, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return vehicleFromView.To<Vehicle>().To<VehicleCreateViewModel>();
            }

            var checkForRegistrationNumber = await this.vehicleRepository
                .All()
                .FirstOrDefaultAsync(x => x.RegistrationNumber == vehicleFromView.RegistrationNumber);

            // If customer with phone number exists return existing view model
            if (checkForRegistrationNumber != null)
            {
                modelState.AddModelError(nameof(vehicleFromView.RegistrationNumber), string.Format(VehicleConstants.ArgumentExceptionRegistrationNumber, vehicleFromView.RegistrationNumber));
                return vehicleFromView.To<Vehicle>().To<VehicleCreateViewModel>();
            }

            var vehicleToDb = vehicleFromView.To<Vehicle>();

            await this.vehicleRepository.AddAsync(vehicleToDb);

            await this.vehicleRepository.SaveChangesAsync();

            return vehicleToDb.To<VehicleCreateViewModel>();
        }

        public async Task<VehicleDeleteViewModel> DeleteByIdAsync(int id)
        {
            var vehicle = await this.vehicleRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (vehicle == null)
            {
                throw new NullReferenceException(string.Format(VehicleConstants.NullReferenceId, id), new Exception(nameof(id)));
            }

            this.vehicleRepository.Delete(vehicle);
            await this.vehicleRepository.SaveChangesAsync();

            return vehicle.To<VehicleDeleteViewModel>();
        }

        public async Task<VehicleEditViewModel> EditByIdAsync(int id, VehicleEditInputModel vehicleFromView, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return vehicleFromView.To<Vehicle>().To<VehicleEditViewModel>();
            }

            var vehicleDeleteAfterEdit = await this.vehicleRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (vehicleDeleteAfterEdit == null)
            {
                Exception innerException = new Exception(nameof(id));
                throw new NullReferenceException(string.Format(CustomerConstants.NullReferenceCustomerId, id), innerException);
            }

            var checkForRegistrationNumber = await this.vehicleRepository
                .All()
                .FirstOrDefaultAsync(x => x.RegistrationNumber == vehicleFromView.RegistrationNumber);

            // If vehicle with Registration Number exists and id's are different do not delete. Return existing view model
            if (checkForRegistrationNumber != null && vehicleDeleteAfterEdit.Id != checkForRegistrationNumber.Id)
            {
                modelState.AddModelError(nameof(vehicleFromView.RegistrationNumber), string.Format(VehicleConstants.ArgumentExceptionRegistrationNumber, vehicleFromView.RegistrationNumber));
                return vehicleFromView.To<Vehicle>().To<VehicleEditViewModel>();
            }

            var newVehicle = vehicleFromView.To<Vehicle>();

            this.vehicleRepository.Delete(vehicleDeleteAfterEdit);
            await this.vehicleRepository.AddAsync(newVehicle);
            await this.vehicleRepository.SaveChangesAsync();

            return newVehicle.To<VehicleEditViewModel>();
        }

        public IQueryable<TViewModel> GetAllAsync<TViewModel>()
        {
            return this.vehicleRepository.All().Select(x => x.To<TViewModel>());
        }

        public async Task<TViewModel> GetByIdAsync<TViewModel>(int id)
        {
            return await this.vehicleRepository.All().Where(x => x.Id == id).To<TViewModel>().FirstOrDefaultAsync();
        }
    }
}
