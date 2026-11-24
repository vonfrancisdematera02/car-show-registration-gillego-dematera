using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IAppDbContext
{
    DbSet<Registration> Registrations { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}