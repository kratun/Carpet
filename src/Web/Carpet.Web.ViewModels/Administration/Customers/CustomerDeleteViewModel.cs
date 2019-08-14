﻿namespace Carpet.Web.ViewModels.Administration.Customers
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class CustomerDeleteViewModel : IMapTo<Customer>, IMapFrom<Customer>
    {
        [Display(Name = CustomerConstants.DisplayNameFirstName)]
        public string FirstName { get; set; }

        [Display(Name = CustomerConstants.DisplayNameLastName)]
        public string LastName { get; set; }

        [Display(Name = CustomerConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = CustomerConstants.DisplayNamePickUpAddress)]
        public string PickUpAddress { get; set; }

        [Display(Name = CustomerConstants.DisplayNameDeliveryAddress)]
        public string DeliveryAddress { get; set; }
    }
}
