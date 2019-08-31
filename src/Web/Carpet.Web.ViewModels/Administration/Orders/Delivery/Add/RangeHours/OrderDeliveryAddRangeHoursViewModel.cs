namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Add.RangeHours
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class OrderDeliveryAddRangeHoursViewModel : IMapFrom<Order>, IMapFrom<Vehicle>, IHaveCustomMappings
    {
        public OrderDeliveryAddRangeHoursViewModel()
        {
            this.DeliveryVehicles = new HashSet<OrderDeliveryVehicleEmployeeDeliveryAddRangeHoursViewModel>();

            this.DeliveryForStartHours = new List<SelectListItem>();
            this.DeliveryForEndHours = new List<SelectListItem>();
        }

        public string Id { get; set; }

        public string CustomerId { get; set; }

        public string RouteString { get; set; }

        public OrderCustomerDeliveryRangeHoursViewModel Customer { get; set; }

        [Display(Name = OrderConstants.DisplayNameIsExpress)]
        public string IsExpress { get; set; }

        [Display(Name = OrderConstants.DisplayNameItemQuantity)]
        public int ItemQuantity { get; set; }

        [Display(Name = OrderConstants.DisplayNameCreatedOn)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = OrderConstants.DisplayNameStatus)]
        public string StatusName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = OrderConstants.DisplayNameDeliveryDate)]
        public DateTime DeliveringFor { get; set; }

        [Display(Name = VehicleConstants.DisplayNameRegistrationNumber)]
        public string RegistrationNumber => this.DeliveryVehicles.FirstOrDefault().VehicleEmployee.Vehicle.RegistrationNumber;

        [Display(Name = OrderConstants.DisplayNameStartHour)]
        public string DeliveringForStartHour { get; set; }

        [Display(Name = OrderConstants.DisplayNameEndHour)]
        public string DeliveringForEndHour { get; set; }

        public ICollection<OrderDeliveryVehicleEmployeeDeliveryAddRangeHoursViewModel> DeliveryVehicles { get; set; }

        public ICollection<SelectListItem> DeliveryForStartHours { get; set; }

        public ICollection<SelectListItem> DeliveryForEndHours { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Order, OrderDeliveryAddRangeHoursViewModel>()
                .ForMember(
                    destination => destination.StatusName,
                    opts => opts.MapFrom(origin => origin.Status.Name))
                .ForMember(
                    destination => destination.ItemQuantity,
                    opts => opts.MapFrom(origin => origin.OrderItems.Count))
                .ForMember(
                    destination => destination.IsExpress,
                    opts => opts.MapFrom(origin => origin.IsExpress == true ? GlobalConstants.YesString : GlobalConstants.NoString));
        }
    }
}
