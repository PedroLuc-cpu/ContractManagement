﻿using ContractManagement.Application.Abstractions;
using ContractManagement.Domain.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContractManagement.Infrastructure.Authentication
{
    internal sealed class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public string Generete(Member member)
        {
            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, member.Email.Value),

            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;

        }
    }
}
