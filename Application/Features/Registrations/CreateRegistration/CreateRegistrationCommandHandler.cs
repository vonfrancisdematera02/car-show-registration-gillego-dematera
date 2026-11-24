using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
namespace Application.Features.Registrations.CreateRegistration;

public sealed class CreateRegistrationCommandHandler
    : ICommandHandler<CreateRegistrationCommand, Result<RegistrationResponse>>
{
    private readonly IAppDbContext _dbContext;
    private readonly IValidator<CreateRegistrationCommand> _validator;
    public CreateRegistrationCommandHandler(
        IAppDbContext dbContext,
        IValidator<CreateRegistrationCommand> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    public async Task<Result<RegistrationResponse>> HandleAsync(
        CreateRegistrationCommand command,
        CancellationToken cancellationToken = default)
    {
        // Validate the submitted information
        var validation = await _validator.ValidateAsync(command, cancellationToken);


        if (!validation.IsValid)
        {
            return Result<RegistrationResponse>.Failure(
                Error.Validation("Registration.Validation",
                    validation.Errors[0].ErrorMessage));
        }
        // Normalize the plate number
        var plateNumber = command.PlateNumber.Trim().ToUpperInvariant();


        // Check for duplicate plate number
        var plateExists = await _dbContext.Registrations
            .AnyAsync(r => r.PlateNumber == plateNumber, cancellationToken);
        if (plateExists)
        {
            return Result<RegistrationResponse>.Failure(
                new Error("Registration.DuplicatePlate",
                    "This plate number is already registered."));
        }
        // Generate the next registration number
        var lastNumber = await _dbContext.Registrations
            .OrderByDescending(r => r.RegistrationNumber)
            .Select(r => r.RegistrationNumber)
            .FirstOrDefaultAsync(cancellationToken);
        var nextNumber = 1;
        if (!string.IsNullOrEmpty(lastNumber))
        {
            var numberPart = lastNumber.Split('-')[1];
            if (int.TryParse(numberPart, out var currentNumber))
            {
                nextNumber = currentNumber + 1;
            }
        }
        var registrationNumber = $"CS27-{nextNumber:D4}";
        // Set the registration fee based on type
        var fee = command.RegistrationType switch
        {
            RegistrationType.Exhibitor => 1500m,
            RegistrationType.Competitor => 2500m,
            RegistrationType.VIP => 5000m,
            _ => 0m
        };
        // Create the registration
        var registration = new Registration
        {
            Id = Guid.NewGuid(),
            RegistrationNumber = registrationNumber,
            RegistrationDate = DateTime.UtcNow,
            ParticipantName = command.ParticipantName.Trim(),
            ContactNumber = command.ContactNumber.Trim(),
            Email = command.Email.Trim(),
            Address = command.Address.Trim(),
            CarMakeModel = command.CarMakeModel.Trim(),
            PlateNumber = plateNumber,
            RegistrationType = command.RegistrationType,
            Status = RegistrationStatus.Pending,
            RegistrationFee = fee,
            AmountPaid = command.AmountPaid
        };
        _dbContext.Registrations.Add(registration);
        await _dbContext.SaveChangesAsync(cancellationToken);
        // Determine payment status
        var paymentStatus = registration.AmountPaid == 0
            ? "Unpaid"
            : registration.AmountPaid >= registration.RegistrationFee
                ? "Paid"
                : "Partial";
        // Return the registration response
        var response = new RegistrationResponse(
            registration.Id,
            registration.RegistrationNumber,
            registration.RegistrationDate,
            registration.ParticipantName,
            registration.ContactNumber,
            registration.Email,
            registration.Address,
            registration.CarMakeModel,
            registration.PlateNumber,
            registration.RegistrationType,
            registration.Status,
            registration.RegistrationFee,
            registration.AmountPaid,
            paymentStatus
        );
        return Result<RegistrationResponse>.Success(response);
    }
}
