using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProConstructionsManagment.Infrastructure.Migrations
{
    public partial class Projects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Projects",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactLastName = table.Column<string>(nullable: true),
                    ContactPhoneNumber = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true),
                    StartDate = table.Column<string>(nullable: true),
                    EndDate = table.Column<string>(nullable: true),
                    PlaceOfPerformance = table.Column<string>(nullable: true),
                    PercentageOfImplementation = table.Column<int>(),
                    RequiredNumberOfEmployees = table.Column<int>(),
                    NumberOfEmployees = table.Column<int>(),
                    NumerOfCandidates = table.Column<int>(),
                    NumberOfHours = table.Column<int>(),
                    DateOfPaymentDelivery = table.Column<string>(nullable: true),
                    DateOfPayment = table.Column<string>(nullable: true),
                    PaymentProtocol = table.Column<bool>(),
                    Agreement = table.Column<bool>(),
                    PaymentStatus = table.Column<int>(),
                    Status = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_Projects", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Projects");
        }
    }
}