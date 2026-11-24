namespace Application.Features.Registrations.GetRegistrationSummary;


public sealed record GetRegistrationSummaryResponse(
    int TotalRegistrations,
    int PendingRegistrations,
    int ConfirmedRegistrations,
    int CheckedInRegistrations,
    int CancelledRegistrations,
    decimal TotalRegistrationFees,
    decimal TotalAmountPaid,
    decimal OutstandingBalance
);
