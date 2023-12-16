using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatViP_API.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeUpdated",
                table: "ExpertApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "$2a$11$abMw1mxOs0jTcpxZB1KgcOofo9emmJulAu2BNxJDKca0/Wo.u/lcC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Password",
                value: "$2a$11$.oGf37pfJa61sZBrJQqTNusnNYCZWSm3r6.8ffMFw6GeqM9Bdp7HO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Password",
                value: "$2a$11$YnSEayzp7rO/qEY5L9D73OKNYrTH6o6KoLQDQYM5MPDbBcFalI3Oe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Password",
                value: "$2a$11$17Zh7q.wqEzTrZxEah2nBOs02GcwNwz4wE2f4GtgX/u7QKv7EnkWq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DateTimeUpdated",
                table: "ExpertApplications");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "$2a$11$pgWxbecCwHUstYdLviI/u.pT0NHqrOmmoDYid.5hQ1xnmANG1vkJS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Password",
                value: "$2a$11$CdBiJGiO4THhyPyXiJj4reEGvHGNk8smuVCSZmZWgU94gkeDw/gmK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Password",
                value: "$2a$11$G.nrmkUptcAnr4iP9WZJoeCVuS664HqEsSc3J8OWyPA02zQqXEyIe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Password",
                value: "$2a$11$eKn1KbVgaM1Co3g6jlQG6Ol2V3mh3.cWJrg98tHtKRV/V5zwL9M.6");
        }
    }
}
