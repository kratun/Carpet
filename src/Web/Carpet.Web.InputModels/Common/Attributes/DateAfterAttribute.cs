namespace Carpet.Web.InputModels.Common.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;

    [AttributeUsage(AttributeTargets.Property)]
    public class DateAfterAttribute : ValidationAttribute
    {
        public DateAfterAttribute(string dateToCompareToFieldName)
        {
            this.DateToCompareToFieldName = dateToCompareToFieldName;
        }

        private string DateToCompareToFieldName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime earlierDate = DateTime.Parse(value.ToString());

            DateTime laterDate = DateTime.Parse(validationContext.ObjectType.GetProperty(this.DateToCompareToFieldName).GetValue(validationContext.ObjectInstance, null) != null ? validationContext.ObjectType.GetProperty(this.DateToCompareToFieldName).GetValue(validationContext.ObjectInstance, null).ToString() : "00:00");

            if (laterDate < earlierDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(OrderConstants.ErrorDateAfterAttribute);
            }
        }
    }
}
