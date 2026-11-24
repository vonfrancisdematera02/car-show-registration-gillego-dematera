using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Enums;
namespace Application.Features.Registrations.GetRegistrations;

public sealed record GetRegistrationsQuery(
    RegistrationType? RegistrationType,
    RegistrationStatus? Status,
    string? ParticipantName,
    int Page = 1,
    int PageSize = 10
) : IQuery<Result<GetRegistrationsResponse>>;
