namespace Carpet.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Web.InputModels.Administration.Vehicles;
    using Carpet.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;


    public class VehicleServiceTests : BaseServiceTests
    {
        private IVehiclesService VehiclesServiceMock =>
            this.ServiceProvider.GetRequiredService<IVehiclesService>();

        [Fact]
        public async Task CreateAsyncReturnsCorrect()
        {
            var id = 1;
            var first = new Vehicle
            {
                Id = id,
                Make = "Opel",
                Model = "Corsa",
                RegistrationNumber = "CA2254TX",
            };

            var expected = new List<Vehicle>();
            expected.Add(first);

            var add = new VehicleCreateInputModel
            {
                Make = "Mazda",
                Model = "Z6",
                RegistrationNumber = "CA5554TX",
            };

            var result = await this.VehiclesServiceMock.CreateAsync(add);
            var actual = this.DbContext.Vehicles;

            Assert.Equal(expected.Count, actual.Count());
            Assert.Equal(add.RegistrationNumber, result.RegistrationNumber);
        }

        [Fact]
        public async Task GetAllAsyncReturnsCorrect()
        {
            var id = 1;
            var first = new Vehicle
            {
                Id = id,
                Make = "Opel",
                Model = "Corsa",
                RegistrationNumber = "CA2254TX",
            };

            var expected = new List<Vehicle>();
            expected.Add(first);

            var add = new VehicleCreateInputModel
            {
                Make = "Mazda",
                Model = "Z6",
                RegistrationNumber = "CA5554TX",
            };

            var result = await this.VehiclesServiceMock.CreateAsync(add);
            var actual = await this.VehiclesServiceMock.GetAllAsync<VehicleCreateViewModel>().ToListAsync();

            Assert.Equal(expected.Count, actual.Count());
        }

        [Fact]
        public async Task GetByAsyncReturnsCorrect()
        {
            var id = 2;
            var first = new VehicleCreateInputModel
            {
                Make = "Mazda",
                Model = "Z6",
                RegistrationNumber = "CA5554TX",
            };

            await this.VehiclesServiceMock.CreateAsync(first);

            var second = new VehicleCreateInputModel
            {
                Make = "Opel",
                Model = "Corsa",
                RegistrationNumber = "CA2222TX",
            };

            var result = await this.VehiclesServiceMock.CreateAsync(second);
            var actual = await this.VehiclesServiceMock.GetByIdAsync<VehicleCreateViewModel>(id);

            Assert.Equal(second.RegistrationNumber, actual.RegistrationNumber);
        }

        [Fact]
        public async Task EditByIdAsyncReturnsCorrect()
        {
            var id = 2;
            var first = new VehicleCreateInputModel
            {
                Make = "Mazda",
                Model = "Z6",
                RegistrationNumber = "CA5554TX",
            };

            await this.VehiclesServiceMock.CreateAsync(first);

            var second = new VehicleCreateInputModel
            {
                Make = "Opel",
                Model = "Corsa",
                RegistrationNumber = "CA2222TX",
            };

            var result = await this.VehiclesServiceMock.CreateAsync(second);
            var editModel = await this.VehiclesServiceMock.GetByIdAsync<VehicleEditViewModel>(id);

            var edit = new VehicleEditInputModel
            {
                Make = "OpelEdited",
                Model = "Corsa",
                RegistrationNumber = "CA2222TX",
            };

            var actual = await this.VehiclesServiceMock.EditByIdAsync(editModel.Id, edit);

            var vehicles = await this.DbContext.Vehicles.ToListAsync();

            Assert.Equal(edit.Make, actual.Make);
            Assert.Contains(vehicles, x => x.Make == edit.Make);
        }

        [Fact]
        public async Task EditByIdAsyncithWrongIdReturnsError()
        {
            var id = 3;
            var first = new VehicleCreateInputModel
            {
                Make = "Mazda",
                Model = "Z6",
                RegistrationNumber = "CA5554TX",
            };

            await this.VehiclesServiceMock.CreateAsync(first);

            var second = new VehicleCreateInputModel
            {
                Make = "Opel",
                Model = "Corsa",
                RegistrationNumber = "CA2222TX",
            };

            var result = await this.VehiclesServiceMock.CreateAsync(second);
            var editModel = await this.VehiclesServiceMock.GetByIdAsync<VehicleEditViewModel>(id);

            var edit = new VehicleEditInputModel
            {
                Make = "OpelEdited",
                Model = "Corsa",
                RegistrationNumber = "CA2222TX",
            };

            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => this.VehiclesServiceMock.EditByIdAsync(id, edit));
            Assert.Equal(string.Format(string.Format(VehicleConstants.NullReferenceId, id)), exception.Message);
        }

        [Fact]
        public async Task DeleteByIdAsyncReturnsCorrect()
        {
            var id = 2;
            var first = new VehicleCreateInputModel
            {
                Make = "Mazda",
                Model = "Z6",
                RegistrationNumber = "CA5554TX",
            };

            await this.VehiclesServiceMock.CreateAsync(first);

            var second = new VehicleCreateInputModel
            {
                Make = "Opel",
                Model = "Corsa",
                RegistrationNumber = "CA2222TX",
            };

            var result = await this.VehiclesServiceMock.CreateAsync(second);

            var result1 = await this.VehiclesServiceMock.DeleteByIdAsync(id);

            var actual = await this.VehiclesServiceMock.GetAllAsync<VehicleDeleteViewModel>().ToListAsync();

            Assert.DoesNotContain(actual, x => x.Id == id);
        }

        [Fact]
        public async Task GetVehicleNames()
        {
            var id = 2;
            var first = new VehicleCreateInputModel
            {
                Make = "Mazda",
                Model = "Z6",
                RegistrationNumber = "CA5554TX",
            };

            await this.VehiclesServiceMock.CreateAsync(first);

            var second = new VehicleCreateInputModel
            {
                Make = "Opel",
                Model = "Corsa",
                RegistrationNumber = "CA2222TX",
            };

            var result = await this.VehiclesServiceMock.CreateAsync(second);

            await this.VehiclesServiceMock.DeleteByIdAsync(1);

            var actual = await this.VehiclesServiceMock.GetVehicleNames();

            Assert.Single(actual);
            Assert.Contains(actual, x => x.Text == result.RegistrationNumber);
        }
    }
}
