using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> CreateUser(CreateUser user);
        Task<LoginResponse> LoginUser(LoginUser user);
        Task<LoginResponse> ReviveToken(string refreshToken);
    }
}
