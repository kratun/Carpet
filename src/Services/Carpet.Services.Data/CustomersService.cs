namespace Carpet.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Carpet.Common.Constants;
    using Carpet.Data;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Customers;
    using Carpet.Web.ViewModels.Customers;

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

            // If item exists return existing view model
            if (checkForPhoneNumber != null)
            {
                throw new ArgumentException(string.Format(CustomerConstants.ArgumentExceptionCustomerPhone, customerFromView.PhoneNumber), nameof(customerFromView.PhoneNumber));
            }

            var customerToDb = customerFromView.To<Customer>();

            await this.customerRepository.AddAsync(customerToDb);

            await this.customerRepository.SaveChangesAsync();

            return customerToDb.To<CustomerIndexViewModel>();
        }

        public async Task<CustomerDeleteViewModel> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerEditViewModel> EditByIdAsync(int id, CustomerEditInputModel customerFromView)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TViewModel> GetAllCustomersAsync<TViewModel>()
        {
            return this.customerRepository.All().Select(x => x.To<TViewModel>());
        }

        public async Task<TViewModel> GetByIdAsync<TViewModel>(int id)
        {
            throw new NotImplementedException();
        }
    }
}
