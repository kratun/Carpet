namespace Carpet.Services.Data.OrdersService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Data.CustomerService;
    using Carpet.Services.Data.EmployeeService;
    using Carpet.Services.Data.OrderItemsService;
    using Carpet.Services.Data.OrderStatusService;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Orders.AddItems;
    using Carpet.Web.InputModels.Administration.Orders.AddVehicleForPickUp;
    using Carpet.Web.InputModels.Administration.Orders.Create;
    using Carpet.Web.InputModels.Administration.Orders.Delivery.Add.RangeHours;
    using Carpet.Web.InputModels.Administration.Orders.Delivery.Add.Vehicle;
    using Carpet.Web.InputModels.Administration.Orders.PickUpRangeHours;
    using Carpet.Web.ViewModels.Administration.Orders.AddItems;
    using Carpet.Web.ViewModels.Administration.Orders.AddVehicleToPickUp;
    using Carpet.Web.ViewModels.Administration.Orders.Create;
    using Carpet.Web.ViewModels.Administration.Orders.Delivery.Add.RangeHours;
    using Carpet.Web.ViewModels.Administration.Orders.Delivery.Add.Vehicles;
    using Carpet.Web.ViewModels.Administration.Orders.PickUpRangeHours;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;
        private readonly ICustomersService customersService;
        private readonly IOrderStatusService orderStatusService;
        private readonly IEmployeesService employeesService;
        private readonly IItemsService itemsService;
        private readonly IOrderItemsService orderItemsService;

        public OrdersService(IDeletableEntityRepository<Order> orderRepository, ICustomersService customersService, IOrderStatusService orderStatusService, IEmployeesService employeesService, IItemsService itemsService, IOrderItemsService orderItemsService)
        {
            this.orderRepository = orderRepository;
            this.customersService = customersService;
            this.orderStatusService = orderStatusService;
            this.employeesService = employeesService;
            this.itemsService = itemsService;
            this.orderItemsService = orderItemsService;
        }

        public async Task<OrderAddVehicleToPickUpViewModel> AddVehicleForPickUpAsync(OrderAddVehicleForPickUpInputModel orderFromView, string username, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var errorModel = await this.GetByIdAsync<OrderAddVehicleToPickUpViewModel>(orderFromView.Id);
                errorModel.PickUpFor = orderFromView.PickUpFor;
                return errorModel;
            }

            var orderFromDb = await this.orderRepository.All().FirstOrDefaultAsync(x => x.Id == orderFromView.Id);

            // If customer with Id NOT exists return existing view model
            if (orderFromDb == null)
            {
                throw new ArgumentNullException(nameof(orderFromView.Id), string.Format(OrderConstants.NullReferenceOrderIdNotFound, orderFromView.Id));
            }

            orderFromDb.PickUpFor = orderFromView.PickUpFor;
            var orderPickUpVehicleEmploee = new OrderPickUpVehicleEmployee { OrderId = orderFromDb.Id, VehicleEmployeeId = orderFromView.RegistrationNumber };
            orderFromDb.PickUpVehicles.Add(orderPickUpVehicleEmploee);

            orderFromDb.StatusId = await this.orderStatusService.GetIdByNameAsync(OrderConstants.StatusPickUpArrangeHourRangeWaiting);

            orderFromDb.CreatorId = await this.employeesService.GetIdByUserNameAsync(username);

            this.orderRepository.Update(orderFromDb);

            await this.orderRepository.SaveChangesAsync();

            return orderFromDb.To<OrderAddVehicleToPickUpViewModel>();
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

            orderToDb.CreatorId = await this.employeesService.GetIdByUserNameAsync(username);

            await this.orderRepository.AddAsync(orderToDb);

            await this.orderRepository.SaveChangesAsync();

            return orderToDb.To<OrderCreateViewModel>();
        }

        public IQueryable<TViewModel> GetAllAsNoTrackingAsync<TViewModel>()
        {
            return this.orderRepository.AllAsNoTracking().To<TViewModel>();
        }

        public IQueryable<TViewModel> GetAllAsync<TViewModel>()
        {
            return this.orderRepository.All().Select(x => x.To<TViewModel>());
        }

        public IQueryable<TViewModel> GetAllCreatedAsync<TViewModel>()
        {
            return this.orderRepository.All().Where(x => x.Status.Name == OrderConstants.StatusPickUpArrangeDayWaiting).To<TViewModel>();
        }

        public async Task<TViewModel> GetByIdAsync<TViewModel>(string id)
        {
            return await this.orderRepository.All().Where(x => x.Id == id).To<TViewModel>().FirstOrDefaultAsync();
        }

        public async Task<string> GetByOrderIdRegistrationNumberAsync(string id)
        {
            var registrationNumber = await this.GetAllAsNoTrackingAsync<Order>().Where(x => x.Id == id).Select(x => x.PickUpVehicles.FirstOrDefault().VehicleEmployee.Vehicle.RegistrationNumber).FirstOrDefaultAsync();

            return registrationNumber;
        }

        public async Task<OrderPickUpRangeHoursViewModel> SetPickUpRangeHoursAsync(OrderPickUpRangeHoursInputModel orderFromView, string username, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var errorModel = await this.GetByIdAsync<OrderPickUpRangeHoursViewModel>(orderFromView.Id);
                errorModel.PickUpForStartHour = orderFromView.PickUpForStartHour;
                errorModel.PickUpForEndHour = orderFromView.PickUpForEndHour;

                return errorModel;
            }

            var orderFromDb = await this.orderRepository.All().FirstOrDefaultAsync(x => x.Id == orderFromView.Id);

            // If order with Id NOT exists
            if (orderFromDb == null)
            {
                throw new ArgumentNullException(nameof(orderFromView.Id), string.Format(OrderConstants.NullReferenceOrderIdNotFound, orderFromView.Id));
            }

            orderFromDb.PickUpForStartHour = orderFromView.PickUpForStartHour;
            orderFromDb.PickUpForEndHour = orderFromView.PickUpForEndHour;

            orderFromDb.StatusId = await this.orderStatusService.GetIdByNameAsync(OrderConstants.StatusPickUpArrangedDateWaiting);

            orderFromDb.CreatorId = await this.employeesService.GetIdByUserNameAsync(username);

            this.orderRepository.Update(orderFromDb);

            await this.orderRepository.SaveChangesAsync();

            return orderFromDb.To<OrderPickUpRangeHoursViewModel>();
        }

        public async Task<OrderAddItemsViewModel> AddItemAsync(OrderAddItemInputModel orderFromView, string username, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var errorModel = await this.orderRepository.AllAsNoTracking().Where(x => x.Id == orderFromView.Id).To<OrderAddItemsViewModel>().FirstOrDefaultAsync();
                return errorModel;
            }

            var order = await this.orderRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == orderFromView.Id && !x.IsDeleted);

            if (order == null)
            {
                // TODO: exception
            }

            var orderItemDb = order.OrderItems
                .Where(x => !x.IsDeleted)
                .ToList();

            order.OrderItems = orderItemDb;

            var isItemExist = await this.itemsService.GetAllItemsAsync<Item>().AnyAsync(x => x.Id == orderFromView.ItemId);

            if (!isItemExist)
            {
                // TODO: exception
                throw new NullReferenceException();
            }

            var orderItem = new OrderItem
            {
                ItemId = orderFromView.ItemId,
                OrderId = order.Id,
                ItemWidth = orderFromView.ItemWidth,
                ItemHeight = orderFromView.ItemHeight,
                Description = orderFromView.Description,
            };

            order.OrderItems.Add(orderItem);

            this.orderRepository.Update(order);

            await this.orderRepository.SaveChangesAsync();

            return order.To<OrderAddItemsViewModel>();
        }

        public async Task<bool> OrderGangeStatusAsync(string id, string username, string newStatus, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return false;
            }

            var orderFromDb = await this.orderRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            // If order with Id NOT exists
            if (orderFromDb == null)
            {
                throw new ArgumentNullException(nameof(id), string.Format(OrderConstants.NullReferenceOrderIdNotFound, id));
            }

            orderFromDb.StatusId = await this.orderStatusService.GetIdByNameAsync(newStatus);

            orderFromDb.CreatorId = await this.employeesService.GetIdByUserNameAsync(username);

            this.orderRepository.Update(orderFromDb);

            await this.orderRepository.SaveChangesAsync();

            return true;
        }

        public IQueryable<TViewModel> GetAllAsNoTrackingWithDeteletedAsync<TViewModel>()
        {
            return this.orderRepository.AllAsNoTrackingWithDeleted().To<TViewModel>();
        }

        public async Task<OrderDeliveryAddVehicleViewModel> DeliveryAddVehicleAsync(OrderDeliveryAddVehicleInputModel orderFromView, string username, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var errorModel = await this.GetByIdAsync<OrderDeliveryAddVehicleViewModel>(orderFromView.Id);
                errorModel.DeliveringFor = orderFromView.DeliveringFor;
                return errorModel;
            }

            var orderFromDb = await this.orderRepository.All().FirstOrDefaultAsync(x => x.Id == orderFromView.Id);

            // If customer with Id NOT exists return existing view model
            if (orderFromDb == null)
            {
                throw new ArgumentNullException(nameof(orderFromView.Id), string.Format(OrderConstants.NullReferenceOrderIdNotFound, orderFromView.Id));
            }

            orderFromDb.DeliveringFor = orderFromView.DeliveringFor;
            var orderDeliveryVehicleEmploee = new OrderDeliveryVehicleEmployee { OrderId = orderFromDb.Id, VehicleEmployeeId = orderFromView.RegistrationNumber };
            orderFromDb.DeliveryVehicles.Add(orderDeliveryVehicleEmploee);

            orderFromDb.StatusId = await this.orderStatusService.GetIdByNameAsync(OrderConstants.StatusDeliveryArrangeHourRangeWaiting);

            orderFromDb.CreatorId = await this.employeesService.GetIdByUserNameAsync(username);

            this.orderRepository.Update(orderFromDb);

            await this.orderRepository.SaveChangesAsync();

            return orderFromDb.To<OrderDeliveryAddVehicleViewModel>();
        }

        public async Task<OrderDeliveryAddRangeHoursViewModel> SetDeliveryRangeHoursAsync(OrderDeliveryAddRangeHoursInputModel orderFromView, string username, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var errorModel = await this.GetByIdAsync<OrderDeliveryAddRangeHoursViewModel>(orderFromView.Id);
                errorModel.DeliveringForStartHour = orderFromView.DeliveringForStartHour;
                errorModel.DeliveringForEndHour = orderFromView.DeliveringForEndHour;

                return errorModel;
            }

            var orderFromDb = await this.orderRepository.All().FirstOrDefaultAsync(x => x.Id == orderFromView.Id);

            // If order with Id NOT exists
            if (orderFromDb == null)
            {
                throw new ArgumentNullException(nameof(orderFromView.Id), string.Format(OrderConstants.NullReferenceOrderIdNotFound, orderFromView.Id));
            }

            orderFromDb.DeliveringForStartHour = orderFromView.DeliveringForStartHour;
            orderFromDb.DeliveringForEndHour = orderFromView.DeliveringForEndHour;

            orderFromDb.StatusId = await this.orderStatusService.GetIdByNameAsync(OrderConstants.StatusDeliveryArrangedDateWaiting);

            orderFromDb.CreatorId = await this.employeesService.GetIdByUserNameAsync(username);

            this.orderRepository.Update(orderFromDb);

            await this.orderRepository.SaveChangesAsync();

            return orderFromDb.To<OrderDeliveryAddRangeHoursViewModel>();
        }
    }
}
