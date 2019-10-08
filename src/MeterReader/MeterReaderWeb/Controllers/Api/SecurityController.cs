using MeterReaderLib;
using MeterReaderLib.Models;
using MeterReaderWeb.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaderWeb.Controllers.Api
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [ApiController]
  [Route("api/{controller}")]
  public class SecurityController : ControllerBase
  {
    private readonly ILogger<SecurityController> _logger;
    private readonly JwtTokenValidationService _tokenService;

    public SecurityController(ILogger<SecurityController> logger, JwtTokenValidationService tokenService)
    {
      _logger = logger;
      _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("token")]
    public async Task<IActionResult> CreateToken(CredentialModel model)
    {
      var result = await _tokenService.GenerateTokenModelAsync(model);

      if (result.Success)
      {
        return Created("", new { token = result.Token, expiration = result.Expiration });
      }

      return BadRequest();
    }

  }
}
