namespace Carpet.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Data;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Carpet.Web.ViewModels.Customers;

    public class CustomersService : ICustomersService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository<Customer> customerRepository;

        public CustomersService(ApplicationDbContext context, IRepository<Customer> customerRepository)
        {
            this.context = context;
            this.customerRepository = customerRepository;
        }

        public Task<bool> Create(CustomerIndexViewModel customerInput)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(int id, CustomerIndexViewModel customerEdit)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CustomerIndexViewModel> GetAllCustomers()
        {
            return this.customerRepository.All().Select(x => x.To<CustomerIndexViewModel>());
        }

        public Task<CustomerIndexViewModel> GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
