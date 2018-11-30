using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuitSupply.Infrastructure.Validator.Utility;

namespace SuitSupply.Infrastructure.Validator.Test
{
    [TestClass]
    public class FluentValidationExtensionsTest
    {
        [TestMethod]
        public void ToSuitValidationResultMustSucceed()
        {
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("ProductId","Not be empty"),
                new ValidationFailure("ProductName","Not be empty")
            });

            var suitValidationResult = validationResult.ToSuitValidationResult();
            Assert.AreEqual(2, suitValidationResult.Errors.Count);
        }
    }
}
