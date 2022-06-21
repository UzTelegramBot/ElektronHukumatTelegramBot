using Domains;
using System;
using System.ComponentModel.DataAnnotations;
namespace Business.Helpers.ValidationAttributes
{
    public class IsRoleOrganizatorAttribute : ValidationAttribute
    {
        public IsRoleOrganizatorAttribute(string Role)
        {
            this.Role = Role;
        }
        
        private string Role;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(Role);
            if (property == null)
                throw new ArgumentException("Property with this name not found");
            var managerValue = (RoleManager)property.GetValue(validationContext.ObjectInstance);
            if (value == null)
            {
                if (managerValue == RoleManager.Admin)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(ErrorMessage ?? "Organization Id is not valid");
            }
            else if (managerValue == RoleManager.Organizator || managerValue == RoleManager.Operator)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Organization Id is not valid");
        }
    }
}
