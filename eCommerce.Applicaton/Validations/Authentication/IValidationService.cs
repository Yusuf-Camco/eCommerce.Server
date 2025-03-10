using eCommerce.Application.DTOs;
using FluentValidation;

namespace eCommerce.Application.Validations.Authentication
{
    public interface IValidationService
    {
        Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator);
    }
}
