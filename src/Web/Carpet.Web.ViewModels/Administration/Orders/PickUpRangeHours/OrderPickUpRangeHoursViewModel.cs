namespace Carpet.Web.ViewModels.Administration.Orders.PickUpRangeHours
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

    public class OrderPickUpRangeHoursViewModel : IMapFrom<Order>, IMapFrom<Vehicle>, IHaveCustomMappings
    {
        public OrderPickUpRangeHoursViewModel()
        {
            this.PickUpVehicles = new HashSet<OrderPickUpVehicleEmployeeViewModel>();

            this.PickUpForStartHours = new List<SelectListItem>();
            this.PickUpForEndHours = new List<SelectListItem>();
        }

        public string Id { get; set; }

        public string CustomerId { get; set; }

        public OrderCustomerPickUpRangeHoursViewModel Customer { get; set; }

        [Display(Name = OrderConstants.DisplayNameIsExpress)]
        public string IsExpress { get; set; }

        [Display(Name = OrderConstants.DisplayNameItemQuantitySetByUser)]
        public int ItemQuantitySetByUser { get; set; }

        [Display(Name = OrderConstants.DisplayNameCreatedOn)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = OrderConstants.DisplayNameStatus)]
        public string StatusName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = OrderConstants.DisplayNamePickUpDate)]
        public DateTime PickUpFor { get; set; }

        [Display(Name = VehicleConstants.DisplayNameRegistrationNumber)]
        public string RegistrationNumber => this.PickUpVehicles?.FirstOrDefault().VehicleEmployee.Vehicle.RegistrationNumber;

        [Display(Name = OrderConstants.DisplayNamePickUpForStartHour)]
        public string PickUpForStartHour { get; set; }

        [Display(Name = OrderConstants.DisplayNamePickUpForEndHour)]
        public string PickUpForEndHour { get; set; }

        public ICollection<OrderPickUpVehicleEmployeeViewModel> PickUpVehicles { get; set; }

        public ICollection<SelectListItem> PickUpForStartHours { get; set; }

        public ICollection<SelectListItem> PickUpForEndHours { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Order, OrderPickUpRangeHoursViewModel>()
                .ForMember(
                    destination => destination.StatusName,
                    opts => opts.MapFrom(origin => origin.Status.Name))
                .ForMember(
                    destination => destination.IsExpress,
                    opts => opts.MapFrom(origin => origin.IsExpress == true ? GlobalConstants.YesString : GlobalConstants.NoString));
        }
    }
}
