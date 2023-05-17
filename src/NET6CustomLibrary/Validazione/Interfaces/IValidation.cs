namespace NET6CustomLibrary.Validazione.Interfaces;

public interface IValidation
{
    List<string> ProcessErrorList(ValidationResult validationResult);
}