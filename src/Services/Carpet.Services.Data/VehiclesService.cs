namespace Carpet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Vehicles;
    using Carpet.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class VehiclesService : IVehiclesService
    {
        private readonly IDeletableEntityRepository<Vehicle> vehicleRepository;

        public VehiclesService(IDeletableEntityRepository<Vehicle> vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleCreateViewModel> CreateAsync(VehicleCreateInputModel vehicleFromView)
        {
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

        public async Task<VehicleEditViewModel> EditByIdAsync(int id, VehicleEditInputModel vehicleFromView)
        {
            var vehicleDeleteAfterEdit = await this.vehicleRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (vehicleDeleteAfterEdit == null)
            {
                throw new NullReferenceException(string.Format(VehicleConstants.NullReferenceId, id));
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

        public async Task<List<SelectListItem>> GetVehicleNames()
        {
            return await this.vehicleRepository.All().Where(x => !x.IsDamaged).Select(x => new SelectListItem { Value = x.RegistrationNumber, Text = x.RegistrationNumber }).ToListAsync();
        }
    }
}
