using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Validations
{
    public class CityValidation : IValidation<City>
    {
        public ValidationResult Validate(City entity)
        {
            ValidationResult result = new ValidationResult() { IsValid = true, Message = "" };
            if (String.IsNullOrWhiteSpace(entity.Name))
            {
                result.IsValid = false;
                result.Message = "Invalid name";
                return result;
            }
            if (entity.Country == null)
            {
                result.IsValid = false;
                result.Message = "Invalid country";
                return result;
            }
            return result;
        }
    }
}
