using System.Reflection;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServicesModule();
// Repositories
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();

builder.Services.AddDbContextPool<RepositoryDbContext>(options => 
    options
    .UseSqlServer(builder.Configuration.GetConnectionString("BlogProjectConnectionString"))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // Should Change EfRepositoryBase.cs 
);
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

app.UseAuthorization();

app.MapControllers();

app.Run();
