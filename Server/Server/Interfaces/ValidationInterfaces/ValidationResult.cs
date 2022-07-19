namespace Server.Interfaces.ValidationInterfaces
{
    public class ValidationResult
    {
        public string Message { get; set; } = default!;
        public bool IsValid { get; set; }
    }
}
