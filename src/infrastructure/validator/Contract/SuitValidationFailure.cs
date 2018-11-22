namespace SuitSupply.Infrastructure.Validator.Contract
{
    public class SuitValidationFailure
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
        public string ResourceName { get; set; }
    }
}
