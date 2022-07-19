using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Validations
{
    public class UserValidation : IValidation<User>
    {
        public ValidationResult Validate(User entity)
        {
            ValidationResult result = new ValidationResult() { IsValid = true, Message = ""};
            if (String.IsNullOrEmpty(entity.Username.Trim()))
            {
                result.IsValid = false;
                result.Message = "Invalid username";
                return result;
            }
            if (String.IsNullOrEmpty(entity.Password.Trim()))
            {
                result.IsValid = false;
                result.Message = "Invalid password";
                return result;
            }
            if (String.IsNullOrEmpty(entity.Name.Trim()))
            {
                result.IsValid = false;
                result.Message = "Invalid name";
                return result;
            }
            if (String.IsNullOrEmpty(entity.LastName.Trim()))
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
