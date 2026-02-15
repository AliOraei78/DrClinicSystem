using DrClinicSystem.Core.Services;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DrClinicSystem API",
        Version = "v1",
        Description = "API for managing doctor's clinic system",
        Contact = new OpenApiContact
        {
            Name = "Ali Jenabi",
            Email = "a.jenabi78@example.com"
        }
    });
});

builder.Services.AddKeyedTransient<IMessageService, TransientMessageService>("transient");
builder.Services.AddKeyedScoped<IMessageService, ScopedMessageService>("scoped");
builder.Services.AddKeyedSingleton<IMessageService, SingletonMessageService>("singleton");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DrClinicSystem API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
