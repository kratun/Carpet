namespace Carpet.Services.Data.OrdersService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Data.CustomerService;
    using Carpet.Services.Data.EmployeesService;
    using Carpet.Services.Data.OrderStatusService;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Orders.Create;
    using Carpet.Web.ViewModels.Administration.Orders.Create;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;
        private readonly ICustomersService customersService;
        private readonly IOrderStatusService orderStatusService;
        private readonly IEmployeesService employeesService;

        public OrdersService(IDeletableEntityRepository<Order> orderRepository, ICustomersService customersService, IOrderStatusService orderStatusService, IEmployeesService employeesService)
        {
            this.orderRepository = orderRepository;
            this.customersService = customersService;
            this.orderStatusService = orderStatusService;
            this.employeesService = employeesService;
        }

        public async Task<OrderCreateViewModel> CreateAsync(OrderCreateInputModel orderFromView, string username, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var errorModel = await this.customersService.GetByIdAsync<OrderCreateViewModel>(orderFromView.CustomerId);
                errorModel.IsExpress = orderFromView.IsExpress;
                errorModel.HasFlavor = orderFromView.HasFlavor;
                errorModel.ItemQuantitySetByUser = orderFromView.ItemQuantitySetByUser;
                return errorModel;
            }

            var hasAnyCustomerWithId = await this.customersService.IsCustomerExistAsync(orderFromView.CustomerId);

            // If customer with Id NOT exists return existing view model
            if (!hasAnyCustomerWithId)
            {
                throw new ArgumentNullException(nameof(orderFromView.CustomerId), string.Format(OrderConstants.NullReferenceCustomerId, orderFromView.CustomerId));
            }

            var orderToDb = orderFromView.To<Order>();

            orderToDb.StatusId = await this.orderStatusService.GetIdByNameAsync(OrderConstants.StatusPickUpArrangeDayWaiting);

            if (orderToDb.StatusId == 0)
            {
                throw new NullReferenceException(string.Format(OrderConstants.NullReferenceStatusNameNotFound, OrderConstants.StatusPickUpArrangeDayWaiting));
            }

            orderToDb.CreatorId = await this.employeesService.GetIdByUserNameAsync(username);

            if (orderToDb.CreatorId == null)
            {
                throw new NullReferenceException(string.Format(OrderConstants.NullReferenceCreatorUsernameNotFound, username));
            }

            await this.orderRepository.AddAsync(orderToDb);

            await this.orderRepository.SaveChangesAsync();

            return orderToDb.To<OrderCreateViewModel>();
        }

        public IQueryable<TViewModel> GetAllAsync<TViewModel>()
        {
            return this.orderRepository.All().Select(x => x.To<TViewModel>());
        }

        public async Task<TViewModel> GetByIdAsync<TViewModel>(string id)
        {
            return await this.orderRepository.All().Where(x => x.Id == id).To<TViewModel>().FirstOrDefaultAsync();
        }
    }
}
