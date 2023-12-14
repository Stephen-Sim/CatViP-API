using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatViP_API.Migrations
{
    /// <inheritdoc />
    public partial class AddDateTimeUpdatedToTableExpertApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                value: "$2a$11$.iGSFqBeV4GP17WW4AmUb.loxbd/9ggXYemdc8jfXmXrbnIPtgSCy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Password",
                value: "$2a$11$bqZ51U.HQB0D4.ZiT8HbnuW5j4h0AL/29czXCCXAeLOqis.mesBp2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Password",
                value: "$2a$11$M.iBKzEZ/lYMfgEofrRKL.ZPZdJ6j77DoSdPHL3DfT9fm6EPqUNKm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Password",
                value: "$2a$11$QVBarp5N82I3F4uprLQj0ur0cjxP.XDZQkADLUhuhS3RGzYTCBAYS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
