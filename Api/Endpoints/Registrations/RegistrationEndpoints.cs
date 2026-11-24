using Application.Abstractions.Messaging;
using Application.Features.Registrations.CreateRegistration;
using Application.Features.Registrations.GetRegistrationById;
using Application.Features.Registrations.GetRegistrations;
using Application.Features.Registrations.GetRegistrationSummary;
using Domain.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Api.Endpoints.Registrations;


public static class RegistrationEndpoints
{
    public static IEndpointRouteBuilder MapRegistrationEndpoints(
        this IEndpointRouteBuilder app)
    {
        // CREATE REGISTRATION

        // CREATE REGISTRATION
        app.MapPost("/api/registrations", async (
            CreateRegistrationCommand command,
            ICommandHandler<CreateRegistrationCommand,
                Result<RegistrationResponse>> handler,
            [FromServices] IValidator<CreateRegistrationCommand> validator,
            CancellationToken cancellationToken) =>
        {
            // Validate fields and return errors by property name
            var validationResult = await validator.ValidateAsync(
                command, cancellationToken);


            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray());


                return Results.ValidationProblem(errors);
            }


            var result = await handler.HandleAsync(
                command, cancellationToken);


            if (!result.IsSuccess)
            {
                if (result.Error?.Code == "Registration.DuplicatePlate")
                {
                    return Results.Conflict(new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message
                    });
                }


                return Results.BadRequest(new
                {
                    code = result.Error?.Code,
                    message = result.Error?.Message
                });
            }


            return Results.Created(
                $"/api/registrations/{result.Value!.Id}",
                result.Value);
        })
        .WithName("CreateRegistration")
        .WithTags("Registrations");


        // GET ALL REGISTRATIONS
        app.MapGet("/api/registrations", async (
            [AsParameters] GetRegistrationsQuery query,
            [FromServices] IQueryHandler<GetRegistrationsQuery,
        Result<GetRegistrationsResponse>> handler,
            [FromServices] IValidator<GetRegistrationsQuery> validator,
            CancellationToken cancellationToken) =>
        {
            var validationResult = await validator.ValidateAsync(
                query, cancellationToken);


            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray());


                return Results.ValidationProblem(errors);
            }


            var result = await handler.HandleAsync(
                query, cancellationToken);


            if (!result.IsSuccess)
            {
                return Results.BadRequest(new
                {
                    code = result.Error?.Code,
                    message = result.Error?.Message
                });
            }


            return Results.Ok(result.Value);
        })
        .WithName("GetRegistrations")
        .WithTags("Registrations");
        // GET REGISTRATION BY ID
        app.MapGet("/api/registrations/{id:guid}", async (
            Guid id,
            [FromServices] IQueryHandler<GetRegistrationByIdQuery,
                Result<RegistrationResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRegistrationByIdQuery(id);


            var result = await handler.HandleAsync(
                query, cancellationToken);


            if (!result.IsSuccess)
            {
                return Results.NotFound(new
                {
                    code = result.Error?.Code,
                    message = result.Error?.Message
                });
            }


            return Results.Ok(result.Value);
        })
        .WithName("GetRegistrationById")
        .WithTags("Registrations");


        // GET Registration Summary
        app.MapGet("/api/registrations/summary", async (
            [FromServices] IQueryHandler<GetRegistrationSummaryQuery,
        Result<GetRegistrationSummaryResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRegistrationSummaryQuery();


            var result = await handler.HandleAsync(
                query, cancellationToken);


            return Results.Ok(result.Value);
        })
        .WithName("GetRegistrationSummary")
        .WithTags("Registrations");


        return app;
    }
}