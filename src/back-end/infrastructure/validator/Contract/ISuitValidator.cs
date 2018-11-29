namespace SuitSupply.Infrastructure.Validator.Contract
{
    public interface ISuitValidator<in T> where T: class 
    {
        SuitValidationResult PerformValidation(T model);
    }
}
