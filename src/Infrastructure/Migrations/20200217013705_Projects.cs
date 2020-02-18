using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProConstructionsManagment.Infrastructure.Migrations
{
    public partial class Projects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactLastName = table.Column<string>(nullable: true),
                    ContactPhoneNumber = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true),
                    StartDate = table.Column<string>(nullable: true),
                    EndDate = table.Column<string>(nullable: true),
                    PlaceOfPerformance = table.Column<string>(nullable: true),
                    PercentageOfImplementation = table.Column<int>(nullable: false),
                    RequiredNumberOfEmployees = table.Column<int>(nullable: false),
                    NumberOfEmployees = table.Column<int>(nullable: false),
                    NumerOfCandidates = table.Column<int>(nullable: false),
                    NumberOfHours = table.Column<int>(nullable: false),
                    DateOfPaymentDelivery = table.Column<string>(nullable: true),
                    DateOfPayment = table.Column<string>(nullable: true),
                    PaymentProtocol = table.Column<bool>(nullable: false),
                    Agreement = table.Column<bool>(nullable: false),
                    PaymentStatus = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
