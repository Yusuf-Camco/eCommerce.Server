using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Authentication;
using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Application.Services.Interfaces.Logging;
using eCommerce.Application.Validations.Authentication;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentications;
using FluentValidation;

namespace eCommerce.Application.Services.Implementations.Authentication
{
    public class AuthenticationService(ITokenManagement tokenManagement, IUserManagement userManagement, IRoleManagement roleManagement, IAppLogger<AuthenticationService> logger, IMapper mapper, IValidator<CreateUser> createUservlaidator, IValidator<LoginUser> loginUserValidator, IValidationService validationService) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            var validationResult = await validationService.ValidateAsync(user, createUservlaidator);
            if(!validationResult.Success)
                return validationResult;
            var mappedModel = mapper.Map<AppUser>(user);
            mappedModel.UserName = user.Email;
            mappedModel.PasswordHash = user.Password;

            var result = await userManagement.CreateUserAsync(mappedModel);
            if (!result)
                return new ServiceResponse
                {
                    Message = "User failed to create"
                };

            var _user = await userManagement.GetUserByEmailAsync(user.Email!);
            var users = await userManagement.GetUsersByEmailAsync();
            bool assignedRole = await roleManagement.AddUserToRole(_user!, users!.Count()> 1 ? "User" : "Admin");

            if (!assignedRole)
            {
                int removeUserResult = await userManagement.RemoveUserByEmail(user.Email!);
                if(removeUserResult <= 0)
                {
                    logger.LogError(new Exception($"User with Email as {_user!.Email} failed to be removed as a result of role assigning issue"), "User could not be assigned role");
                    return new ServiceResponse
                    {
                        Message = "Error occured in creating account"
                    };
                }
            }
            return new ServiceResponse
            {
                Message = "User created successfully"
            };

        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var _validateionResult = await validationService.ValidateAsync(user, loginUserValidator);
            if(!_validateionResult.Success)
                return new LoginResponse
                {
                    Message = _validateionResult.Message
                };

            var mappedModel = mapper.Map<AppUser>(user);
            mappedModel.PasswordHash = user.Password;

            bool loginResult = await userManagement.LoginUser(mappedModel);
            if(!loginResult)
                return new LoginResponse
                {
                    Message = "Email not found or password is incorrect"
                };

            var _user = await userManagement.GetUserByEmailAsync(user.Email!);
            var claims = await userManagement.GetUserClaimsAsync(user.Email!);

            var token = tokenManagement.GenerateAccessToken(claims);
            string jwtToken = tokenManagement.GenerateAccessToken(claims);
            string refreshToken = tokenManagement.GetRefreshToken();

            int result = await tokenManagement.AddRefreshToken(_user!.Id!, refreshToken);
            return result <= 0 ? new LoginResponse
            {
                Message = "Error occured in creating account"
            } : new LoginResponse
            {
                Success = true,
                Message = "User created successfully",
                Token = jwtToken,
                RefreshToken = refreshToken
            };

        }

        public Task<LoginResponse> ReviveToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
