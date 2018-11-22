using MeanCards.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeanCards.WebAPI.Services
{
    public interface IJwtTokenFactory
    {
        string GenerateEncodedToken(string userName);
    }

    public class JwtTokenFactory : IJwtTokenFactory
    {
        private readonly IAuthConfiguration authConfiguration;

        public JwtTokenFactory(IAuthConfiguration authConfiguration)
        {
            this.authConfiguration = authConfiguration;
        }

        public string GenerateEncodedToken(string userName)
        {
            var key = Encoding.ASCII.GetBytes(authConfiguration.GetSecret());
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userName)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddMinutes(authConfiguration.GetExpirationTimeInMinutes()),
                Issuer = authConfiguration.GetIssuer(),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
