using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Features.Registrations.CreateRegistration;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
namespace Application.Features.Registrations.GetRegistrationById;

public sealed class GetRegistrationByIdQueryHandler
    : IQueryHandler<GetRegistrationByIdQuery,
        Result<RegistrationResponse>>
{
    private readonly IAppDbContext _context;
    public GetRegistrationByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<Result<RegistrationResponse>> HandleAsync(
        GetRegistrationByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var registration = await _context.Registrations
            .AsNoTracking()
            .FirstOrDefaultAsync(
                r => r.Id == query.Id,
                cancellationToken);
        if (registration is null)
        {
            return Result<RegistrationResponse>.Failure(
    Error.NotFound(
        "Registration.NotFound",
        "Registration was not found."));
        }
        var paymentStatus = registration.AmountPaid == 0
            ? "Unpaid"
            : registration.AmountPaid < registration.RegistrationFee
                ? "Partial"
                : "Paid";
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
            paymentStatus);
        return Result<RegistrationResponse>.Success(response);
    }
}
