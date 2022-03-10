using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder);

var app = builder.Build();

SetupMiddleware(app);
SetupApi(app);

app.Run();

static void SetupApi(WebApplication app)
{
  // Token Generation
  app.MapPost("/api/token", async Task<IResult> (CredentialModel model, JwtTokenValidationService tokenService) =>
  {
    var result = await tokenService.GenerateTokenModelAsync(model);

    if (result.Success)
    {
      return Results.Created("", new { token = result.Token, expiration = result.Expiration });
    }

    return Results.BadRequest();
  }).AllowAnonymous();

  // REST API
  app.MapGet("/api/customers", async Task<IResult> (IReadingRepository repo) =>
  {
    var result = await repo.GetCustomersWithReadingsAsync();

    return Results.Ok(result);
  });

  app.MapGet("/api/customers/{id:int}", async Task<IResult> (int id, IReadingRepository repo) =>
  {
    var result = await repo.GetCustomerWithReadingsAsync(id);

    return Results.Ok(result);
  });
}

static void SetupMiddleware(WebApplication webApp)
{
  // Configure the HTTP request pipeline.
  if (webApp.Environment.IsDevelopment())
  {
    webApp.UseMigrationsEndPoint();
  }
  else
  {
    webApp.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    webApp.UseHsts();
    webApp.UseHttpsRedirection();
  }

  webApp.UseStaticFiles();

  webApp.UseRouting();

  webApp.UseCors();

  webApp.UseAuthentication();
  webApp.UseAuthorization();

  webApp.MapRazorPages();

}


static void RegisterServices(WebApplicationBuilder bldr)
{
  bldr.Services.AddScoped<JwtTokenValidationService>();
  bldr.Services.AddAuthentication()
    .AddJwtBearer(cfg =>
    {
      cfg.TokenValidationParameters = new MeterReaderTokenValidationParameters(bldr.Configuration);
    });

  bldr.Services.AddCors(cfg =>
  {
    cfg.AddPolicy("AllowAll", opt =>
    {
      opt.AllowAnyOrigin();
      opt.AllowAnyMethod();
      opt.AllowAnyHeader();
    });
  });
  
  var connectionString = bldr.Configuration.GetConnectionString("DefaultConnection");
  bldr.Services.AddDbContext<ReadingContext>(options =>
      options.UseSqlServer(connectionString));
  bldr.Services.AddDatabaseDeveloperPageExceptionFilter();

  bldr.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
      .AddEntityFrameworkStores<ReadingContext>();

  bldr.Services.AddScoped<IReadingRepository, ReadingRepository>();

  bldr.Services.AddRazorPages();

}
