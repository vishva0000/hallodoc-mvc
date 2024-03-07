using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.Repository.Implementation
{
    public class JwtService : IJwtService
    {
        public IConfiguration Configuration { get; }
        public JwtService(IConfiguration configuration) 
        {
            Configuration = configuration;
        }
        
        public string GenerateToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey? key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("B374A26A71490437AA024E4FADD5B497FDFF1A8EA6FF12F6FB65AF2720B59CCF"));
            //var key = Encoding.UTF8.GetBytes("usAHUiji/dbhsaADSF/2763=v");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role), 
                    //new Claim(ClaimTypes., role),

                }),
                //Expires = DateTime.UtcNow.AddMinutes(double.Parse(Configuration["jwt:expiryDays"])),
                Expires = DateTime.UtcNow.AddMinutes(20),

                //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token, out JwtSecurityToken jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.UTF8.GetBytes(Configuration["jwt:key"]);
            SymmetricSecurityKey? key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("B374A26A71490437AA024E4FADD5B497FDFF1A8EA6FF12F6FB65AF2720B59CCF"));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    //IssuerSigningKey = new SymmetricSecurityKey(key),                    
                    IssuerSigningKey = key,
                    
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                jwtToken = (JwtSecurityToken)validatedToken;

                if(jwtToken != null)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                jwtToken = null;
                return false;
            }
            
        }
    }
}
