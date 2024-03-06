using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessLayer.Repository.Interface
{
    public interface IJwtService
    {
        IConfiguration Configuration { get; }
        string GenerateToken(string username, string role);
        bool ValidateToken(string token, out JwtSecurityToken jwtToken);
    }
}