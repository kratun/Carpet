﻿namespace Carpet.Web.ViewModels.Administration.Orders.AllWaitingPickUpHours
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderCustomerAllWaitingPickUpHoursViewModel : IMapFrom<Customer>
    {
        [Display(Name = CustomerConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = CustomerConstants.DisplayNamePickUpAddress)]
        public string PickUpAddress { get; set; }
    }
}
