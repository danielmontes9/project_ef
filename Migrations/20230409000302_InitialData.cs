using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_ef.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c100"), null, "Actividades pendientes", 20 },
                    { new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"), null, "Actividades personales", 50 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c101"), new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c100"), null, new DateTime(2023, 4, 9, 0, 3, 1, 128, DateTimeKind.Utc).AddTicks(2705), 1, "Pago de servicios públicos" },
                    { new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c602"), new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"), null, new DateTime(2023, 4, 9, 0, 3, 1, 128, DateTimeKind.Utc).AddTicks(2942), 0, "Terminar de ver pelicula en Netflix" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c101"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c602"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c100"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
