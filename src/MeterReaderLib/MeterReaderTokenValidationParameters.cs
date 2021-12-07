using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaderLib
{
  public class MeterReaderTokenValidationParameters : TokenValidationParameters
  {
    public MeterReaderTokenValidationParameters(IConfiguration config)
    {
      ValidIssuer = config["Tokens:Issuer"];
      ValidAudience = config["Tokens:Audience"];
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
    }
  }
}
