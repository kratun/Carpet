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

            var checkForPhoneNumber = await this.customerRepository.All().FirstOrDefaultAsync(x => x.PhoneNumber == customerFromView.PhoneNumber);

            // If customer with phone number exists return existing view model
            if (checkForPhoneNumber != null)
            {
                modelState.AddModelError(nameof(customerFromView.PhoneNumber), string.Format(CustomerConstants.ArgumentExceptionCustomerPhone, customerFromView.PhoneNumber));
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

            var customerToDelete = await this.customerRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customerToDelete == null)
            {
                Exception innerException = new Exception(nameof(id));
                throw new NullReferenceException(string.Format(CustomerConstants.NullReferenceCustomerId, id), innerException);
            }

            var checkForPhoneNumber = await this.customerRepository
                .All()
                .FirstOrDefaultAsync(x => x.PhoneNumber == customerFromView.PhoneNumber);

            // If customer with phone number exists return existing view model
            if (checkForPhoneNumber != null && customerToDelete.Id != checkForPhoneNumber.Id)
            {
                modelState.AddModelError(nameof(customerFromView.PhoneNumber), string.Format(CustomerConstants.ArgumentExceptionCustomerPhone, customerFromView.PhoneNumber));
                var result = customerFromView.To<Customer>().To<CustomerEditViewModel>();
                return result;
            }

            var newCustomer = customerFromView.To<Customer>();

            this.customerRepository.Delete(customerToDelete);
            await this.customerRepository.AddAsync(newCustomer);
            await this.customerRepository.SaveChangesAsync();

            return newCustomer.To<CustomerEditViewModel>();
        }

        public IQueryable<TViewModel> GetAllCustomersAsync<TViewModel>()
        {
            return this.customerRepository.All().Select(x => x.To<TViewModel>());
        }

        public async Task<TViewModel> GetByIdAsync<TViewModel>(string id)
        {
            return await this.customerRepository.All().Where(x => x.Id == id).To<TViewModel>().FirstOrDefaultAsync();
        }
    }
}
