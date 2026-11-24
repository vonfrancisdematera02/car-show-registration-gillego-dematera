using Api.Endpoints.Registrations;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Application.Features.Registrations.CreateRegistration;
using Application.Features.Registrations.GetRegistrationById;
using Application.Features.Registrations.GetRegistrations;
using Application.Features.Registrations.GetRegistrationSummary;




var builder = WebApplication.CreateBuilder(args);
// CORS
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? [];


var isDevelopment = builder.Environment.IsDevelopment();


builder.Services.AddCors(options =>
    options.AddPolicy("Frontend", policy =>
        policy.SetIsOriginAllowed(origin =>
                allowedOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase) ||
                (isDevelopment &&
                 Uri.TryCreate(origin, UriKind.Absolute, out var uri) &&
                 uri.IsLoopback))
            .AllowAnyHeader()
            .AllowAnyMethod()));
// Database
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=carshow.db"));
builder.Services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());
// Validators
builder.Services.AddValidatorsFromAssembly(
    typeof(GetRegistrationsQueryValidator).Assembly);


// Handlers
builder.Services.AddScoped<ICommandHandler<CreateRegistrationCommand, Result<RegistrationResponse>>, CreateRegistrationCommandHandler>();
builder.Services.AddScoped<GetRegistrationsQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetRegistrationsQuery, Result<GetRegistrationsResponse>>, GetRegistrationsQueryHandler>();
//builder.Services.AddScoped<ICommandHandler<TCommand, Result<TResponse>>, TCommandHandler>();
// builder.Services.AddScoped<IQueryHandler<TQuery, Result<TResponse>>, TQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetRegistrationByIdQuery, Result<RegistrationResponse>>, GetRegistrationByIdQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetRegistrationSummaryQuery, Result<GetRegistrationSummaryResponse>>, GetRegistrationSummaryQueryHandler>();


// API
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureHttpJsonOptions(options => { options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()); });
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info.Title = "Vertical Slice API Template by Elvin Manuel R. Luces, MIT";
        document.Info.Description = "A simple API demonstrating Vertical Slice Architecture";
        return Task.CompletedTask;
    });
});


var app = builder.Build();
// CORS Policy
app.UseCors("Frontend");
// Seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();


    // dbContext.<Entities>.AddRange(...);


    dbContext.SaveChanges();
}


// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


// Root endpoint
app.MapGet("/", () => "Welcome to the Vertical Slice API! Go to /scalar/v1 for API documentation.");




// Map endpoints
app.MapRegistrationEndpoints();
// app.Map{FeatureGroup}Endpoints();


app.Run();
