using Server.Enums;
using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Validations
{
    public class BusLineValidation : IValidation<BusLine>
    {
        public ValidationResult Validate(BusLine entity)
        {
            ValidationResult result = new ValidationResult() { IsValid = true, Message = "" };
            if (String.IsNullOrWhiteSpace(entity.Label))
            {
                result.IsValid = false;
                result.Message = "Invalid label";
                return result;
            }
            if(entity.BusLineType != EBusLineType.GRADSKA && entity.BusLineType != EBusLineType.MEDJUGRADSKA && entity.BusLineType != EBusLineType.INTERNACIONALNA)
            {
                result.IsValid = false;
                result.Message = "Invalid bus line type";
                return result;
            }
            return result;
        }
    }
}
