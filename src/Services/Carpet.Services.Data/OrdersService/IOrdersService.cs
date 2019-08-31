namespace Carpet.Services.Data.OrdersService
{
    using System.Linq;
    using System.Threading.Tasks;

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

    public interface IOrdersService
    {
        IQueryable<TViewModel> GetAllAsync<TViewModel>();

        IQueryable<TViewModel> GetAllAsNoTrackingAsync<TViewModel>();


        IQueryable<TViewModel> GetAllAsNoTrackingWithDeteletedAsync<TViewModel>();

        Task<TViewModel> GetByIdAsync<TViewModel>(string id);

        Task<OrderCreateViewModel> CreateAsync(OrderCreateInputModel orderFromView, string username, ModelStateDictionary modelState);

        IQueryable<TViewModel> GetAllCreatedAsync<TViewModel>();

        Task<OrderAddVehicleToPickUpViewModel> AddVehicleForPickUpAsync(OrderAddVehicleForPickUpInputModel orederVehicleFOrPickUp, string username, ModelStateDictionary modelState);

        Task<string> GetByOrderIdRegistrationNumberAsync(string id);

        Task<OrderPickUpRangeHoursViewModel> SetPickUpRangeHoursAsync(OrderPickUpRangeHoursInputModel orderFromView, string username, ModelStateDictionary modelState);

        Task<OrderDeliveryAddRangeHoursViewModel> SetDeliveryRangeHoursAsync(OrderDeliveryAddRangeHoursInputModel orderFromView, string username, ModelStateDictionary modelState);

        Task<OrderDeliveryAddVehicleViewModel> DeliveryAddVehicleAsync(OrderDeliveryAddVehicleInputModel orderFromView, string username, ModelStateDictionary modelState);

        Task<OrderAddItemsViewModel> AddItemAsync(OrderAddItemInputModel orderFromView, string username, ModelStateDictionary modelState);

        Task<bool> OrderGangeStatusAsync(string id, string username, string newStatus, ModelStateDictionary modelState);

        Task<bool> OrderGangeStatusAndRemoveVehicleAsync(string id, string username, string newStatus, ModelStateDictionary modelState);

        Task<bool> OrderGangeStatusAndRemovePickUpVehicleAsync(string id, string username, string newStatus, ModelStateDictionary modelState);
    }
}
