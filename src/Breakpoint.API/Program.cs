using Breakpoint.API.Filters;
using Breakpoint.API.Middlewares;
using Breakpoint.Business;
using Breakpoint.Domain;
using Breakpoint.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

#if RELEASE
using Microsoft.OpenApi.Models;
#endif

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

var settings = new BreakpointSettings(config);
builder.Services.AddSingleton<IBreakpointSettings>(settings);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(x =>
{
#if RELEASE
	x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Scheme = "bearer",
		Description = "Please insert JWT into field"
	});
#endif

	x.OperationFilter<SwaggerAuthFilter>();
});

builder.Services.AddDomain();
builder.Services.AddService();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Jwt.Key!)),
						ClockSkew = TimeSpan.Zero
					};
				});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#if DEBUG
app.UseMiddleware<SwaggerAuthMiddleware>();
#endif

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();