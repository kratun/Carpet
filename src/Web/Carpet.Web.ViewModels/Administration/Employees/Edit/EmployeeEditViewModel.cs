namespace Carpet.Web.ViewModels.Administration.Employees.Edit
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EmployeeEditViewModel : IMapTo<Employee>, IMapFrom<Employee>, IMapTo<CarpetUser>, IMapFrom<CarpetUser>, IHaveCustomMappings
    {
        public EmployeeEditViewModel()
        {
            this.RoleList = new List<SelectListItem>();
        }

        public string Id { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameFirstName)]
        public string FirstName { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameLastName)]
        public string LastName { get; set; }

        [Display(Name = EmployeeConstants.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameSalary)]
        public decimal Salary { get; set; }

        [Display(Name = EmployeeConstants.DisplayNamePicture)]
        public IFormFile PictureLink { get; set; }

        public string UserId { get; set; }

        public EmployeeEditUserViewModel User { get; set; }

        public string RoleId { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameEmployeeRoleName)]
        public string RoleName { get; set; }

        public ICollection<string> Roles { get; set; }

        [Display(Name = EmployeeConstants.DisplayNameEmployeeRoleName)]
        public ICollection<SelectListItem> RoleList { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Employee, EmployeeEditViewModel>()
                .ForMember(
                    destination => destination.Roles,
                    opts => opts.MapFrom(origin => origin.User.Roles.FirstOrDefault().RoleId));
        }
    }
}
