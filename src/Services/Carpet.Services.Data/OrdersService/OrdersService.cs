﻿namespace Carpet.Services.Data.OrdersService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Data.CustomerService;
    using Carpet.Services.Data.OrderStatusService;
    using Carpet.Services.Mapping;
    using Carpet.Web.InputModels.Administration.Orders.AddVehicleForPickUp;
    using Carpet.Web.InputModels.Administration.Orders.Create;
    using Carpet.Web.ViewModels.Administration.Orders.AddVehicleToPickUp;
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
    }
}
