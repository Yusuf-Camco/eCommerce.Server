﻿using System.Security.Claims;

namespace eCommerce.Domain.Interfaces.Authentications
{
    public interface ITokenManagement
    {
        string GetRefreshToken();
        List<Claim> GetUserClaimsFromToken(string token);
        Task<bool> RevokeRefreshToken(string email);
        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<string> GetUserIdByRefreshToken(string refreshToken);
        Task<int> AddRefreshToken(string userId, string refreshToken);
        Task<int> UpdateRefreshToken(string userId, string refreshToken);
        string GenerateAccessToken(IEnumerable<Claim> claims);
    }
}
