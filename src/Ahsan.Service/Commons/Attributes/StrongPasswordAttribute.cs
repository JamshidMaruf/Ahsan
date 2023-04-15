using Ahsan.Service.Commons.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.Commons.Attributes
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return new ValidationResult("Set the password");
            else
            {
                string password = value.ToString();
                if (password.Length < 8)
                    return new ValidationResult("Password length must be at least 8 characters.");
                else if (password.Length < 30)
                    return new ValidationResult("Password length must be maximum 30 characters.");
                var result = PasswordValidation.IsStrong(password);

                if (result.IsValid is false) return new ValidationResult(result.Message);
                return ValidationResult.Success;
            }
        }
    }
}
