namespace Carpet.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Data.CustomerService;
    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class CustomerServiceTests : BaseServiceTests
    {
        private ICustomersService CustomersServiceMock =>
            this.ServiceProvider.GetRequiredService<ICustomersService>();

        [Fact]
        public async Task CreateAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var secondItem = new Customer
            {
                Id = id,
                FirstName = "Иван",
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
            };

            var expected = new List<Customer>();
            expected.Add(secondItem);

            var add = new CustomerCreateInputModel
            {
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                DeliveryAddress = null,
            };

            var result = await this.CustomersServiceMock.CreateAsync(add);
            var actual = this.DbContext.Customers;

            Assert.Equal(expected.Count, actual.Count());
            Assert.Equal(add.PhoneNumber, result.PhoneNumber);
        }

        [Fact]
        public async Task GetAllCustomersAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var secondItem = new Customer
            {
                Id = id,
                FirstName = "Иван",
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
            };

            var expected = new List<Customer>();
            expected.Add(secondItem);

            var add = new CustomerCreateInputModel
            {
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                DeliveryAddress = null,
            };

            var result = await this.CustomersServiceMock.CreateAsync(add);
            var actual = await this.CustomersServiceMock.GetAllCustomersAsync<CustomerCreateViewModel>().ToListAsync();

            Assert.Equal(expected.Count, actual.Count());
        }

        [Fact]
        public async Task IsCustomerExistAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var secondItem = new Customer
            {
                Id = id,
                FirstName = "Иван",
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
            };

            var expected = new List<Customer>();
            expected.Add(secondItem);

            var add = new CustomerCreateInputModel
            {
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                DeliveryAddress = null,
            };

            var result = await this.CustomersServiceMock.CreateAsync(add);
            var actualTrue = await this.CustomersServiceMock.IsCustomerExistAsync(result.Id);
            var actualFalse = await this.CustomersServiceMock.IsCustomerExistAsync(null);

            Assert.True(actualTrue);
            Assert.False(actualFalse);
        }

        [Fact]
        public async Task AddAddressToCustomerAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var secondItem = new Customer
            {
                Id = id,
                FirstName = "Иван",
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
            };

            var expected = new List<Customer>();
            expected.Add(secondItem);
            secondItem.PickUpAddress = "Варна";
            secondItem.Id = Guid.NewGuid().ToString();
            expected.Add(secondItem);

            var add = new CustomerCreateInputModel
            {
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                DeliveryAddress = null,
            };

            var result = await this.CustomersServiceMock.CreateAsync(add);

            var addAddress = new CustomerAddAddressInputModel
            {
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "Варна",
                DeliveryAddress = null,
            };

            var result1 = await this.CustomersServiceMock.AddAddressToCustomerAsync(addAddress);
            var actual = await this.CustomersServiceMock.GetAllCustomersAsync<CustomerAddAddressInputModel>().ToListAsync();

            Assert.Equal(expected.Count, actual.Count());
            Assert.Equal(addAddress.PickUpAddress, result1.PickUpAddress);
        }

        [Fact]
        public async Task GetByAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var secondItem = new Customer
            {
                Id = id,
                FirstName = "Иван",
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                UserId = userId,
            };

            var add = new CustomerCreateInputModel
            {
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                DeliveryAddress = null,
            };

            var expected = await this.CustomersServiceMock.CreateAsync(add);

            var addAddress = new CustomerAddAddressInputModel
            {
                Id = expected.Id,
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "Варна",
                DeliveryAddress = null,
            };

            var result1 = await this.CustomersServiceMock.AddAddressToCustomerAsync(addAddress);

            var actual = await this.DbContext.Customers.ToListAsync();

            Assert.Equal(expected.PhoneNumber, actual[1].PhoneNumber);
        }

        [Fact]
        public async Task EditByIdAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var secondItem = new Customer
            {
                Id = id,
                FirstName = "Иван",
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                UserId = userId,
            };

            var add = new CustomerCreateInputModel
            {
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                DeliveryAddress = null,
            };

            var expected = await this.CustomersServiceMock.CreateAsync(add);

            var edit = new CustomerEditInputModel
            {
                Id = expected.Id,
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "Варна",
                DeliveryAddress = null,
            };

            var result1 = await this.CustomersServiceMock.EditByIdAsync(expected.Id, edit);

            var actual = await this.DbContext.Customers.ToListAsync();

            Assert.Equal(expected.PhoneNumber, actual[0].PhoneNumber);
            Assert.Equal(result1.PickUpAddress, actual[0].PickUpAddress);
        }

        [Fact]
        public async Task EditByIdAsyncithWrongIdReturnsError()
        {
            var id = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var secondItem = new Customer
            {
                Id = id,
                FirstName = "Иван",
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                UserId = userId,
            };

            var add = new CustomerCreateInputModel
            {
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                DeliveryAddress = null,
            };

            var expected = await this.CustomersServiceMock.CreateAsync(add);
            var wrongId = "123";
            var edit = new CustomerEditInputModel
            {
                Id = wrongId,
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "Варна",
                DeliveryAddress = null,
            };

            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => this.CustomersServiceMock.EditByIdAsync(edit.Id, edit));
            Assert.Equal(string.Format(string.Format(CustomerConstants.NullReferenceCustomerId, wrongId)), exception.Message);
        }

        [Fact]
        public async Task DeleteByIdAsyncReturnsCorrect()
        {
            var id = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var secondItem = new Customer
            {
                Id = id,
                FirstName = "Иван",
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                UserId = userId,
            };

            var add = new CustomerCreateInputModel
            {
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                DeliveryAddress = null,
            };

            var expected = await this.CustomersServiceMock.CreateAsync(add);

            var result1 = await this.CustomersServiceMock.DeleteByIdAsync(expected.Id);

            var actual = await this.DbContext.Customers.ToListAsync();

            Assert.Empty(actual);
        }

        [Fact]
        public async Task DeleteByIdAsyncithWrongIdReturnsError()
        {
            var id = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var secondItem = new Customer
            {
                Id = id,
                FirstName = "Иван",
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                UserId = userId,
            };

            var add = new CustomerCreateInputModel
            {
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "София",
                DeliveryAddress = null,
            };

            var expected = await this.CustomersServiceMock.CreateAsync(add);
            var wrongId = "123";
            var edit = new CustomerDeleteViewModel
            {
                FirstName = "Иван",
                LastName = null,
                PhoneNumber = "0888777444",
                PickUpAddress = "Варна",
                DeliveryAddress = null,
            };

            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => this.CustomersServiceMock.DeleteByIdAsync(wrongId));
            Assert.Equal(string.Format(string.Format(CustomerConstants.NullReferenceCustomerId, wrongId)), exception.Message);
        }
    }
}
