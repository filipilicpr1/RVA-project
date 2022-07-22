using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Validations
{
    public class BusValidation : IValidation<Bus>
    {
        public ValidationResult Validate(Bus entity)
        {
            ValidationResult result = new ValidationResult() { IsValid = true, Message = "" };
            if (String.IsNullOrWhiteSpace(entity.Name))
            {
                result.IsValid = false;
                result.Message = "Invalid name";
                return result;
            }
            if (String.IsNullOrWhiteSpace(entity.Label))
            {
                result.IsValid = false;
                result.Message = "Invalid label";
                return result;
            }
            return result;
        }
    }
}
