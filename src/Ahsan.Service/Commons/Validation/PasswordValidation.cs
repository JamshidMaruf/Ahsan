namespace Ahsan.Service.Commons.Validation
{
    public class PasswordValidation
    {
        public static (bool IsValid,string Message) IsStrong(string possword)
        {
            bool isDigit = possword.Any(x => char.IsDigit(x));
            if (!isDigit)
                return (IsValid: false, Message: "Password must contain at least 1 digit.");

            bool isUppercase = possword.Any(x => char.IsUpper(x));
            if (!isUppercase)
                return (IsValid: false, Message: "Password must contain at least 1 Upper case.");

            return (IsValid: true, Message: "Valid Password");
        }
    }
}
