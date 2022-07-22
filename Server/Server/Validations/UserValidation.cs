using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Validations
{
    public class UserValidation : IValidation<User>
    {
        public ValidationResult Validate(User entity)
        {
            ValidationResult result = new ValidationResult() { IsValid = true, Message = ""};
            if (String.IsNullOrWhiteSpace(entity.Username))
            {
                result.IsValid = false;
                result.Message = "Invalid username";
                return result;
            }
            if(entity.Username.Trim().Contains(' '))
            {
                result.IsValid = false;
                result.Message = "Username must be 1 word";
                return result;
            }
            if (String.IsNullOrWhiteSpace(entity.Password))
            {
                result.IsValid = false;
                result.Message = "Invalid password";
                return result;
            }
            if (String.IsNullOrWhiteSpace(entity.Name))
            {
                result.IsValid = false;
                result.Message = "Invalid name";
                return result;
            }
            if (String.IsNullOrWhiteSpace(entity.LastName))
            {
                result.IsValid = false;
                result.Message = "Invalid last name";
                return result;
            }
            if(entity.UserType != Enums.EUserType.ADMIN && entity.UserType != Enums.EUserType.GUEST)
            {
                result.IsValid = false;
                result.Message = "Invalid user type";
                return result;
            }
            return result;
        }
    }
}
