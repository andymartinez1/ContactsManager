using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Services.Helpers;

public class ValidationHelper
{
    public static void ModelValidation(object obj)
    {
        // Model validations used since there are several model property validations
        var validationContext = new ValidationContext(obj);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
        if (!isValid)
            throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
    }
}
