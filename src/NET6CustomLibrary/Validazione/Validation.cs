namespace NET6CustomLibrary.Validazione;

public class Validation : IValidation
{
    private readonly ILoggerService logger;

    public Validation(ILoggerService logger)
    {
        this.logger = logger;
    }

    public List<string> ProcessErrorList(ValidationResult validationResult)
    {
        List<string> errorList = new();

        validationResult.Errors.ForEach(x => errorList.Add(x.ErrorMessage));
        validationResult.Errors.ForEach(y => logger.SaveLogError(y.ErrorMessage));

        return errorList;
    }
}