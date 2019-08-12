namespace Carpet.Web.ViewModels.Customers
{
    using System;

    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class CustomerIndexViewModel : IMapTo<Customer>, IMapFrom<Customer>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string PickUpAddress { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
