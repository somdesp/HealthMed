using FluentValidation.Results;

namespace HealthMed.MedicoService.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException()
            : base("Uma ou mais validações falharam.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ValidationException(string key, string message)
    {
        Errors = new Dictionary<string, string[]>
        {
            [key] = [message]
        };
    }

    public IDictionary<string, string[]> Errors { get; }
}
