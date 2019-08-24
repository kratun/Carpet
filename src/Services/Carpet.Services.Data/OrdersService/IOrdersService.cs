namespace Carpet.Services.Data.OrdersService
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Orders.AddItems;
    using Carpet.Web.InputModels.Administration.Orders.AddVehicleForPickUp;
    using Carpet.Web.InputModels.Administration.Orders.Create;
    using Carpet.Web.InputModels.Administration.Orders.PickUpRangeHours;
    using Carpet.Web.ViewModels.Administration.Orders.AddItems;
    using Carpet.Web.ViewModels.Administration.Orders.AddVehicleToPickUp;
    using Carpet.Web.ViewModels.Administration.Orders.Create;
    using Carpet.Web.ViewModels.Administration.Orders.PickUpRangeHours;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public interface IOrdersService
    {
        IQueryable<TViewModel> GetAllAsync<TViewModel>();

        IQueryable<TViewModel> GetAllAsNoTrackingAsync<TViewModel>();

        Task<TViewModel> GetByIdAsync<TViewModel>(string id);

        Task<OrderCreateViewModel> CreateAsync(OrderCreateInputModel orderFromView, string username, ModelStateDictionary modelState);

        IQueryable<TViewModel> GetAllCreatedAsync<TViewModel>();

        Task<OrderAddVehicleToPickUpViewModel> AddVehicleForPickUpAsync(OrderAddVehicleForPickUpInputModel orederVehicleFOrPickUp, string username, ModelStateDictionary modelState);

        Task<string> GetByOrderIdRegistrationNumberAsync(string id);

        Task<OrderPickUpRangeHoursViewModel> SetPickUpRangeHoursAsync(OrderPickUpRangeHoursInputModel orderFromView, string username, ModelStateDictionary modelState);

        Task<bool> OrderPickUpConfirmedAsync(string id, string username, ModelStateDictionary modelState);

        Task<OrderAddItemsViewModel> AddItemAsync(OrderAddItemInputModel orderFromView, string username, ModelStateDictionary modelState);
    }
}
