
using EmployeeProfile;
using Microsoft.OpenApi.Models;
using MediatR;
using EmployeeProfile.Application.Queries.Departments;
using EmployeeProfile.Infrastructure;
using FluentValidation;
using FluentValidation.Validators;
using EmployeeProfile.Application.Validators;
using Microsoft.AspNetCore.Mvc;
using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee Profile", Version = "v1" });
});
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetDepartmentByIdHandler).Assembly);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Profile API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
