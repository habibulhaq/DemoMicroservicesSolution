using JwtAuthenticationManager.Contracts;
using JwtAuthenticationManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        private readonly string _secret;
        private readonly string _expDate;
        private readonly IConfiguration _configuration;
        private readonly MicroservicesUsersDBContext _context;
        public JwtTokenHandler(MicroservicesUsersDBContext context, IConfiguration config)
        {
            //_configuration = config;
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
            _context = context;
        }

        public async Task<AuthenticationResponse> GenerateSecurityToken(AuthenticationRequest request)
        {

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return null;
            }

            var dbUser = await _context.Users.Where(x => x.Username.ToLower() == request.Username.ToLower() && x.Password == request.Password).FirstOrDefaultAsync();
            if (dbUser != null)
            {
                var tokenExpiry = DateTime.UtcNow.AddMinutes(double.Parse(_expDate));
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, request.Username.ToString()),
                        new Claim("Role", dbUser.Role)
                    }),
                    Expires = tokenExpiry,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                var token = tokenHandler.WriteToken(securityToken);
                return new AuthenticationResponse
                {
                    Username = request.Username,
                    ExpiresIn = (int)tokenExpiry.Subtract(DateTime.UtcNow).TotalSeconds,
                    JwtToken = token
                };
            }
            else
            {
                return null;
            }
        }
    }
}
