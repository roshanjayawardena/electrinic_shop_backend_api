using FluentValidation.Results;

namespace Electronic_Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
             : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        public ValidationException(ValidationFailure failure)
            : this()
        {
            Errors.Add(failure.PropertyName, new string[] { failure.ErrorMessage });
        }

        public ValidationException(string propertyName, string errorMessage)
            : this()
        {
            Errors.Add(propertyName, new string[] { errorMessage });
        }

        public IDictionary<string, string[]> Errors { get; }

    }
}
