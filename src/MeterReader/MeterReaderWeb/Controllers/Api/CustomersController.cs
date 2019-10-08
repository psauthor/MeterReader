using MeterReaderWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MeterReaderWeb.Controllers.Api
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [ApiController]
  [Route("api/{controller}")]
  public class CustomersController : ControllerBase
  {
    private readonly ILogger<CustomersController> _logger;
    private readonly IReadingRepository _repository;

    public CustomersController(ILogger<CustomersController> logger, IReadingRepository repository)
    {
      _logger = logger;
      _repository = repository;
    }

    public async Task<IActionResult> Get()
    {
      var result = await _repository.GetCustomersWithReadingsAsync();

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var result = await _repository.GetCustomerWithReadingsAsync(id);

      return Ok(result);
    }
  }
}
