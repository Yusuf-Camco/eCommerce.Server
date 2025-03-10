using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentications;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerce.Infrastructure.Repositories.Authentication
{
    public class TokenManagement(AppDbContext context, IConfiguration config) : ITokenManagement
    {
        public async Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            context.RefreshTokens.Add(new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                ExpiryDate = DateTime.Now.AddDays(7)
            });
            return await context.SaveChangesAsync();
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetRefreshToken()
        {
           const int byteSize = 64;
            byte[] randomBytes = new byte[byteSize];
            using(RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public List<Claim> GetUserClaimsFromToken(string token)
        {
          var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if(jwtToken == null)
                return new List<Claim>();
            return jwtToken.Claims.ToList();
        }

        public async Task<string> GetUserIdByRefreshToken(string refreshToken)
        {
            return (await context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken))!.UserId!;
        }

        public Task<bool> RevokeRefreshToken(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
        {
            var data = new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
            };
           var user = await context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
            if(user == null)
            {
                return -1;
                //context.RefreshTokens.Add(data);
            }
            else
            {
                user.Token = refreshToken;
            }   
            return await context.SaveChangesAsync();
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
           var user = await context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
            return user != null;
        }        
    }
}
