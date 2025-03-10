using eCommerce.Application.DTOs;
using FluentValidation;

namespace eCommerce.Application.Validations.Authentication
{
    public class ValidationService: IValidationService
    {
        public async Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator)
        {
            var _validation = await validator.ValidateAsync(model);
            if (!_validation.IsValid)
            {
                var errors = _validation.Errors.Select(e => e.ErrorMessage).ToList();
                string errorToString = string.Join("; ", errors);
                return new ServiceResponse { Message = errorToString };
            }
            return new ServiceResponse { Success = true };

        }
    }
}
