using Application.Abstractions.Data;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistence;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Registration> Registrations => Set<Registration>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.RegistrationNumber)
                .IsRequired()
                .HasMaxLength(20);
            entity.HasIndex(r => r.RegistrationNumber)
                .IsUnique();
            entity.Property(r => r.ParticipantName)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(r => r.ContactNumber)
                .HasMaxLength(11);
            entity.Property(r => r.Email)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(r => r.Address)
                .HasMaxLength(300);
            entity.Property(r => r.CarMakeModel)
                .HasMaxLength(100);
            entity.Property(r => r.PlateNumber)
                .IsRequired()
                .HasMaxLength(15);
            entity.HasIndex(r => r.PlateNumber)
                .IsUnique();
            entity.Property(r => r.RegistrationType)
                .HasConversion<string>();
            entity.Property(r => r.Status)
                .HasConversion<string>();
            entity.Property(r => r.RegistrationFee)
                .HasPrecision(10, 2);
            entity.Property(r => r.AmountPaid)
                .HasPrecision(10, 2);
        });
        // Seed 25 sample registrations
        modelBuilder.Entity<Registration>().HasData(
            CreateSeedData()
        );
    }
    private static Registration[] CreateSeedData()
    {
        var names = new[]
        {
        "Von Francis dematera",
        "Rafah rafah",
        "Mica Juadines",
        "Von Dematera",
        "Ninio Feolino",
        "Aivy Bolano",
        "Mary Rose Soriano",
        "Angel Gillego",
        "Venmar Fuellas",
        "Francis Garais",
        "Wendel Duque",
        "Angelica Greago",
        "Aiden Evasco",
        "Camille Gonzales",
        "Christian Aquino",
        "Gabriel Navarro",
        "Isabella Cruz",
        "Rafael Domingo",
        "Trisha Morales",
        "Kevin Salazar",
        "Bianca Herrera",
        "Adrian Mercado",
        "Jasmine Lopez",
        "Nathaniel Perez",
        "Erika Fernandez"
    };
        var cars = new[]
        {
        "Toyota Supra MK5",
        "Honda Civic Type R",
        "Nissan GT-R R35",
        "Mitsubishi Lancer Evolution X",
        "Subaru WRX STI",
        "Mazda RX-7",
        "Toyota GR86",
        "Honda Civic RS",
        "Ford Mustang GT",
        "BMW M3",
        "Toyota 86",
        "Nissan Silvia S15",
        "Honda Integra Type R",
        "Mazda MX-5 Miata",
        "Chevrolet Camaro SS",
        "Porsche 911 Carrera",
        "Toyota GR Yaris",
        "Mitsubishi Lancer EX",
        "Hyundai Genesis Coupe",
        "Nissan 370Z",
        "BMW M4",
        "Honda S2000",
        "Toyota GR Supra",
        "Subaru BRZ",
        "Ford Focus RS"
    };
        var addresses = new[]
        {
        "Sorsogon City",
        "Legazpi City",
        "Naga City",
        "Daraga, Albay",
        "Irosin, Sorsogon",
        "bulusan, Sorsogon"
    };
        var types = new[]
        {
        RegistrationType.Exhibitor,
        RegistrationType.Competitor,
        RegistrationType.VIP
    };
        var statuses = new[]
        {
        RegistrationStatus.Pending,
        RegistrationStatus.Confirmed,
        RegistrationStatus.CheckedIn,
        RegistrationStatus.Cancelled
    };
        decimal[] fees = { 1500m, 2500m, 5000m };


        var registrations = new List<Registration>();


        for (int i = 0; i < 25; i++)
        {
            var typeIndex = i % 3;
            var statusIndex = i % 4;
            var fee = fees[typeIndex];
            var paymentPattern = i % 3;
            var amountPaid = paymentPattern switch
            {
                0 => 0m,
                1 => fee / 2,
                _ => fee
            };
            registrations.Add(new Registration
            {
                Id = Guid.Parse(
                   $"00000000-0000-0000-0000-{i + 1:000000000000}"),
                RegistrationNumber = $"CS27-{9000 + i + 1:0000}",


                RegistrationDate = new DateTime(
                    2026, 11, 1, 8, 0, 0, DateTimeKind.Utc)
                    .AddDays(i),
                ParticipantName = names[i],
                ContactNumber = $"091700000{i + 1:00}",
                Email = $"participant{i + 1}@example.com",
                Address = addresses[i % addresses.Length]
                    + ", Philippines",
                CarMakeModel = cars[i],
                PlateNumber = $"DEMO{i + 1:0000}",
                RegistrationType = types[typeIndex],
                Status = statuses[statusIndex],
                RegistrationFee = fee,
                AmountPaid = amountPaid
            });
        }
        return registrations.ToArray();
    }
}
