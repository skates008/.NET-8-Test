﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sigma.ORM.Migrations
{
	/// <inheritdoc />
	public partial class Init : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Candidates",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
					PreferredCallTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
					LinkedInProfile = table.Column<string>(type: "nvarchar(max)", nullable: false),
					GitHubProfile = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Candidates", x => x.Id);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Candidates_Email",
				table: "Candidates",
				column: "Email",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Candidates");
		}
	}
}
