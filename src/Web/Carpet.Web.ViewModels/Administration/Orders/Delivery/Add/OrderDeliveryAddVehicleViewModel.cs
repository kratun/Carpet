namespace Carpet.Web.ViewModels.Administration.Orders.Delivery.Add
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class OrderDeliveryAddVehicleViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public OrderDeliveryAddVehicleViewModel()
        {
            this.VehicleList = new HashSet<SelectListItem>();
        }

        public string Id { get; set; }

        public string CustomerId { get; set; }

        public OrderCustomerDeliveryAddVehicleViewModel Customer { get; set; }

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
        public string RegistrationNumber { get; set; }

        public ICollection<SelectListItem> VehicleList { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Order, OrderDeliveryAddVehicleViewModel>()
                .ForMember(
                    destination => destination.StatusName,
                    opts => opts.MapFrom(origin => origin.Status.Name))
                .ForMember(
                    destination => destination.IsExpress,
                    opts => opts.MapFrom(origin => origin.IsExpress == true ? GlobalConstants.YesString : GlobalConstants.NoString));
        }
    }
}
