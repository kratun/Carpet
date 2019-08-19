namespace Carpet.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.ViewModels.Administration.Customers;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.EntityFrameworkCore;

    public class CustomersService : ICustomersService
    {
        private readonly IDeletableEntityRepository<Customer> customerRepository;

        public CustomersService(IDeletableEntityRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<CustomerCreateViewModel> CreateAsync(CustomerCreateInputModel customerFromView, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return customerFromView.To<Customer>().To<CustomerCreateViewModel>();
            }

            var hasAnyCustomerWithPhoneNumber = await this.customerRepository.All().AnyAsync(x => x.PhoneNumber == customerFromView.PhoneNumber);

            // If customer with phone number exists return existing view model
            if (hasAnyCustomerWithPhoneNumber)
            {
                modelState.AddModelError(string.Empty, string.Format(CustomerConstants.ArgumentExceptionCustomerExistAddAddress));
                return customerFromView.To<Customer>().To<CustomerCreateViewModel>();
            }

            var customerToDb = customerFromView.To<Customer>();

            await this.customerRepository.AddAsync(customerToDb);

            await this.customerRepository.SaveChangesAsync();

            return customerToDb.To<CustomerCreateViewModel>();
        }

        public async Task<CustomerDeleteViewModel> DeleteByIdAsync(string id)
        {
            var customer = await this.customerRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (customer == null)
            {
                throw new NullReferenceException(string.Format(CustomerConstants.NullReferenceCustomerId, id), new Exception(nameof(id)));
            }

            // TODO DETOUCH USER!!!
            if (!string.IsNullOrEmpty(customer.UserId))
            {
                customer.UserId = null;
                customer.User = null;
            }

            this.customerRepository.Delete(customer);
            await this.customerRepository.SaveChangesAsync();

            return customer.To<CustomerDeleteViewModel>();
        }

        public async Task<CustomerEditViewModel> EditByIdAsync(string id, CustomerEditInputModel customerFromView, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return customerFromView.To<Customer>().To<CustomerEditViewModel>();
            }

            var customerToUpdate = await this.customerRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == customerFromView.Id);

            if (customerToUpdate == null)
            {
                Exception innerException = new Exception(nameof(id));
                throw new NullReferenceException(string.Format(CustomerConstants.NullReferenceCustomerId, id), innerException);
            }

            var hasAnyCustomerWithSameData = await this.customerRepository
                .AllAsNoTracking()
                .AnyAsync(x => HasMaxSameCustomerData(x, customerFromView.FirstName, customerFromView.LastName, customerFromView.PhoneNumber, customerFromView.PickUpAddress, customerFromView.DeliveryAddress));

            if (hasAnyCustomerWithSameData)
            {
                modelState.AddModelError(string.Empty, CustomerConstants.ArgumentExceptionCustomerExist);
                var result = customerFromView.To<Customer>().To<CustomerEditViewModel>();
                return result;
            }

            customerToUpdate.FirstName = customerFromView.FirstName;
            customerToUpdate.LastName = customerFromView.LastName;
            customerToUpdate.PhoneNumber = customerFromView.PhoneNumber;
            customerToUpdate.PickUpAddress = customerFromView.PickUpAddress;
            customerToUpdate.DeliveryAddress = customerFromView.DeliveryAddress;

            this.customerRepository.Update(customerToUpdate);
            await this.customerRepository.SaveChangesAsync();

            return customerToUpdate.To<CustomerEditViewModel>();
        }

        public IQueryable<TViewModel> GetAllCustomersAsync<TViewModel>()
        {
            return this.customerRepository.All().Select(x => x.To<TViewModel>());
        }

        public async Task<TViewModel> GetByIdAsync<TViewModel>(string id)
        {
            return await this.customerRepository.All().Where(x => x.Id == id).To<TViewModel>().FirstOrDefaultAsync();
        }

        public async Task<CustomerAddAddressViewModel> AddAddressToCustomerAsync(CustomerAddAddressInputModel customerFromView, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return customerFromView.To<Customer>().To<CustomerAddAddressViewModel>();
            }

            var hasAnyCustomerWithMaxSameData = await this.customerRepository.All().Where(x => x.PhoneNumber == customerFromView.PhoneNumber).AnyAsync(x => HasMaxSameCustomerData(x, customerFromView.FirstName, customerFromView.LastName, customerFromView.PhoneNumber, customerFromView.PickUpAddress, customerFromView.DeliveryAddress));

            if (hasAnyCustomerWithMaxSameData)
            {
                modelState.AddModelError(string.Empty, CustomerConstants.ArgumentExceptionCustomerExist);
                return customerFromView.To<Customer>().To<CustomerAddAddressViewModel>();
            }

            var hasAnyCustomerWithMinSameData = await this.customerRepository.All().Where(x => x.PhoneNumber == customerFromView.PhoneNumber).AnyAsync(x => !HasMinSameCustomerData(x, customerFromView.FirstName, customerFromView.LastName, customerFromView.PhoneNumber));

            if (hasAnyCustomerWithMinSameData)
            {
                modelState.AddModelError(string.Empty, string.Format(CustomerConstants.ArgumentExceptionCustomerPhone, customerFromView.PhoneNumber));
                return customerFromView.To<Customer>().To<CustomerAddAddressViewModel>();
            }

            var customerToDb = customerFromView.To<Customer>();

            // Remove mapped Key to create new Customer
            customerToDb.Id = null;

            // if User with Id NOT EXIST set Null
            customerToDb.UserId = this.customerRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == customerFromView.Id)?.UserId;

            await this.customerRepository.AddAsync(customerToDb);

            await this.customerRepository.SaveChangesAsync();

            return customerToDb.To<CustomerAddAddressViewModel>();
        }

        private static bool HasMaxSameCustomerData(Customer checkForCustomer, string firstName, string lastName, string phoneNumber, string pickUpAddress, string deliveryAddress)
        {
            return HasMinSameCustomerData(checkForCustomer, firstName, lastName, phoneNumber) &&
                            checkForCustomer.PickUpAddress == pickUpAddress &&
                            checkForCustomer.DeliveryAddress == deliveryAddress;
        }

        private static bool HasMinSameCustomerData(Customer checkForCustomer, string firstName, string lastName, string phoneNumber)
        {
            return checkForCustomer != null &&
                                        checkForCustomer.FirstName == firstName &&
                                        checkForCustomer.LastName == lastName &&
                                        checkForCustomer.PhoneNumber == phoneNumber;
        }
    }
}
