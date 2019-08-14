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
    using Microsoft.EntityFrameworkCore;

    public class CustomersService : ICustomersService
    {
        private readonly IRepository<Customer> customerRepository;

        public CustomersService(IDeletableEntityRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<CustomerIndexViewModel> CreateAsync(CustomerCreateInputModel customerFromView)
        {
            var checkForPhoneNumber = this.customerRepository.All().FirstOrDefault(x => x.PhoneNumber == customerFromView.PhoneNumber);

            // If customer with phone number exists return existing view model
            if (checkForPhoneNumber != null)
            {
                throw new ArgumentException(string.Format(CustomerConstants.ArgumentExceptionCustomerPhone, customerFromView.PhoneNumber), nameof(customerFromView.PhoneNumber));
            }

            var customerToDb = customerFromView.To<Customer>();

            await this.customerRepository.AddAsync(customerToDb);

            await this.customerRepository.SaveChangesAsync();

            return customerToDb.To<CustomerIndexViewModel>();
        }

        public async Task<CustomerDeleteViewModel> DeleteByIdAsync(string id)
        {
            var customer = await this.customerRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (customer == null)
            {
                throw new NullReferenceException(string.Format(ItemConstants.NullReferenceItemId, id), new Exception(nameof(id)));
            }

            this.customerRepository.Delete(customer);
            await this.customerRepository.SaveChangesAsync();

            return customer.To<CustomerDeleteViewModel>();
        }

        public async Task<CustomerEditViewModel> EditByIdAsync(string id, CustomerEditInputModel customerFromView)
        {
            var customerToDelete = this.customerRepository.All().FirstOrDefault(x => x.Id == id);

            if (customerToDelete == null)
            {
                Exception innerException = new Exception(nameof(id));
                throw new NullReferenceException(string.Format(CustomerConstants.NullReferenceCustomerId, id), innerException);
            }

            var checkForPhoneNumber = this.customerRepository.All().FirstOrDefault(x => x.PhoneNumber == customerFromView.PhoneNumber);

            // If customer with phone number exists return existing view model
            if (checkForPhoneNumber != null && customerToDelete.Id != checkForPhoneNumber.Id)
            {
                throw new ArgumentException(string.Format(CustomerConstants.ArgumentExceptionCustomerPhone, customerFromView.PhoneNumber), nameof(customerFromView.PhoneNumber));
            }

            var newItem = customerFromView.To<Customer>();

            this.customerRepository.Delete(customerToDelete);
            await this.customerRepository.AddAsync(newItem);
            await this.customerRepository.SaveChangesAsync();

            return newItem.To<CustomerEditViewModel>();
        }

        public IQueryable<TViewModel> GetAllCustomersAsync<TViewModel>()
        {
            return this.customerRepository.All().Select(x => x.To<TViewModel>());
        }

        public async Task<TViewModel> GetByIdAsync<TViewModel>(string id)
        {
            return this.customerRepository.All().FirstOrDefault(x => x.Id == id).To<TViewModel>();
        }
    }
}
