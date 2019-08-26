namespace Carpet.Web.InputModels.Common.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;

    [AttributeUsage(AttributeTargets.Property)]
    public class DateBeforeAttribute : ValidationAttribute
    {
        public DateBeforeAttribute(string dateToCompareToFieldName)
        {
            this.DateToCompareToFieldName = dateToCompareToFieldName;
        }

        private string DateToCompareToFieldName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime earlierDate = value != null ? DateTime.Parse(value.ToString()) : DateTime.MaxValue;

            DateTime laterDate = DateTime.Parse(validationContext.ObjectType.GetProperty(this.DateToCompareToFieldName).GetValue(validationContext.ObjectInstance, null) != null ? validationContext.ObjectType.GetProperty(this.DateToCompareToFieldName).GetValue(validationContext.ObjectInstance, null).ToString() : "00:00");

            if (laterDate > earlierDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(OrderConstants.ErrorDateBeforeAttribute);
            }
        }
    }
}
