using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
namespace Application.Features.Registrations.GetRegistrationSummary;

public sealed class GetRegistrationSummaryQueryHandler
    : IQueryHandler<GetRegistrationSummaryQuery,
        Result<GetRegistrationSummaryResponse>>
{
    private readonly IAppDbContext _context;
    public GetRegistrationSummaryQueryHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<Result<GetRegistrationSummaryResponse>> HandleAsync(
        GetRegistrationSummaryQuery query,
        CancellationToken cancellationToken = default)
    {
        var registrations = await _context.Registrations
            .AsNoTracking()
            .Select(r => new
            {
                r.Status,
                r.RegistrationFee,
                r.AmountPaid
            })
            .ToListAsync(cancellationToken);
        var activeRegistrations = registrations
            .Where(r => r.Status != RegistrationStatus.Cancelled)
            .ToList();
        var response = new GetRegistrationSummaryResponse(
            TotalRegistrations: registrations.Count,
            PendingRegistrations: registrations.Count(
                r => r.Status == RegistrationStatus.Pending),
            ConfirmedRegistrations: registrations.Count(
                r => r.Status == RegistrationStatus.Confirmed),
            CheckedInRegistrations: registrations.Count(
                r => r.Status == RegistrationStatus.CheckedIn),
            CancelledRegistrations: registrations.Count(
                r => r.Status == RegistrationStatus.Cancelled),
            TotalRegistrationFees: activeRegistrations.Sum(
                r => r.RegistrationFee),
            TotalAmountPaid: activeRegistrations.Sum(
                r => r.AmountPaid),
            OutstandingBalance: activeRegistrations.Sum(
                r => r.RegistrationFee - r.AmountPaid)
        );
        return Result<GetRegistrationSummaryResponse>.Success(response);
    }
}
