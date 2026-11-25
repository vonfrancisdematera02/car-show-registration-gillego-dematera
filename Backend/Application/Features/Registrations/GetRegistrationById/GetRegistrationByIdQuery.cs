using Application.Abstractions.Messaging;
using Application.Features.Registrations.CreateRegistration;
using Domain.Common;


namespace Application.Features.Registrations.GetRegistrationById;

public sealed record GetRegistrationByIdQuery(Guid Id)
    : IQuery<Result<RegistrationResponse>>;
