using DeveloperTest;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<SecretKeyAuthenticationHandler>();
builder.Services.AddAuthentication()
    .AddScheme<SecretKeyAuthenticationOptions, SecretKeyAuthenticationHandler>(SecretKeyAuthenticationOptions.DefaultScheme, null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition(SecretKeyAuthenticationOptions.DefaultScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = SecretKeyAuthenticationOptions.HeaderName,
        Type = SecuritySchemeType.ApiKey
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = SecretKeyAuthenticationOptions.DefaultScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<InvoiceContext>(configure =>
    configure.UseInMemoryDatabase("ToDoDatabase")
        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();