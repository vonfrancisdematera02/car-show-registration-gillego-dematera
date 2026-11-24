using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ParticipantName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    CarMakeModel = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PlateNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    RegistrationType = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    RegistrationFee = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    AmountPaid = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "Id", "Address", "AmountPaid", "CarMakeModel", "ContactNumber", "Email", "ParticipantName", "PlateNumber", "RegistrationDate", "RegistrationFee", "RegistrationNumber", "RegistrationType", "Status" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Sorsogon City, Philippines", 0m, "Toyota Supra MK5", "09170000001", "participant1@example.com", "Von Francis dematera", "DEMO0001", new DateTime(2026, 11, 1, 8, 0, 0, 0, DateTimeKind.Utc), 1500m, "CS27-9001", "Exhibitor", "Pending" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Legazpi City, Philippines", 1250m, "Honda Civic Type R", "09170000002", "participant2@example.com", "Rafah rafah", "DEMO0002", new DateTime(2026, 11, 2, 8, 0, 0, 0, DateTimeKind.Utc), 2500m, "CS27-9002", "Competitor", "Confirmed" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Naga City, Philippines", 5000m, "Nissan GT-R R35", "09170000003", "participant3@example.com", "Mica Juadines", "DEMO0003", new DateTime(2026, 11, 3, 8, 0, 0, 0, DateTimeKind.Utc), 5000m, "CS27-9003", "VIP", "CheckedIn" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Daraga, Albay, Philippines", 0m, "Mitsubishi Lancer Evolution X", "09170000004", "participant4@example.com", "Von Dematera", "DEMO0004", new DateTime(2026, 11, 4, 8, 0, 0, 0, DateTimeKind.Utc), 1500m, "CS27-9004", "Exhibitor", "Cancelled" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Irosin, Sorsogon, Philippines", 1250m, "Subaru WRX STI", "09170000005", "participant5@example.com", "Ninio Feolino", "DEMO0005", new DateTime(2026, 11, 5, 8, 0, 0, 0, DateTimeKind.Utc), 2500m, "CS27-9005", "Competitor", "Pending" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "bulusan, Sorsogon, Philippines", 5000m, "Mazda RX-7", "09170000006", "participant6@example.com", "Aivy Bolano", "DEMO0006", new DateTime(2026, 11, 6, 8, 0, 0, 0, DateTimeKind.Utc), 5000m, "CS27-9006", "VIP", "Confirmed" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Sorsogon City, Philippines", 0m, "Toyota GR86", "09170000007", "participant7@example.com", "Mary Rose Soriano", "DEMO0007", new DateTime(2026, 11, 7, 8, 0, 0, 0, DateTimeKind.Utc), 1500m, "CS27-9007", "Exhibitor", "CheckedIn" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Legazpi City, Philippines", 1250m, "Honda Civic RS", "09170000008", "participant8@example.com", "Angel Gillego", "DEMO0008", new DateTime(2026, 11, 8, 8, 0, 0, 0, DateTimeKind.Utc), 2500m, "CS27-9008", "Competitor", "Cancelled" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Naga City, Philippines", 5000m, "Ford Mustang GT", "09170000009", "participant9@example.com", "Venmar Fuellas", "DEMO0009", new DateTime(2026, 11, 9, 8, 0, 0, 0, DateTimeKind.Utc), 5000m, "CS27-9009", "VIP", "Pending" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Daraga, Albay, Philippines", 0m, "BMW M3", "09170000010", "participant10@example.com", "Francis Garais", "DEMO0010", new DateTime(2026, 11, 10, 8, 0, 0, 0, DateTimeKind.Utc), 1500m, "CS27-9010", "Exhibitor", "Confirmed" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Irosin, Sorsogon, Philippines", 1250m, "Toyota 86", "09170000011", "participant11@example.com", "Wendel Duque", "DEMO0011", new DateTime(2026, 11, 11, 8, 0, 0, 0, DateTimeKind.Utc), 2500m, "CS27-9011", "Competitor", "CheckedIn" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "bulusan, Sorsogon, Philippines", 5000m, "Nissan Silvia S15", "09170000012", "participant12@example.com", "Angelica Greago", "DEMO0012", new DateTime(2026, 11, 12, 8, 0, 0, 0, DateTimeKind.Utc), 5000m, "CS27-9012", "VIP", "Cancelled" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "Sorsogon City, Philippines", 0m, "Honda Integra Type R", "09170000013", "participant13@example.com", "Aiden Evasco", "DEMO0013", new DateTime(2026, 11, 13, 8, 0, 0, 0, DateTimeKind.Utc), 1500m, "CS27-9013", "Exhibitor", "Pending" },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "Legazpi City, Philippines", 1250m, "Mazda MX-5 Miata", "09170000014", "participant14@example.com", "Camille Gonzales", "DEMO0014", new DateTime(2026, 11, 14, 8, 0, 0, 0, DateTimeKind.Utc), 2500m, "CS27-9014", "Competitor", "Confirmed" },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "Naga City, Philippines", 5000m, "Chevrolet Camaro SS", "09170000015", "participant15@example.com", "Christian Aquino", "DEMO0015", new DateTime(2026, 11, 15, 8, 0, 0, 0, DateTimeKind.Utc), 5000m, "CS27-9015", "VIP", "CheckedIn" },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "Daraga, Albay, Philippines", 0m, "Porsche 911 Carrera", "09170000016", "participant16@example.com", "Gabriel Navarro", "DEMO0016", new DateTime(2026, 11, 16, 8, 0, 0, 0, DateTimeKind.Utc), 1500m, "CS27-9016", "Exhibitor", "Cancelled" },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "Irosin, Sorsogon, Philippines", 1250m, "Toyota GR Yaris", "09170000017", "participant17@example.com", "Isabella Cruz", "DEMO0017", new DateTime(2026, 11, 17, 8, 0, 0, 0, DateTimeKind.Utc), 2500m, "CS27-9017", "Competitor", "Pending" },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "bulusan, Sorsogon, Philippines", 5000m, "Mitsubishi Lancer EX", "09170000018", "participant18@example.com", "Rafael Domingo", "DEMO0018", new DateTime(2026, 11, 18, 8, 0, 0, 0, DateTimeKind.Utc), 5000m, "CS27-9018", "VIP", "Confirmed" },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "Sorsogon City, Philippines", 0m, "Hyundai Genesis Coupe", "09170000019", "participant19@example.com", "Trisha Morales", "DEMO0019", new DateTime(2026, 11, 19, 8, 0, 0, 0, DateTimeKind.Utc), 1500m, "CS27-9019", "Exhibitor", "CheckedIn" },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "Legazpi City, Philippines", 1250m, "Nissan 370Z", "09170000020", "participant20@example.com", "Kevin Salazar", "DEMO0020", new DateTime(2026, 11, 20, 8, 0, 0, 0, DateTimeKind.Utc), 2500m, "CS27-9020", "Competitor", "Cancelled" },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "Naga City, Philippines", 5000m, "BMW M4", "09170000021", "participant21@example.com", "Bianca Herrera", "DEMO0021", new DateTime(2026, 11, 21, 8, 0, 0, 0, DateTimeKind.Utc), 5000m, "CS27-9021", "VIP", "Pending" },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "Daraga, Albay, Philippines", 0m, "Honda S2000", "09170000022", "participant22@example.com", "Adrian Mercado", "DEMO0022", new DateTime(2026, 11, 22, 8, 0, 0, 0, DateTimeKind.Utc), 1500m, "CS27-9022", "Exhibitor", "Confirmed" },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "Irosin, Sorsogon, Philippines", 1250m, "Toyota GR Supra", "09170000023", "participant23@example.com", "Jasmine Lopez", "DEMO0023", new DateTime(2026, 11, 23, 8, 0, 0, 0, DateTimeKind.Utc), 2500m, "CS27-9023", "Competitor", "CheckedIn" },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "bulusan, Sorsogon, Philippines", 5000m, "Subaru BRZ", "09170000024", "participant24@example.com", "Nathaniel Perez", "DEMO0024", new DateTime(2026, 11, 24, 8, 0, 0, 0, DateTimeKind.Utc), 5000m, "CS27-9024", "VIP", "Cancelled" },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "Sorsogon City, Philippines", 0m, "Ford Focus RS", "09170000025", "participant25@example.com", "Erika Fernandez", "DEMO0025", new DateTime(2026, 11, 25, 8, 0, 0, 0, DateTimeKind.Utc), 1500m, "CS27-9025", "Exhibitor", "Pending" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_PlateNumber",
                table: "Registrations",
                column: "PlateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_RegistrationNumber",
                table: "Registrations",
                column: "RegistrationNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");
        }
    }
}
