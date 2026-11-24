using Application.Features.Registrations.CreateRegistration;
namespace Application.Features.Registrations.GetRegistrations;

public sealed record GetRegistrationsResponse(
    IReadOnlyList<RegistrationResponse> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages
);
