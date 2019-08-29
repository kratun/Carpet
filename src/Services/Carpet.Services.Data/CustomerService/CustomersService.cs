namespace Carpet.Services.Data.CustomerService
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

        public async Task<CustomerCreateViewModel> CreateAsync(CustomerCreateInputModel customerFromView)
        {
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
                throw new NullReferenceException(string.Format(CustomerConstants.NullReferenceCustomerId, id));
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

        public async Task<CustomerEditViewModel> EditByIdAsync(string id, CustomerEditInputModel customerFromView)
        {
            var customerToUpdate = await this.customerRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == customerFromView.Id);

            if (customerToUpdate == null)
            {
                throw new NullReferenceException(string.Format(CustomerConstants.NullReferenceCustomerId, id));
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

        public async Task<CustomerAddAddressViewModel> AddAddressToCustomerAsync(CustomerAddAddressInputModel customerFromView)
        {
            var customerToDb = customerFromView.To<Customer>();

            // Remove mapped Key to create new Customer
            customerToDb.Id = null;

            // if User with Id NOT EXIST set Null
            customerToDb.UserId = this.customerRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == customerFromView.Id)?.UserId;

            await this.customerRepository.AddAsync(customerToDb);

            await this.customerRepository.SaveChangesAsync();

            return customerToDb.To<CustomerAddAddressViewModel>();
        }

        public async Task<bool> IsCustomerExistAsync(string id)
        {
            return await this.customerRepository.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }
    }
}
