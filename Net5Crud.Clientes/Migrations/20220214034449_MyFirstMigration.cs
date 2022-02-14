using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Net5Crud.Clientes.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "varchar(15)", nullable: false),
                    contrasena = table.Column<string>(type: "varchar(15)", nullable: false),
                    intentos = table.Column<int>(type: "int", nullable: false),
                    nivelSeg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fechaReg = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
