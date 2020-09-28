using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codeizi.CQRS.Saga.FunctionalTest.Migrations
{
    public partial class InitialCreateSaga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdSaga = table.Column<Guid>(nullable: false),
                    Position = table.Column<byte>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    TypeState = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Scheduled = table.Column<DateTime>(nullable: false),
                    Initiate = table.Column<DateTime>(nullable: false),
                    Ended = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionSchudele",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SagaActionId = table.Column<Guid>(nullable: false),
                    ActionId = table.Column<Guid>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    TypeState = table.Column<string>(nullable: true),
                    Cancel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionSchudele", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogStateAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SagaId = table.Column<Guid>(nullable: false),
                    ActionId = table.Column<Guid>(nullable: false),
                    InitialState = table.Column<string>(nullable: true),
                    FinshedState = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogStateAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SagaInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SagaInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdSaga = table.Column<Guid>(nullable: false),
                    ExtendedData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "ActionSchudele");

            migrationBuilder.DropTable(
                name: "LogStateAction");

            migrationBuilder.DropTable(
                name: "SagaInfo");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
