﻿namespace Carpet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Models;

    public class Vehicle : BaseDeletableModel<int>
    {
        public Vehicle()
        {
            this.VehicleEmployees = new HashSet<VehicleEmployee>();
        }

        [Required(ErrorMessage = VehicleConstants.ErrorFieldRequired)]
        [MinLength(VehicleConstants.MakeMinValue, ErrorMessage = VehicleConstants.ErrorFieldMakeLength)]
        [Display(Name = VehicleConstants.DisplayNameMake)]
        public string Make { get; set; }

        [Required(ErrorMessage = VehicleConstants.ErrorFieldRequired)]
        [MinLength(VehicleConstants.ModelMinValue, ErrorMessage = VehicleConstants.ErrorFieldModelLength)]
        [Display(Name = VehicleConstants.DisplayNameModel)]
        public string Model { get; set; }

        [Required]
        [Display(Name = VehicleConstants.DisplayNameRegistrationNumber)]
        [RegularExpression(VehicleConstants.RegistrationNumberValidation, ErrorMessage = VehicleConstants.ErrorFieldRegistrationNumberRegex)]
        public string RegistrationNumber { get; set; }

        public bool IsDamaged { get; set; }

        public virtual ICollection<VehicleEmployee> VehicleEmployees { get; set; }
    }
}
