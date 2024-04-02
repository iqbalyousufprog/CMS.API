using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS.API.Migrations
{
    /// <inheritdoc />
    public partial class disease_data_seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseasePatient_Diseases_DiseaseId",
                table: "DiseasePatient");

            migrationBuilder.RenameColumn(
                name: "DiseaseId",
                table: "DiseasePatient",
                newName: "DiseasesId");

            migrationBuilder.InsertData(
                table: "Diseases",
                columns: new[] { "Id", "DiseaseName" },
                values: new object[,]
                {
                    { 1, "Flu" },
                    { 2, "Cold" },
                    { 3, "Fever" },
                    { 4, "Headache" },
                    { 5, "Sore throat" },
                    { 6, "Stomach flu" },
                    { 7, "Pneumonia" },
                    { 8, "Sinusitis" },
                    { 9, "Bronchitis" },
                    { 10, "Allergies" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DiseasePatient_Diseases_DiseasesId",
                table: "DiseasePatient",
                column: "DiseasesId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseasePatient_Diseases_DiseasesId",
                table: "DiseasePatient");

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.RenameColumn(
                name: "DiseasesId",
                table: "DiseasePatient",
                newName: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiseasePatient_Diseases_DiseaseId",
                table: "DiseasePatient",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
