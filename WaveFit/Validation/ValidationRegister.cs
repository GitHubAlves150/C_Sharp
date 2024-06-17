using System.Globalization;
using System.Windows.Controls;

namespace WaveFit2.Validation
{
    public class ValidationRegister : ValidationRule
    {
        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string texto && string.IsNullOrEmpty(texto))
            {
                return new ValidationResult(false, ErrorMessage);
            }

            return ValidationResult.ValidResult;
        }
    }
}