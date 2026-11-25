using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Features.Registrations.CreateRegistration;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
namespace Application.Features.Registrations.GetRegistrations;

public sealed class GetRegistrationsQueryHandler
    : IQueryHandler<GetRegistrationsQuery, Result<GetRegistrationsResponse>>
{
    private readonly IAppDbContext _dbContext;
    public GetRegistrationsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<GetRegistrationsResponse>> HandleAsync(
        GetRegistrationsQuery query,
        CancellationToken cancellationToken = default)
    {
        // Start with all registrations
        var registrations = _dbContext.Registrations
            .AsNoTracking()
            .AsQueryable();


        // Filter by registration type
        if (query.RegistrationType.HasValue)
        {
            registrations = registrations.Where(
                r => r.RegistrationType == query.RegistrationType.Value);
        }
        // Filter by registration status
        if (query.Status.HasValue)
        {
            registrations = registrations.Where(
                r => r.Status == query.Status.Value);
        }
        // Search by participant name
        if (!string.IsNullOrWhiteSpace(query.ParticipantName))
        {
            var searchName = query.ParticipantName.Trim().ToLower();


            registrations = registrations.Where(
                r => r.ParticipantName.ToLower().Contains(searchName));
        }
        // Count matching registrations before pagination
        var totalCount = await registrations.CountAsync(cancellationToken);
        // Ensure valid pagination values
        var page = Math.Max(1, query.Page);
        var pageSize = Math.Clamp(query.PageSize, 1, 50);
        // Get the requested page, newest registrations first
        var records = await registrations
            .OrderByDescending(r => r.RegistrationDate)
            .ThenByDescending(r => r.RegistrationNumber)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        // Convert database records into response objects
        var items = records.Select(registration =>
        {
            var paymentStatus = registration.AmountPaid == 0
                ? "Unpaid"
                : registration.AmountPaid >= registration.RegistrationFee
                    ? "Paid"
                    : "Partial";
            return new RegistrationResponse(
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
        }).ToList();
        // Calculate total pages
        var totalPages = (int)Math.Ceiling(
            (double)totalCount / pageSize);
        // Return the paginated response
        var response = new GetRegistrationsResponse(
            items,
            page,
            pageSize,
            totalCount,
            totalPages
        );
        return Result<GetRegistrationsResponse>.Success(response);
    }
}
