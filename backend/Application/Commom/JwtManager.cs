using CrossCutting.Commom;
using CrossCutting.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Commom
{
	public class JwtManager : IJwtManager
	{
		private readonly IConfiguration _configuration;

        public  JwtManager(IConfiguration configuration)
        {
			_configuration = configuration;
		}

        public string GenerateToken(Employer employer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var claims = new[]
            {
           new Claim(ClaimTypes.NameIdentifier, employer.Id.ToString()),
           new Claim(ClaimTypes.Name, employer.FirstName),
           new Claim(ClaimTypes.Role, ((int)employer.Role).ToString())
       };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

