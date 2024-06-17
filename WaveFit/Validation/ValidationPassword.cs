using System.Globalization;
using System.Windows.Controls;

namespace WaveFit2.Validation
{
    public class ValidationLength : ValidationRule
    {
        public int MinLength { get; set; }

        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string texbox && texbox.Length >= MinLength)
            {
                return ValidationResult.ValidResult;
            }

            return new ValidationResult(false, ErrorMessage);
        }
    }
}