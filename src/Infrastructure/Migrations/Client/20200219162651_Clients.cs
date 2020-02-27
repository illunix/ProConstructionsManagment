using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProConstructionsManagment.Infrastructure.Migrations.Client
{
    public partial class Clients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Clients",
                table => new
                {
                    Id = table.Column<Guid>(),
                    CompanyName = table.Column<string>(nullable: true),
                    NIP = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactLastName = table.Column<string>(nullable: true),
                    ContactPhoneNumber = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Clients", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Clients");
        }
    }
}