using System.ComponentModel.DataAnnotations;

namespace APICatalago.Validations
{
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeirLetra = value.ToString()[0].ToString();
            if(primeirLetra != primeirLetra.ToUpper())
            {
                return new ValidationResult("A primeira letra do nome deve ser maiúscula");
            }

            return ValidationResult.Success;
        }
    }
}
