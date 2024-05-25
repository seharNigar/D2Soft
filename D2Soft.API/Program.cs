using AutoMapper;
using D2Soft.API.Middleware;
using D2Soft.Application.DTOs;
using D2Soft.Application.Interfaces;
using D2Soft.Application.Mappings;
using D2Soft.Application.Services;
using D2Soft.Application.Validators;
using D2Soft.Domain.Interfaces;
using D2Soft.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile<UserProfile>();
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();

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

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
