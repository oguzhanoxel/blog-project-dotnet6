using Application;
using Core.CrossCuttingConcers.Exceptions;
using Core.Security.Encryption;
using Core.Security.JWT;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServicesModule();
builder.Services.AddSecurityServices();
// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
builder.Services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();

builder.Services.AddDbContext<RepositoryDbContext>(options =>
	options
	.UseSqlServer(builder.Configuration.GetConnectionString("BlogProjectConnectionString"))
	.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // Should Change EfRepositoryBase.cs 
);

TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidIssuer = tokenOptions?.Issuer,
		ValidAudience = tokenOptions?.Audience,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions?.SecurityKey)
	};
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
