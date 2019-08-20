namespace Carpet.Web.ViewModels.Administration.Orders.Create
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class OrderCreateViewModel : IMapFrom<Order>, IMapFrom<Customer>, IHaveCustomMappings
    {
        public string CustomerId { get; set; }

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

        [Display(Name = OrderConstants.DisplayNameIsExpress)]
        public bool IsExpress { get; set; }

        [Display(Name = OrderConstants.DisplayNameHasFlavor)]
        public bool HasFlavor { get; set; }

        [Display(Name = OrderConstants.DisplayNameItemQuantitySetByUser)]
        public int ItemQuantitySetByUser { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Customer, OrderCreateViewModel>()
                .ForMember(
                    destination => destination.CustomerId,
                    opts => opts.MapFrom(origin => origin.Id));

            configuration
               .CreateMap<Order, OrderCreateViewModel>()
               .ForMember(
                   destination => destination.CustomerId,
                   opts => opts.MapFrom(origin => origin.CustomerId))
               .ForMember(
                   destination => destination.FirstName,
                   opts => opts.MapFrom(origin => origin.Customer.FirstName))
               .ForMember(
                   destination => destination.LastName,
                   opts => opts.MapFrom(origin => origin.Customer.LastName))
               .ForMember(
                   destination => destination.PhoneNumber,
                   opts => opts.MapFrom(origin => origin.Customer.PhoneNumber))
               .ForMember(
                   destination => destination.PickUpAddress,
                   opts => opts.MapFrom(origin => origin.Customer.PickUpAddress))
               .ForMember(
                   destination => destination.DeliveryAddress,
                   opts => opts.MapFrom(origin => origin.Customer.DeliveryAddress));
        }
    }
}
