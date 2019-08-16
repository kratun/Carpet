namespace Carpet.Web.ViewModels.Administration.Employees.Edit
{
    using System.Collections.Generic;

    using AutoMapper;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class EmployeeEditUserViewModel : IMapFrom<CarpetUser>, IHaveCustomMappings
    {
        public EmployeeEditUserViewModel()
        {
            this.Roles = new HashSet<string>();
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public ICollection<string> Roles { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<CarpetUser, EmployeeEditUserViewModel>()
                .ForMember(
                    destination => destination.Roles,
                    opts => opts.MapFrom(origin => origin.Roles));
        }
    }
}
