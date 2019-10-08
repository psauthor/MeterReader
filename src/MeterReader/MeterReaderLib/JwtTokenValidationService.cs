using MeterReaderLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaderLib
{
  public class JwtTokenValidationService
  {
    private readonly ILogger<JwtTokenValidationService> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _config;

    public JwtTokenValidationService(ILogger<JwtTokenValidationService> logger,
      UserManager<IdentityUser> userManager,
      SignInManager<IdentityUser> signInManager,
      IConfiguration config)
    {
      _logger = logger;
      _userManager = userManager;
      _signInManager = signInManager;
      _config = config;
    }

    public async Task<TokenModel> GenerateTokenModelAsync(CredentialModel model)
    {
      var user = await _userManager.FindByNameAsync(model.UserName);
      var result = new TokenModel()
      {
        Success = false
      };

      if (user != null)
      {
        var check = await _signInManager.CheckPasswordSignInAsync(user, model.Passcode, false);

        if (check.Succeeded)
        {
          // Create the token
          var claims = new[]
          {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
          };

          var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
          var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

          var token = new JwtSecurityToken(
            _config["Tokens:Issuer"],
            _config["Tokens:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

          result.Token = new JwtSecurityTokenHandler().WriteToken(token);
          result.Expiration = token.ValidTo;
          result.Success = true;

        }
      }


      return result;
    }
  }
}
