using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EasyCommerce.Server.Shared.Common.TokenManager;

public class JwtAuthenticationManager : IJwtAuthenticationManager
{
    private readonly string _apikey;
    private readonly int _addMonths;
    public JwtAuthenticationManager(string apiKey, int addMonhts)
    {
        _apikey = apiKey;
        _addMonths = addMonhts;
    }
    public string BuildToken()
    {
        var tokenHalder = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(_apikey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMonths(_addMonths),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHalder.CreateToken(tokenDescriptor);
        return tokenHalder.WriteToken(token);
    }
}
